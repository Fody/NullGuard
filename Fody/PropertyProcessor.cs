using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Anotar.Custom;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using NullGuard;

public class PropertyProcessor
{
    const string ReturnValueOfPropertyIsNull = "[NullGuard] Return value of property '{0}' is null.";
    const string CannotSetTheValueOfPropertyToNull = "[NullGuard] Cannot set the value of property '{0}' to null.";

    bool isDebug;
    ValidationFlags validationFlags;

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
            LogTo.Error(exception, "An error occurred processing property '{0}'.", property.FullName);
        }
    }

    void InnerProcess(PropertyDefinition property)
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
            var getMethod = property.GetMethod;

            var doc = getMethod.DebugInformation.SequencePoints.FirstOrDefault()?.Document;

            getMethod.Body.SimplifyMacros();

            if ((localValidationFlags.HasFlag(ValidationFlags.NonPublic) || property.GetMethod.IsPublic && property.DeclaringType.IsPublicOrNestedPublic()) &&
                !property.GetMethod.MethodReturnType.AllowsNull()
               )
            {
                InjectPropertyGetterGuard(getMethod, doc, property);
            }

            getMethod.Body.InitLocals = true;
            getMethod.Body.OptimizeMacros();
        }

        if (property.SetMethod != null && property.SetMethod.Body != null)
        {
            var setBody = property.SetMethod.Body;

            var doc = property.SetMethod.DebugInformation.SequencePoints.FirstOrDefault()?.Document;

            setBody.SimplifyMacros();

            if (localValidationFlags.HasFlag(ValidationFlags.NonPublic) || property.SetMethod.IsPublic && property.DeclaringType.IsPublicOrNestedPublic())
            {
                InjectPropertySetterGuard(property.SetMethod, doc, property);
            }

            setBody.InitLocals = true;
            setBody.OptimizeMacros();
        }
    }

    void InjectPropertyGetterGuard(MethodDefinition getMethod, Document doc, PropertyReference property)
    {
        var guardInstructions = new List<Instruction>();

        var returnPoints = getMethod.Body.Instructions
            .Select((o, i) => new { o, i })
            .Where(a => a.o.OpCode == OpCodes.Ret)
            .Select(a => a.i)
            .OrderByDescending(i => i);

        foreach (var ret in returnPoints)
        {
            var returnInstruction = getMethod.Body.Instructions[ret];
            var errorMessage = string.Format(CultureInfo.InvariantCulture, ReturnValueOfPropertyIsNull, property.FullName);

            guardInstructions.Clear();

            if (isDebug)
            {
                InstructionPatterns.DuplicateReturnValue(guardInstructions, property.PropertyType);

                InstructionPatterns.CallDebugAssertInstructions(guardInstructions, errorMessage);
            }

            InstructionPatterns.DuplicateReturnValue(guardInstructions, property.PropertyType);

            InstructionPatterns.IfNull(guardInstructions, returnInstruction, i =>
            {
                // Clean up the stack since we're about to throw up.
                i.Add(Instruction.Create(OpCodes.Pop));

                InstructionPatterns.LoadInvalidOperationException(i, errorMessage);

                // Throw the top item off the stack
                i.Add(Instruction.Create(OpCodes.Throw));
            });

            getMethod.HideLineFromDebugger(guardInstructions[0], doc);

            getMethod.Body.InsertAtMethodReturnPoint(ret, guardInstructions);
        }
    }

    void InjectPropertySetterGuard(MethodDefinition setMethod, Document doc, PropertyDefinition property)
    {
        var valueParameter = property.SetMethod.GetPropertySetterValueParameter();

        if (!valueParameter.MayNotBeNull())
            return;

        var guardInstructions = new List<Instruction>();
        var errorMessage = string.Format(CultureInfo.InvariantCulture, CannotSetTheValueOfPropertyToNull, property.FullName);
        var entry = setMethod.Body.Instructions.First();

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

        setMethod.HideLineFromDebugger(guardInstructions[0], doc);

        setMethod.Body.Instructions.Prepend(guardInstructions);
    }
}