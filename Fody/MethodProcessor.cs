using System;
using System.Globalization;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using Mono.Collections.Generic;
using NullGuard;

public class MethodProcessor
{
    public ModuleWeaver ModuleWeaver;
    public MethodDefinition Method;
    private MethodBody body;

    public void Process()
    {
        try
        {
            InnerProcess();
        }
        catch (Exception exception)
        {
            throw new WeavingException(string.Format("An error occurred processing method '{0}'.", Method.FullName), exception);
        }
    }

    private void InnerProcess()
    {
        var validationFlags = ModuleWeaver.ValidationFlags;

        var attribute = Method.DeclaringType.GetNullGuardAttribute();
        if (attribute != null)
        {
            validationFlags = (ValidationFlags)attribute.ConstructorArguments[0].Value;
        }

        if ((!validationFlags.HasFlag(ValidationFlags.NonPublic) && !Method.IsPublic)
            || Method.IsProperty()
            )
            return;

        body = Method.Body;
        body.SimplifyMacros();

        if (validationFlags.HasFlag(ValidationFlags.Arguments))
        {
            InjectMethodArgumentGuards(validationFlags);
        }

        if (!Method.IsAsyncStateMachine() &&
            !Method.IsIteratorStateMachine())
        {
            InjectMethodReturnGuard(validationFlags);
        }

        body.InitLocals = true;
        body.OptimizeMacros();
    }

    private void InjectMethodArgumentGuards(ValidationFlags validationFlags)
    {
        foreach (var parameter in Method.Parameters.Reverse())
        {
            if (!parameter.MayNotBeNull())
                continue;

            if (CheckForExistingGuard(body.Instructions, parameter))
                continue;

            var entry = body.Instructions.First();

            body.Instructions.Prepend(

                // Load the argument onto the stack
                Instruction.Create(OpCodes.Ldarg, parameter),

                // Branch if value on stack is true, not null or non-zero
                Instruction.Create(OpCodes.Brtrue_S, entry),

                // Load the name of the argument onto the stack
                Instruction.Create(OpCodes.Ldstr, parameter.Name),

                // Load the ArgumentNullException onto the stack
                Instruction.Create(OpCodes.Newobj, ModuleWeaver.ArgumentNullExceptionConstructor),

                // Throw the top item of the stack
                Instruction.Create(OpCodes.Throw)
                );
        }
    }

    private void InjectMethodReturnGuard(ValidationFlags validationFlags)
    {
        var returnPoints = body.Instructions
                .Select((o, ix) => new { o, ix })
                .Where(a => a.o.OpCode == OpCodes.Ret)
                .Select(a => a.ix)
                .OrderByDescending(ix => ix);

        foreach (var ret in returnPoints)
        {
            var returnInstruction = body.Instructions[ret];

            if (validationFlags.HasFlag(ValidationFlags.ReturnValues) &&
                !Method.MethodReturnType.AllowsNull() &&
                Method.ReturnType.IsRefType() &&
                Method.ReturnType.FullName != typeof(void).FullName)
            {
                body.Instructions.Insert(ret,

                    // Duplicate the stack (this should be the return value)
                    Instruction.Create(OpCodes.Dup),

                    // Branch if value on stack is true, not null or non-zero
                    Instruction.Create(OpCodes.Brtrue_S, returnInstruction),

                    // Clean up the stack since we're about to throw up.
                    Instruction.Create(OpCodes.Pop),

                    // Load the exception text onto the stack
                    Instruction.Create(OpCodes.Ldstr, String.Format(CultureInfo.InvariantCulture, "Return value of method '{0}' is null.", Method.Name)),

                    // Load the InvalidOperationException onto the stack
                    Instruction.Create(OpCodes.Newobj, ModuleWeaver.InvalidOperationExceptionConstructor),

                    // Throw the top item of the stack
                    Instruction.Create(OpCodes.Throw)
                    );
            }

            if (validationFlags.HasFlag(ValidationFlags.Arguments))
            {
                foreach (var parameter in Method.Parameters.Reverse())
                {
                    // This is no longer the return instruction location, but it is where we want to jump to.
                    returnInstruction = body.Instructions[ret];

                    if (validationFlags.HasFlag(ValidationFlags.OutValues) &&
                        parameter.IsOut &&
                        parameter.ParameterType.IsRefType() &&
                        !parameter.ParameterType.GetElementType().IsGenericParameter)
                    {
                        body.Instructions.Insert(ret,

                            // Load the out parameter onto the stack
                            Instruction.Create(OpCodes.Ldarg, parameter),

                            // Loads an object reference onto the stack
                            Instruction.Create(OpCodes.Ldind_Ref),

                            // Branch if value on stack is true, not null or non-zero
                            Instruction.Create(OpCodes.Brtrue_S, returnInstruction),

                            // Load the exception text onto the stack
                            Instruction.Create(OpCodes.Ldstr, String.Format(CultureInfo.InvariantCulture, "Out parameter '{0}' is null.", parameter.Name)),

                            // Load the InvalidOperationException onto the stack
                            Instruction.Create(OpCodes.Newobj, ModuleWeaver.InvalidOperationExceptionConstructor),

                            // Throw the top item of the stack
                            Instruction.Create(OpCodes.Throw)
                            );
                    }
                }
            }
        }
    }

    private bool CheckForExistingGuard(Collection<Instruction> instructions, ParameterDefinition parameter)
    {
        for (int i = 1; i < instructions.Count - 1; i++)
        {
            if (instructions[i].OpCode == OpCodes.Newobj)
            {
                var newObjectMethodRef = instructions[i].Operand as MethodReference;

                // Check the 2 most common guards
                // throw new ArgumentNullException("x");
                // throw new ArgumentException("some message", "x");

                if (newObjectMethodRef != null &&
                    (newObjectMethodRef.FullName == ModuleWeaver.ArgumentNullExceptionConstructor.FullName ||
                    newObjectMethodRef.FullName == ModuleWeaver.ArgumentExceptionConstructor.FullName) &&
                    instructions[i - 1].OpCode == OpCodes.Ldstr &&
                    (string)(instructions[i - 1].Operand) == parameter.Name &&
                    instructions[i + 1].OpCode == OpCodes.Throw)
                    return true;
            }
        }

        return false;
    }
}