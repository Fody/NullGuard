using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Anotar.Custom;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using Mono.Collections.Generic;
using NullGuard;

public class MethodProcessor
{
    private const string STR_OutParameterIsNull = "[NullGuard] Out parameter '{0}' is null.";
    private const string STR_ReturnValueOfMethodIsNull = "[NullGuard] Return value of method '{0}' is null.";
    private const string STR_IsNull = "[NullGuard] {0} is null.";

    private readonly bool isDebug;
    private readonly ValidationFlags validationFlags;

    public MethodProcessor(ValidationFlags validationFlags, bool isDebug)
    {
        this.validationFlags = validationFlags;
        this.isDebug = isDebug;
    }

    public void Process(MethodDefinition method)
    {
        try
        {
            if (method.IsGeneratedCode())
            {
                return;
            }
            InnerProcess(method);
        }
        catch (Exception exception)
        {
            LogTo.Error(exception, "An error occurred processing method '{0}'.", method.FullName);
        }
    }

    private void InnerProcess(MethodDefinition method)
    {
        var localValidationFlags = validationFlags;

        var attribute = method.DeclaringType.GetNullGuardAttribute();
        if (attribute != null)
        {
            localValidationFlags = (ValidationFlags)attribute.ConstructorArguments[0].Value;
        }

        if ((!localValidationFlags.HasFlag(ValidationFlags.NonPublic) && (!(method.IsPublic || method.IsExplicitInterfaceMethod()) || !method.DeclaringType.IsPublicOrNestedPublic())))
            return;

        var body = method.Body;

        var sequencePoint = body.Instructions.Select(i => i.SequencePoint).FirstOrDefault();

        body.SimplifyMacros();

        if (localValidationFlags.HasFlag(ValidationFlags.Arguments))
        {
            InjectMethodArgumentGuards(method, body, sequencePoint);
        }

        if (!method.IsAsyncStateMachine() &&
            !method.IsIteratorStateMachine())
        {
            InjectMethodReturnGuard(localValidationFlags, method, body, sequencePoint);
        }

        if (method.IsAsyncStateMachine())
        {
            var returnType = method.ReturnType;
            var genericReturnType = method.ReturnType as GenericInstanceType;
            if (genericReturnType != null && genericReturnType.HasGenericArguments && genericReturnType.Name.StartsWith("Task"))
                returnType = genericReturnType.GenericArguments[0];

            if (localValidationFlags.HasFlag(ValidationFlags.ReturnValues) &&
                !method.AllowsNullReturnValue() &&
                returnType.IsRefType() &&
                returnType.FullName != typeof(void).FullName)
            {
                InjectMethodReturnGuardAsync(body, String.Format(CultureInfo.InvariantCulture, STR_ReturnValueOfMethodIsNull, method.FullName), method.FullName);
            }
        }

        body.InitLocals = true;
        body.OptimizeMacros();
    }

