using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using NullGuard;

public class PropertyProcessor
{
    private const string STR_ReturnValueOfPropertyIsNull = "[NullGuard] Return value of property '{0}' is null.";
    private const string STR_CannotSetTheValueOfPropertyToNull = "[NullGuard] Cannot set the value of property '{0}' to null.";

    private readonly bool isDebug;
    private readonly ValidationFlags validationFlags;

    public PropertyProcessor(ValidationFlags validationFlags, bool isDebug)
    {
        this.validationFlags = validationFlags;
        this.isDebug = isDebug;
    }

    public void Process(PropertyDefinition property)
    {
        try
        {
            if (property.IsGeneratedCode())
            {
                return;
            }
            InnerProcess(property);
        }
        catch (Exception exception)
        {
            throw new WeavingException(string.Format("An error occurred processing property '{0}'", property.FullName), exception);
        }
    }

    private void InnerProcess(PropertyDefinition property)
    {
        var localValidationFlags = validationFlags;

        if (!property.PropertyType.IsRefType())
            return;

        var attribute = property.DeclaringType.GetNullGuardAttribute();
        if (attribute != null)
        {
            localValidationFlags = (ValidationFlags)attribute.ConstructorArguments[0].Value;
        }

        if (!localValidationFlags.HasFlag(ValidationFlags.Properties)) return;

        if (property.AllowsNull())
            return;

        if (property.GetMethod != null && property.GetMethod.Body != null)
        {
            var getBody = property.GetMethod.Body;

            var sequencePoint = getBody.Instructions.Select(i => i.SequencePoint).FirstOrDefault();

            getBody.SimplifyMacros();

            if ((localValidationFlags.HasFlag(ValidationFlags.NonPublic) || (property.GetMethod.IsPublic && property.DeclaringType.IsPublic)) &&
                !property.GetMethod.MethodReturnType.AllowsNull()
               )
            {
                InjectPropertyGetterGuard(getBody, sequencePoint, property.PropertyType, String.Format(CultureInfo.InvariantCulture, STR_ReturnValueOfPropertyIsNull, property.FullName));
            }

            getBody.InitLocals = true;
            getBody.OptimizeMacros();
        }

        if (property.SetMethod != null && property.SetMethod.Body != null)
        {
            var setBody = property.SetMethod.Body;

            var sequencePoint = setBody.Instructions.Select(i => i.SequencePoint).FirstOrDefault();

            setBody.SimplifyMacros();

            if (localValidationFlags.HasFlag(ValidationFlags.NonPublic) || (property.SetMethod.IsPublic && property.DeclaringType.IsPublic))
            {
                InjectPropertySetterGuard(setBody, sequencePoint, property.SetMethod.Parameters[0], String.Format(CultureInfo.InvariantCulture, STR_CannotSetTheValueOfPropertyToNull, property.FullName));
            }

            setBody.InitLocals = true;
            setBody.OptimizeMacros();
        }
    }

    private void InjectPropertyGetterGuard(MethodBody getBody, SequencePoint seqPoint, TypeReference propertyType, string errorMessage)
    {
        var guardInstructions = new List<Instruction>();

        var returnPoints = getBody.Instructions
            .Select((o, i) => new { o, i })
            .Where(a => a.o.OpCode == OpCodes.Ret)
            .Select(a => a.i)
            .OrderByDescending(i => i);

        foreach (var ret in returnPoints)
        {
            var returnInstruction = getBody.Instructions[ret];

            guardInstructions.Clear();

            if (isDebug)
            {
                InstructionPatterns.DuplicateReturnValue(guardInstructions, propertyType);

                InstructionPatterns.CallDebugAssertInstructions(guardInstructions, errorMessage);
            }

            InstructionPatterns.DuplicateReturnValue(guardInstructions, propertyType);

            InstructionPatterns.IfNull(guardInstructions, returnInstruction, i =>
            {
                // Clean up the stack since we're about to throw up.
                i.Add(Instruction.Create(OpCodes.Pop));

                InstructionPatterns.LoadInvalidOperationException(i, errorMessage);

                // Throw the top item off the stack
                i.Add(Instruction.Create(OpCodes.Throw));
            });

            guardInstructions[0].HideLineFromDebugger(seqPoint);

            getBody.Instructions.Insert(ret, guardInstructions);
        }
    }

    private void InjectPropertySetterGuard(MethodBody setBody, SequencePoint seqPoint, ParameterDefinition valueParameter, string errorMessage)
    {
        if (!valueParameter.MayNotBeNull())
            return;

        var guardInstructions = new List<Instruction>();

        var entry = setBody.Instructions.First();

        if (isDebug)
        {
            InstructionPatterns.LoadArgumentOntoStack(guardInstructions, valueParameter);

            InstructionPatterns.CallDebugAssertInstructions(guardInstructions, errorMessage);
        }

        InstructionPatterns.LoadArgumentOntoStack(guardInstructions, valueParameter);

        InstructionPatterns.IfNull(guardInstructions, entry, i =>
        {
            InstructionPatterns.LoadArgumentNullException(i, valueParameter.Name, errorMessage);

            // Throw the top item off the stack
            i.Add(Instruction.Create(OpCodes.Throw));
        });

        guardInstructions[0].HideLineFromDebugger(seqPoint);

        setBody.Instructions.Prepend(guardInstructions);
    }
}