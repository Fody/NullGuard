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
            getBody.SimplifyMacros();

            if ((localValidationFlags.HasFlag(ValidationFlags.NonPublic) || property.GetMethod.IsPublic) &&
                !property.GetMethod.MethodReturnType.AllowsNull()
               )
            {
                InjectPropertyGetterGuard(getBody, property.Name);
            }

            getBody.InitLocals = true;
            getBody.OptimizeMacros();
        }

        if (property.SetMethod != null && property.SetMethod.Body != null)
        {
            var setBody = property.SetMethod.Body;
            setBody.SimplifyMacros();

            if (localValidationFlags.HasFlag(ValidationFlags.NonPublic) || property.SetMethod.IsPublic)
            {
                InjectPropertySetterGuard(setBody, property.Name, property.SetMethod.Parameters[0]);
            }

            setBody.InitLocals = true;
            setBody.OptimizeMacros();
        }
    }

    private void InjectPropertyGetterGuard(MethodBody getBody, string propertyName)
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
                // Duplicate the stack (this should be the return value)
                guardInstructions.Add(Instruction.Create(OpCodes.Dup));

                InstructionPatterns.CallDebugAssertInstructions(guardInstructions, String.Format(CultureInfo.InvariantCulture, "Return value of property '{0}' is null.", propertyName));
            }

            guardInstructions.AddRange(new Instruction[] {
                // Duplicate the stack (this should be the return value)
                Instruction.Create(OpCodes.Dup),

                // Branch if value on stack is true, not null or non-zero
                Instruction.Create(OpCodes.Brtrue_S, returnInstruction),

                // Clean up the stack since we're about to throw up.
                Instruction.Create(OpCodes.Pop),

                // Load the exception text onto the stack
                Instruction.Create(OpCodes.Ldstr, String.Format(CultureInfo.InvariantCulture, "Return value of property '{0}' is null.", propertyName)),

                // Load the InvalidOperationException onto the stack
                Instruction.Create(OpCodes.Newobj, ReferenceFinder.InvalidOperationExceptionConstructor),

                // Throw the top item of the stack
                Instruction.Create(OpCodes.Throw)
            });

            getBody.Instructions.Insert(ret, guardInstructions);
        }
    }

    private void InjectPropertySetterGuard(MethodBody setBody, string propertyName, ParameterDefinition valueParameter)
    {
        if (!valueParameter.MayNotBeNull())
            return;

        var guardInstructions = new List<Instruction>();

        var entry = setBody.Instructions.First();

        if (isDebug)
        {
            // Load the argument onto the stack
            guardInstructions.Add(Instruction.Create(OpCodes.Ldarg, valueParameter));

            InstructionPatterns.CallDebugAssertInstructions(guardInstructions, String.Format(CultureInfo.InvariantCulture, "Cannot set the value of property '{0}' to null.", propertyName));
        }

        guardInstructions.AddRange(new Instruction[] {
                // Load the argument onto the stack
                Instruction.Create(OpCodes.Ldarg, valueParameter),

                // Branch if value on stack is true, not null or non-zero
                Instruction.Create(OpCodes.Brtrue_S, entry),

                // Load the name of the argument onto the stack
                Instruction.Create(OpCodes.Ldstr, valueParameter.Name),

                // Load the exception text onto the stack
                Instruction.Create(OpCodes.Ldstr, String.Format(CultureInfo.InvariantCulture, "Cannot set the value of property '{0}' to null.", propertyName)),

                // Load the ArgumentNullException onto the stack
                Instruction.Create(OpCodes.Newobj, ReferenceFinder.ArgumentNullExceptionWithMessageConstructor),

                // Throw the top item of the stack
                Instruction.Create(OpCodes.Throw)
            });

        setBody.Instructions.Prepend(guardInstructions);
    }
}