    private void InjectMethodArgumentGuards(MethodDefinition method, MethodBody body, SequencePoint seqPoint)
    {
        var guardInstructions = new List<Instruction>();
        var usedArgs = new List<ParameterDefinition>();

        var entry = body.Instructions.First();
        var startIndex = body.Instructions.IndexOf(entry);

        if (method.IsConstructor)
        {
            var call = method.Body.Instructions.FirstOrDefault(i => i.OpCode == OpCodes.Call && ((MethodReference)i.Operand).Resolve().IsConstructor);
            if (call != null)
            {
                entry = call.Next;
                var argsInstructions = method.Body.Instructions.TakeWhile(i => i != entry).ToList();
                // find all args that are accessed in base/this calls
                usedArgs = argsInstructions
                            .Where(i => method.Parameters.Contains(i.Operand) &&
                                        argsInstructions.SkipWhile(op => op != i).Skip(1)
                                                        .Any(op => (op.OpCode == OpCodes.Call && op != call) || op.OpCode == OpCodes.Callvirt || op.OpCode == OpCodes.Ldfld))
                            .Select(i => i.Operand as ParameterDefinition).ToList();

            }
        }

        // this might be the same as the startIndex if this is not a constructor (or a special ctor)
        var ctorIndex = body.Instructions.IndexOf(entry);
        foreach (var parameter in method.Parameters.Reverse())
        {
            if (!parameter.MayNotBeNull())
                continue;

            if (method.IsSetter && parameter.Equals(method.GetPropertySetterValueParameter()))
                continue;

            if (CheckForExistingGuard(body.Instructions, parameter))
                continue;

            // if the arg is used in the base/this call, insert null checks before that call
            var index = usedArgs.Contains(parameter) ? startIndex : ctorIndex;
            entry = body.Instructions[index];
            var errorMessage = String.Format(CultureInfo.InvariantCulture, STR_IsNull, parameter.Name);

            guardInstructions.Clear();

            if (isDebug)
            {
                InstructionPatterns.LoadArgumentOntoStack(guardInstructions, parameter);

                InstructionPatterns.CallDebugAssertInstructions(guardInstructions, errorMessage);
            }

            InstructionPatterns.LoadArgumentOntoStack(guardInstructions, parameter);

            InstructionPatterns.IfNull(guardInstructions, entry, i =>
            {
                InstructionPatterns.LoadArgumentNullException(i, parameter.Name, errorMessage);

                // Throw the top item off the stack
                i.Add(Instruction.Create(OpCodes.Throw));
            });

            guardInstructions[0].HideLineFromDebugger(seqPoint);
            body.Instructions.Insert(index, guardInstructions);
            // if this is a ctor and instructions were inserted before the base/this call,
            // bump its position
            if (index < ctorIndex)
                ctorIndex += guardInstructions.Count;
        }
    }

    private void InjectMethodReturnGuard(ValidationFlags localValidationFlags, MethodDefinition method, MethodBody body, SequencePoint seqPoint)
    {
        var guardInstructions = new List<Instruction>();

        var returnPoints = body.Instructions
                .Select((o, ix) => new { o, ix })
                .Where(a => a.o.OpCode == OpCodes.Ret)
                .Select(a => a.ix)
                .OrderByDescending(ix => ix);

        foreach (var ret in returnPoints)
        {
            if (localValidationFlags.HasFlag(ValidationFlags.ReturnValues) &&
                !method.AllowsNullReturnValue() &&
                method.ReturnType.IsRefType() &&
                method.ReturnType.FullName != typeof(void).FullName &&
                !method.IsGetter)
            {
                var errorMessage = String.Format(CultureInfo.InvariantCulture, STR_ReturnValueOfMethodIsNull, method.FullName);
                AddReturnNullGuard(body.Instructions, seqPoint, ret, method.ReturnType, errorMessage, Instruction.Create(OpCodes.Throw));
            }

            if (localValidationFlags.HasFlag(ValidationFlags.Arguments))
            {
                foreach (var parameter in method.Parameters.Reverse())
                {
                    // This is no longer the return instruction location, but it is where we want to jump to.
                    var returnInstruction = body.Instructions[ret];

                    if (localValidationFlags.HasFlag(ValidationFlags.OutValues) &&
                        parameter.IsOut &&
                        parameter.ParameterType.IsRefType() &&
                        !parameter.AllowsNull())
                    {
                        var errorMessage = String.Format(CultureInfo.InvariantCulture, STR_OutParameterIsNull, parameter.Name);

                        guardInstructions.Clear();

                        if (isDebug)
                        {
                            InstructionPatterns.LoadArgumentOntoStack(guardInstructions, parameter);

                            InstructionPatterns.CallDebugAssertInstructions(guardInstructions, errorMessage);
                        }

                        InstructionPatterns.LoadArgumentOntoStack(guardInstructions, parameter);

                        InstructionPatterns.IfNull(guardInstructions, returnInstruction, i =>
                        {
                            InstructionPatterns.LoadInvalidOperationException(i, errorMessage);

                            // Throw the top item off the stack
                            i.Add(Instruction.Create(OpCodes.Throw));
                        });

                        guardInstructions[0].HideLineFromDebugger(seqPoint);

                        body.Instructions.Insert(ret, guardInstructions);
                    }
                }
            }
        }
    }

