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
    public ModuleWeaver ModuleWeaver;
    public PropertyDefinition Property;
    public bool IsDebug;
    MethodBody getBody;
    MethodBody setBody;

    public void Process()
    {
        try
        {
            InnerProcess();
        }
        catch (Exception exception)
        {
            throw new WeavingException(string.Format("An error occurred processing property '{0}'", Property.FullName), exception);
        }
    }

    void InnerProcess()
    {
        if (!Property.PropertyType.IsRefType())
        {
            return;
        }
        var validationFlags = ModuleWeaver.ValidationFlags;

        var attribute = Property.DeclaringType.GetNullGuardAttribute();
        if (attribute != null)
        {
            validationFlags = (ValidationFlags)attribute.ConstructorArguments[0].Value;
        }

        if (!validationFlags.HasFlag(ValidationFlags.Properties)) return;

        if (Property.AllowsNull())
        {
            return;
        }

        if (Property.GetMethod != null && Property.GetMethod.Body != null)
        {
            getBody = Property.GetMethod.Body;
            getBody.SimplifyMacros();

            if ((validationFlags.HasFlag(ValidationFlags.NonPublic) || Property.GetMethod.IsPublic) &&
                !Property.GetMethod.MethodReturnType.AllowsNull()
               )
            {
                InjectPropertyGetterGuard();
            }

            getBody.InitLocals = true;
            getBody.OptimizeMacros();
        }

        if (Property.SetMethod != null && Property.SetMethod.Body != null)
        {
            setBody = Property.SetMethod.Body;
            setBody.SimplifyMacros();

            if (validationFlags.HasFlag(ValidationFlags.NonPublic) || Property.SetMethod.IsPublic)
            {
                InjectPropertySetterGuard();
            }

            setBody.InitLocals = true;
            setBody.OptimizeMacros();
        }
    }

    void InjectPropertyGetterGuard()
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

            if (IsDebug)
            {
                // Duplicate the stack (this should be the return value)
                guardInstructions.Add(Instruction.Create(OpCodes.Dup));

                guardInstructions.AddRange(ModuleWeaver.CallDebugAssertInstructions(String.Format(CultureInfo.InvariantCulture, "Return value of property '{0}' is null.", Property.Name)));
            }

            guardInstructions.AddRange(new Instruction[] {

                // Duplicate the stack (this should be the return value)
                Instruction.Create(OpCodes.Dup),

                // Branch if value on stack is true, not null or non-zero
                Instruction.Create(OpCodes.Brtrue_S, returnInstruction),

                // Clean up the stack since we're about to throw up.
                Instruction.Create(OpCodes.Pop),

                // Load the exception text onto the stack
                Instruction.Create(OpCodes.Ldstr, String.Format(CultureInfo.InvariantCulture, "Return value of property '{0}' is null.", Property.Name)),

                // Load the InvalidOperationException onto the stack
                Instruction.Create(OpCodes.Newobj, ModuleWeaver.InvalidOperationExceptionConstructor),

                // Throw the top item of the stack
                Instruction.Create(OpCodes.Throw)
            });

            getBody.Instructions.Insert(ret, guardInstructions);
        }
    }

    void InjectPropertySetterGuard()
    {
        var guardInstructions = new List<Instruction>();

        var parameter = Property.SetMethod.Parameters[0]; // The Value parameter

        if (parameter.MayNotBeNull())
        {
            var entry = setBody.Instructions.First();

            if (IsDebug)
            {
                // Load the argument onto the stack
                guardInstructions.Add(Instruction.Create(OpCodes.Ldarg, parameter));

                guardInstructions.AddRange(ModuleWeaver.CallDebugAssertInstructions(String.Format(CultureInfo.InvariantCulture, "Cannot set the value of property '{0}' to null.", Property.Name)));
            }

            guardInstructions.AddRange(new Instruction[] {

                // Load the argument onto the stack
                Instruction.Create(OpCodes.Ldarg, parameter),

                // Branch if value on stack is true, not null or non-zero
                Instruction.Create(OpCodes.Brtrue_S, entry),

                // Load the name of the argument onto the stack
                Instruction.Create(OpCodes.Ldstr, parameter.Name),

                // Load the exception text onto the stack
                Instruction.Create(OpCodes.Ldstr, String.Format(CultureInfo.InvariantCulture, "Cannot set the value of property '{0}' to null.", Property.Name)),

                // Load the ArgumentNullException onto the stack
                Instruction.Create(OpCodes.Newobj, ModuleWeaver.ArgumentNullExceptionWithMessageConstructor),

                // Throw the top item of the stack
                Instruction.Create(OpCodes.Throw)
            });

            setBody.Instructions.Prepend(guardInstructions);
        }
    }
}