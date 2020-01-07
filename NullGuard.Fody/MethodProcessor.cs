using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using Mono.Collections.Generic;
using NullGuard;

public partial class ModuleWeaver
{
    const string ReturnValueOfMethodIsNull = "[NullGuard] Return value of method '{0}' is null.";

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
            throw new Exception($"An error occurred processing method '{method.FullName}'.", exception);
        }
    }

    void InnerProcess(MethodDefinition method)
    {
        var localValidationFlags = ValidationFlags;

        var attribute = method.DeclaringType.GetNullGuardAttribute();
        if (attribute != null)
        {
            localValidationFlags = (ValidationFlags)attribute.ConstructorArguments[0].Value;
        }

        if (!localValidationFlags.HasFlag(ValidationFlags.NonPublic)
            && !(method.IsPublic && method.DeclaringType.IsPublicOrNestedPublic()
            || method.IsOverrideOrImplementationOfPublicMember()))
        {
            return;
        }

        var body = method.Body;

        body.SimplifyMacros();

        if (localValidationFlags.HasFlag(ValidationFlags.Arguments))
        {
            InjectMethodArgumentGuards(method, body);
        }

        if (!method.IsAsyncStateMachine() &&
            !method.IsIteratorStateMachine())
        {
            InjectMethodReturnGuard(localValidationFlags, method, body);
        }

        if (method.IsAsyncStateMachine())
        {
            var returnType = method.ReturnType;
            if (method.ReturnType is GenericInstanceType genericReturnType &&
                genericReturnType.HasGenericArguments &&
                genericReturnType.Name.StartsWith("Task"))
            {
                returnType = genericReturnType.GenericArguments[0];
            }

            if (localValidationFlags.HasFlag(ValidationFlags.ReturnValues) &&
                !nullabilityAnalyzer.AllowsNullReturnValue(method) &&
                returnType.IsRefType() &&
                returnType.FullName != typeof(void).FullName)
            {
                InjectMethodReturnGuardAsync(body, string.Format(CultureInfo.InvariantCulture, ReturnValueOfMethodIsNull, method.FullName), method.FullName);
            }
        }

        method.UpdateDebugInfo();
        body.InitLocals = true;
        body.OptimizeMacros();
    }

    void InjectMethodArgumentGuards(MethodDefinition method, MethodBody body)
    {
        foreach (var parameter in method.Parameters.Reverse())
        {
            if (!parameter.MayNotBeNull())
            {
                continue;
            }

            if (method.IsSetter && parameter.Equals(method.GetPropertySetterValueParameter()))
            {
                continue;
            }

            if (nullabilityAnalyzer.AllowsNull(parameter, method))
            {
                continue;
            }

            if (CheckForExistingGuard(body.Instructions, parameter))
            {
                continue;
            }

            var entry = body.Instructions.First();
            var errorMessage = $"[NullGuard] {parameter.Name} is null.";

            var guardInstructions = new List<Instruction>();

            if (isDebug)
            {
                LoadArgumentOntoStack(guardInstructions, parameter);

                CallDebugAssertInstructions(guardInstructions, errorMessage);
            }

            LoadArgumentOntoStack(guardInstructions, parameter);

            IfNull(guardInstructions, entry, i =>
            {
                LoadArgumentNullException(i, parameter.Name, errorMessage);

                // Throw the top item off the stack
                i.Add(Instruction.Create(OpCodes.Throw));
            });

            body.Instructions.Prepend(guardInstructions);
        }
    }

    void InjectMethodReturnGuard(ValidationFlags localValidationFlags, MethodDefinition method, MethodBody body)
    {
        var returnPoints = body.Instructions
                .Select((o, ix) => new { o, ix })
                .Where(a => a.o.OpCode == OpCodes.Ret)
                .Select(a => a.ix)
                .OrderByDescending(ix => ix);

        foreach (var ret in returnPoints)
        {
            if (localValidationFlags.HasFlag(ValidationFlags.ReturnValues) &&
                !nullabilityAnalyzer.AllowsNullReturnValue(method) &&
                method.ReturnType.IsRefType() &&
                method.ReturnType.FullName != typeof(void).FullName &&
                !method.IsGetter)
            {
                var errorMessage = string.Format(ReturnValueOfMethodIsNull, method.FullName);
                AddReturnNullGuard(method, ret, method.ReturnType, errorMessage, Instruction.Create(OpCodes.Throw));
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
                        !nullabilityAnalyzer.AllowsNull(parameter, method))
                    {
                        var errorMessage = $"[NullGuard] Out parameter '{parameter.Name}' is null.";

                        var guardInstructions = new List<Instruction>();

                        if (isDebug)
                        {
                            LoadArgumentOntoStack(guardInstructions, parameter);

                            CallDebugAssertInstructions(guardInstructions, errorMessage);
                        }

                        LoadArgumentOntoStack(guardInstructions, parameter);

                        IfNull(guardInstructions, returnInstruction, i =>
                        {
                            LoadInvalidOperationException(i, errorMessage);

                            // Throw the top item off the stack
                            i.Add(Instruction.Create(OpCodes.Throw));
                        });

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
            WriteWarning($"Cannot add guards to '{methodName}' as the method contains no await keyword.");
            return;
        }

        var setExceptionMethod = (MethodReference)setExceptionInstruction.Operand;

        var returnPoints = method.Body.Instructions
            .Select((instruction, index) => new { returnType = GetReturnTypeFromSetResultMethod(instruction), index })
            .Where(item => item.returnType != null)
            .OrderByDescending(item => item.index)
            .ToArray();

        foreach (var returnPoint in returnPoints)
        {
            AddReturnNullGuard(method, returnPoint.index, returnPoint.returnType, errorMessage, Instruction.Create(OpCodes.Call, setExceptionMethod), Instruction.Create(OpCodes.Ret));
        }

        method.Body.OptimizeMacros();
    }

    void AddReturnNullGuard(MethodDefinition methodDefinition, int ret, TypeReference returnType, string errorMessage, params Instruction[] finalInstructions)
    {
        var returnInstruction = methodDefinition.Body.Instructions[ret];

        var guardInstructions = new List<Instruction>();

        if (isDebug)
        {
            DuplicateReturnValue(guardInstructions, returnType);

            CallDebugAssertInstructions(guardInstructions, errorMessage);
        }

        DuplicateReturnValue(guardInstructions, returnType);

        IfNull(guardInstructions, returnInstruction, i =>
        {
            // Clean up the stack (important if finalInstructions doesn't throw, e.g. for async methods):
            i.Add(Instruction.Create(OpCodes.Pop));

            LoadInvalidOperationException(i, errorMessage);

            i.AddRange(finalInstructions);
        });

        methodDefinition.Body.InsertAtMethodReturnPoint(ret, guardInstructions);
    }

    bool CheckForExistingGuard(Collection<Instruction> instructions, ParameterDefinition parameter)
    {
        for (var i = 1; i < instructions.Count - 1; i++)
        {
            if (instructions[i].OpCode != OpCodes.Newobj)
            {
                continue;
            }

            if (!(instructions[i].Operand is MethodReference newObjectMethodRef) || instructions[i + 1].OpCode != OpCodes.Throw)
            {
                continue;
            }

            // Checks for throw new ArgumentNullException("x");
            if (newObjectMethodRef.FullName == ArgumentNullExceptionConstructor.FullName &&
                instructions[i - 1].OpCode == OpCodes.Ldstr &&
                (string)instructions[i - 1].Operand == parameter.Name)
            {
                return true;
            }

            // Checks for throw new ArgumentNullException("x", "some message");
            if (newObjectMethodRef.FullName == ArgumentNullExceptionWithMessageConstructor.FullName &&
                i > 1 &&
                instructions[i - 2].OpCode == OpCodes.Ldstr &&
                (string)instructions[i - 2].Operand == parameter.Name)
            {
                return true;
            }
        }

        return false;
    }

    static TypeReference GetReturnTypeFromSetResultMethod(Instruction instruction)
    {
        if (instruction.OpCode != OpCodes.Call)
            return null;

        if (!(instruction.Operand is MethodReference methodReference))
            return null;

        if (methodReference.Name != "SetResult" ||
            methodReference.Parameters.Count != 1)
            return null;

        var declaringType = methodReference.DeclaringType;

        if (!declaringType.FullName.StartsWith("System.Runtime.CompilerServices.AsyncTaskMethodBuilder"))
            return null;
        if (!(declaringType is GenericInstanceType genericInstanceType))
            return null;

        var genericParameter = genericInstanceType.GenericArguments.FirstOrDefault();

        return genericParameter;
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