    private void InjectMethodReturnGuardAsync(MethodBody body, string errorMessage, string methodName)
    {
        foreach (var local in body.Variables)
        {
            if (!local.VariableType.IsValueType ||
                !local.VariableType.Resolve().IsGeneratedCode() ||
                !local.VariableType.Resolve().IsIAsyncStateMachine())
                continue;

            var stateMachine = local.VariableType.Resolve();
            var moveNext = stateMachine.Methods.First(x => x.Name == "MoveNext");

            InjectMethodReturnGuardAsyncIntoMoveNext(moveNext, errorMessage, methodName);
        }
    }

    private void InjectMethodReturnGuardAsyncIntoMoveNext(MethodDefinition method, string errorMessage, string methodName)
    {
        method.Body.SimplifyMacros();

        var setExceptionInstruction = method.Body.Instructions
            .FirstOrDefault(x => x.OpCode == OpCodes.Call && IsSetExceptionMethod(x.Operand as MethodReference));

        if (setExceptionInstruction == null)
        {
            // Mono's broken compiler doen't add a SetException call if there's no await.
            // Bail out since we're not about to rewrite the whole method to fix this. :/
            LogTo.Warning("Cannot add guards to {0} as the method contains no await keyword.", methodName);
            return;
        }

        var setExceptionMethod = (MethodReference)setExceptionInstruction.Operand;

        var returnPoints = method.Body.Instructions
                .Select((o, ix) => new { o, ix })
                .Where(a => a.o.OpCode == OpCodes.Call && IsSetResultMethod(a.o.Operand as MethodReference))
                .Select(a => a.ix)
                .OrderByDescending(ix => ix);

        foreach (var ret in returnPoints)
        {
            AddReturnNullGuard(method.Body.Instructions, null, ret, method.ReturnType, errorMessage, Instruction.Create(OpCodes.Call, setExceptionMethod), Instruction.Create(OpCodes.Ret));
        }

        method.Body.OptimizeMacros();
    }

    private void AddReturnNullGuard(Collection<Instruction> instructions, SequencePoint seqPoint, int ret, TypeReference returnType, string errorMessage, params Instruction[] finalInstructions)
    {
        var returnInstruction = instructions[ret];

        var guardInstructions = new List<Instruction>();

        if (isDebug)
        {
            InstructionPatterns.DuplicateReturnValue(guardInstructions, returnType);

            InstructionPatterns.CallDebugAssertInstructions(guardInstructions, errorMessage);
        }

        InstructionPatterns.DuplicateReturnValue(guardInstructions, returnType);

        InstructionPatterns.IfNull(guardInstructions, returnInstruction, i =>
        {
            // Clean up the stack since we're about to throw up.
            i.Add(Instruction.Create(OpCodes.Pop));

            InstructionPatterns.LoadInvalidOperationException(i, errorMessage);

            i.AddRange(finalInstructions);
        });

        guardInstructions[0].HideLineFromDebugger(seqPoint);

        instructions.Insert(ret, guardInstructions);
    }

    private static bool CheckForExistingGuard(Collection<Instruction> instructions, ParameterDefinition parameter)
    {
        for (var i = 1; i < instructions.Count - 1; i++)
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

    private static bool IsSetResultMethod(MethodReference methodReference)
    {
        return
            methodReference != null &&
            methodReference.Name == "SetResult" &&
            methodReference.Parameters.Count == 1 &&
            methodReference.DeclaringType.FullName.StartsWith("System.Runtime.CompilerServices.AsyncTaskMethodBuilder");
    }

    private static bool IsSetExceptionMethod(MethodReference methodReference)
    {
        return
            methodReference != null &&
            methodReference.Name == "SetException" &&
            methodReference.Parameters.Count == 1 &&
            methodReference.DeclaringType.FullName.StartsWith("System.Runtime.CompilerServices.AsyncTaskMethodBuilder");
    }
}