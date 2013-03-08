using System;
using System.Collections.Generic;
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
    public bool IsDebug;
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

        if (Method.IsAsyncStateMachine())
        {
            InjectMethodReturnGuardAsync(validationFlags);
        }

        body.InitLocals = true;
        body.OptimizeMacros();
    }

    private void InjectMethodArgumentGuards(ValidationFlags validationFlags)
    {
        var guardInstructions = new List<Instruction>();

        foreach (var parameter in Method.Parameters.Reverse())
        {
            if (!parameter.MayNotBeNull())
                continue;

            if (CheckForExistingGuard(body.Instructions, parameter))
                continue;

            var entry = body.Instructions.First();

            guardInstructions.Clear();

            if (IsDebug)
            {
                LoadArgumentOntoStack(guardInstructions, parameter);

                guardInstructions.AddRange(ModuleWeaver.CallDebugAssertInstructions(parameter.Name + " is null."));
            }

            LoadArgumentOntoStack(guardInstructions, parameter);

            guardInstructions.AddRange(new Instruction[] {
                // Branch if value on stack is true, not null or non-zero
                Instruction.Create(OpCodes.Brtrue_S, entry),

                // Load the name of the argument onto the stack
                Instruction.Create(OpCodes.Ldstr, parameter.Name),

                // Load the ArgumentNullException onto the stack
                Instruction.Create(OpCodes.Newobj, ReferenceFinder.ArgumentNullExceptionConstructor),

                // Throw the top item of the stack
                Instruction.Create(OpCodes.Throw)
            });

            body.Instructions.Prepend(guardInstructions);
        }
    }

    private void InjectMethodReturnGuard(ValidationFlags validationFlags)
    {
        var guardInstructions = new List<Instruction>();

        var returnPoints = body.Instructions
                .Select((o, ix) => new { o, ix })
                .Where(a => a.o.OpCode == OpCodes.Ret)
                .Select(a => a.ix)
                .OrderByDescending(ix => ix);

        var isGenericReturn = Method.ReturnType.GetElementType().IsGenericParameter;

        foreach (var ret in returnPoints)
        {
            var returnInstruction = body.Instructions[ret];

            if (validationFlags.HasFlag(ValidationFlags.ReturnValues) &&
                !Method.MethodReturnType.AllowsNull() &&
                Method.ReturnType.IsRefType() &&
                Method.ReturnType.FullName != typeof(void).FullName)
            {
                AddReturnNullGuard(body.Instructions, isGenericReturn, ret, Instruction.Create(OpCodes.Throw));
            }

            if (validationFlags.HasFlag(ValidationFlags.Arguments))
            {
                foreach (var parameter in Method.Parameters.Reverse())
                {
                    // This is no longer the return instruction location, but it is where we want to jump to.
                    returnInstruction = body.Instructions[ret];

                    if (validationFlags.HasFlag(ValidationFlags.OutValues) &&
                        parameter.IsOut &&
                        parameter.ParameterType.IsRefType())
                    {
                        guardInstructions.Clear();

                        if (IsDebug)
                        {
                            LoadArgumentOntoStack(guardInstructions, parameter);

                            guardInstructions.AddRange(ModuleWeaver.CallDebugAssertInstructions(String.Format(CultureInfo.InvariantCulture, "Out parameter '{0}' is null.", parameter.Name)));
                        }

                        LoadArgumentOntoStack(guardInstructions, parameter);

                        guardInstructions.AddRange(new Instruction[] {
                            // Branch if value on stack is true, not null or non-zero
                            Instruction.Create(OpCodes.Brtrue_S, returnInstruction),

                            // Load the exception text onto the stack
                            Instruction.Create(OpCodes.Ldstr, String.Format(CultureInfo.InvariantCulture, "Out parameter '{0}' is null.", parameter.Name)),

                            // Load the InvalidOperationException onto the stack
                            Instruction.Create(OpCodes.Newobj, ReferenceFinder.InvalidOperationExceptionConstructor),

                            // Throw the top item of the stack
                            Instruction.Create(OpCodes.Throw)
                        });

                        body.Instructions.Insert(ret, guardInstructions);
                    }
                }
            }
        }
    }

    private void InjectMethodReturnGuardAsync(ValidationFlags validationFlags)
    {
        var returnType = Method.ReturnType;
        if (Method.ReturnType.HasGenericParameters && Method.ReturnType.Name.StartsWith("Task"))
            returnType = Method.ReturnType.GenericParameters[0];

        if (validationFlags.HasFlag(ValidationFlags.ReturnValues) &&
            !Method.MethodReturnType.AllowsNull() &&
            returnType.IsRefType() &&
            returnType.FullName != typeof(void).FullName)
        {
            foreach (var local in Method.Body.Variables)
            {
                if (!local.VariableType.IsValueType ||
                    !local.VariableType.Resolve().IsCompilerGenerated() ||
                    !local.VariableType.Resolve().IsIAsyncStateMachine())
                    continue;

                var stateMachine = local.VariableType.Resolve();
                var moveNext = stateMachine.Methods.First(x => x.Name == "MoveNext");

                InjectMethodReturnGuardAsync(moveNext);
            }
        }
    }

    private void InjectMethodReturnGuardAsync(MethodDefinition method)
    {
        var guardInstructions = new List<Instruction>();

        method.Body.SimplifyMacros();

        var setExceptionMethod = (MethodReference)method.Body.Instructions
            .First(x => x.OpCode == OpCodes.Call && IsSetExceptionMethod(x.Operand as MethodReference))
            .Operand;

        var returnPoints = method.Body.Instructions
                .Select((o, ix) => new { o, ix })
                .Where(a => a.o.OpCode == OpCodes.Call && IsSetResultMethod(a.o.Operand as MethodReference))
                .Select(a => a.ix)
                .OrderByDescending(ix => ix);

        foreach (var ret in returnPoints)
        {
            AddReturnNullGuard(method.Body.Instructions, false, ret,
                Instruction.Create(OpCodes.Call, setExceptionMethod),
                Instruction.Create(OpCodes.Ret));
        }

        method.Body.OptimizeMacros();
    }

    private void AddReturnNullGuard(Collection<Instruction> instructions, bool isGenericReturn, int ret, params Instruction[] finalInstructions)
    {
        var returnInstruction = instructions[ret];

        var guardInstructions = new List<Instruction>();

        if (IsDebug)
        {
            DuplicateReturnValue(guardInstructions, isGenericReturn, Method.ReturnType);

            guardInstructions.AddRange(ModuleWeaver.CallDebugAssertInstructions(String.Format(CultureInfo.InvariantCulture, "Return value of method '{0}' is null.", Method.Name)));
        }

        DuplicateReturnValue(guardInstructions, isGenericReturn, Method.ReturnType);

        guardInstructions.AddRange(new Instruction[] {
            // Branch if value on stack is true, not null or non-zero
            Instruction.Create(OpCodes.Brtrue_S, returnInstruction),

            // Clean up the stack since we're about to throw up.
            Instruction.Create(OpCodes.Pop),

            // Load the exception text onto the stack
            Instruction.Create(OpCodes.Ldstr, String.Format(CultureInfo.InvariantCulture, "Return value of method '{0}' is null.", Method.Name)),

            // Load the InvalidOperationException onto the stack
            Instruction.Create(OpCodes.Newobj, ReferenceFinder.InvalidOperationExceptionConstructor)
        });

        guardInstructions.AddRange(finalInstructions);

        instructions.Insert(ret, guardInstructions);
    }

    private bool CheckForExistingGuard(Collection<Instruction> instructions, ParameterDefinition parameter)
    {
        for (int i = 1; i < instructions.Count - 1; i++)
        {
            if (instructions[i].OpCode == OpCodes.Newobj)
            {
                var newObjectMethodRef = instructions[i].Operand as MethodReference;

                if (newObjectMethodRef == null || instructions[i + 1].OpCode != OpCodes.Throw)
                    continue;

                // Checks for throw new ArgumentNullException("x");
                if (newObjectMethodRef.FullName == ReferenceFinder.ArgumentNullExceptionConstructor.FullName &&
                    instructions[i - 1].OpCode == OpCodes.Ldstr &&
                    (string)(instructions[i - 1].Operand) == parameter.Name)
                    return true;

                // Checks for throw new ArgumentNullException("x", "some message");
                if (newObjectMethodRef.FullName == ReferenceFinder.ArgumentNullExceptionWithMessageConstructor.FullName &&
                    i > 1 &&
                    instructions[i - 2].OpCode == OpCodes.Ldstr &&
                    (string)(instructions[i - 2].Operand) == parameter.Name)
                    return true;
            }
        }

        return false;
    }

    private static void LoadArgumentOntoStack(List<Instruction> guardInstructions, ParameterDefinition parameter)
    {
        // Load the argument onto the stack
        guardInstructions.Add(Instruction.Create(OpCodes.Ldarg, parameter));

        var elementType = parameter.ParameterType.GetElementType();

        if (parameter.ParameterType.IsByReference)
        {
            if (elementType.IsGenericParameter)
            {
                // Loads an object reference onto the stack
                guardInstructions.Add(Instruction.Create(OpCodes.Ldobj, elementType));

                // Box the type to an object
                guardInstructions.Add(Instruction.Create(OpCodes.Box, elementType));
            }
            else
            {
                // Loads an object reference onto the stack
                guardInstructions.Add(Instruction.Create(OpCodes.Ldind_Ref));
            }
        }
        else if (elementType.IsGenericParameter)
        {
            // Box the type to an object
            guardInstructions.Add(Instruction.Create(OpCodes.Box, parameter.ParameterType));
        }
    }

    private static void DuplicateReturnValue(List<Instruction> guardInstructions, bool isGenericReturn, TypeReference methodReturnType)
    {
        // Duplicate the stack (this should be the return value)
        guardInstructions.Add(Instruction.Create(OpCodes.Dup));

        if (isGenericReturn)
        {
            // Generic parameters must be boxed before access
            guardInstructions.Add(Instruction.Create(OpCodes.Box, methodReturnType));
        }
    }

    public static bool IsSetResultMethod(MethodReference methodReference)
    {
        return
            methodReference != null &&
            methodReference.Name == "SetResult" &&
            methodReference.Parameters.Count == 1 &&
            methodReference.DeclaringType.FullName.StartsWith("System.Runtime.CompilerServices.AsyncTaskMethodBuilder");
    }

    public static bool IsSetExceptionMethod(MethodReference methodReference)
    {
        return
            methodReference != null &&
            methodReference.Name == "SetException" &&
            methodReference.Parameters.Count == 1 &&
            methodReference.DeclaringType.FullName.StartsWith("System.Runtime.CompilerServices.AsyncTaskMethodBuilder");
    }
}