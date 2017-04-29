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
    const string OutParameterIsNull = "[NullGuard] Out parameter '{0}' is null.";
    const string ReturnValueOfMethodIsNull = "[NullGuard] Return value of method '{0}' is null.";
    const string IsNull = "[NullGuard] {0} is null.";

    bool isDebug;
    ValidationFlags validationFlags;

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

    void InnerProcess(MethodDefinition method)
    {
        var localValidationFlags = validationFlags;

        var attribute = method.DeclaringType.GetNullGuardAttribute();
        if (attribute != null)
        {
            localValidationFlags = (ValidationFlags)attribute.ConstructorArguments[0].Value;
        }

        if (!localValidationFlags.HasFlag(ValidationFlags.NonPublic) && (!(method.IsPublic || method.IsExplicitInterfaceMethod()) || !method.DeclaringType.IsPublicOrNestedPublic()))
            return;

        var body = method.Body;

        var doc = method.DebugInformation.SequencePoints.FirstOrDefault()?.Document;

        body.SimplifyMacros();

        if (localValidationFlags.HasFlag(ValidationFlags.Arguments))
        {
            InjectMethodArgumentGuards(method, body, doc);
        }

        if (!method.IsAsyncStateMachine() &&
            !method.IsIteratorStateMachine())
        {
            InjectMethodReturnGuard(localValidationFlags, method, body, doc);
        }

        if (method.IsAsyncStateMachine())
        {
            var returnType = method.ReturnType;
            var genericReturnType = method.ReturnType as GenericInstanceType;
            if (genericReturnType != null && genericReturnType.HasGenericArguments && genericReturnType.Name.StartsWith("Task"))
            {
                returnType = genericReturnType.GenericArguments[0];
            }

            if (localValidationFlags.HasFlag(ValidationFlags.ReturnValues) &&
                !method.AllowsNullReturnValue() &&
                returnType.IsRefType() &&
                returnType.FullName != typeof(void).FullName)
            {
                InjectMethodReturnGuardAsync(body, string.Format(CultureInfo.InvariantCulture, ReturnValueOfMethodIsNull, method.FullName), method.FullName);
            }
        }

        body.InitLocals = true;
        body.OptimizeMacros();
    }

    void InjectMethodArgumentGuards(MethodDefinition method, MethodBody body, Document doc)
    {
        var guardInstructions = new List<Instruction>();

        foreach (var parameter in method.Parameters.Reverse())
        {
            if (!parameter.MayNotBeNull())
                continue;

            if (method.IsSetter && parameter.Equals(method.GetPropertySetterValueParameter()))
                continue;

            if (CheckForExistingGuard(body.Instructions, parameter))
                continue;

            var entry = body.Instructions.First();
            var errorMessage = string.Format(CultureInfo.InvariantCulture, IsNull, parameter.Name);

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

            method.HideLineFromDebugger(guardInstructions[0], doc);

            body.Instructions.Prepend(guardInstructions);
        }
    }

    void InjectMethodReturnGuard(ValidationFlags localValidationFlags, MethodDefinition method, MethodBody body, Document doc)
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
                var errorMessage = string.Format(CultureInfo.InvariantCulture, ReturnValueOfMethodIsNull, method.FullName);
                AddReturnNullGuard(method, doc, ret, method.ReturnType, errorMessage, Instruction.Create(OpCodes.Throw));
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
                        var errorMessage = string.Format(CultureInfo.InvariantCulture, OutParameterIsNull, parameter.Name);

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

                        method.HideLineFromDebugger(guardInstructions[0], doc);

                        body.InsertAtMethodReturnPoint(ret, guardInstructions);
                    }
                }
            }
        }
    }

    void InjectMethodReturnGuardAsync(MethodBody body, string errorMessage, string methodName)
    {
        foreach (var local in body.Variables)
        {
            var resolve = local.VariableType.Resolve();
            if (!resolve.IsGeneratedCode() ||
                !resolve.IsIAsyncStateMachine())
            {
                continue;
            }

            var moveNext = resolve.Methods.First(x => x.Name == "MoveNext");

            InjectMethodReturnGuardAsyncIntoMoveNext(moveNext, errorMessage, methodName);
        }
    }

    void InjectMethodReturnGuardAsyncIntoMoveNext(MethodDefinition method, string errorMessage, string methodName)
    {
        method.Body.SimplifyMacros();

        var setExceptionInstruction = method.Body.Instructions
            .FirstOrDefault(x => x.OpCode == OpCodes.Call && IsSetExceptionMethod(x.Operand as MethodReference));

        if (setExceptionInstruction == null)
        {
            // Mono's broken compiler doesn't add a SetException call if there's no await.
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
            AddReturnNullGuard(method, null, ret, method.ReturnType, errorMessage, Instruction.Create(OpCodes.Call, setExceptionMethod), Instruction.Create(OpCodes.Ret));
        }

        method.Body.OptimizeMacros();
    }

    void AddReturnNullGuard(MethodDefinition methodDefinition, Document doc, int ret, TypeReference returnType, string errorMessage, params Instruction[] finalInstructions)
    {
        var returnInstruction = methodDefinition.Body.Instructions[ret];

        var guardInstructions = new List<Instruction>();

        if (isDebug)
        {
            InstructionPatterns.DuplicateReturnValue(guardInstructions, returnType);

            InstructionPatterns.CallDebugAssertInstructions(guardInstructions, errorMessage);
        }

        InstructionPatterns.DuplicateReturnValue(guardInstructions, returnType);

        InstructionPatterns.IfNull(guardInstructions, returnInstruction, i =>
        {
            // Clean up the stack (important if finalInstructions doesn't throw, e.g. for async methods):
            i.Add(Instruction.Create(OpCodes.Pop));

            InstructionPatterns.LoadInvalidOperationException(i, errorMessage);

            i.AddRange(finalInstructions);
        });

        methodDefinition.HideLineFromDebugger(guardInstructions[0], doc);

        methodDefinition.Body.InsertAtMethodReturnPoint(ret, guardInstructions);
    }

    static bool CheckForExistingGuard(Collection<Instruction> instructions, ParameterDefinition parameter)
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
                    (string)instructions[i - 1].Operand == parameter.Name)
                    return true;

                // Checks for throw new ArgumentNullException("x", "some message");
                if (newObjectMethodRef.FullName == ReferenceFinder.ArgumentNullExceptionWithMessageConstructor.FullName &&
                    i > 1 &&
                    instructions[i - 2].OpCode == OpCodes.Ldstr &&
                    (string)instructions[i - 2].Operand == parameter.Name)
                    return true;
            }
        }

        return false;
    }

    static bool IsSetResultMethod(MethodReference methodReference)
    {
        return
            methodReference != null &&
            methodReference.Name == "SetResult" &&
            methodReference.Parameters.Count == 1 &&
            methodReference.DeclaringType.FullName.StartsWith("System.Runtime.CompilerServices.AsyncTaskMethodBuilder");
    }

    static bool IsSetExceptionMethod(MethodReference methodReference)
    {
        return
            methodReference != null &&
            methodReference.Name == "SetException" &&
            methodReference.Parameters.Count == 1 &&
            methodReference.DeclaringType.FullName.StartsWith("System.Runtime.CompilerServices.AsyncTaskMethodBuilder");
    }
}