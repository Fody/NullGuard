using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using NullGuard;

public partial class ModuleWeaver
{
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
            throw new Exception($"An error occurred processing property '{property.FullName}'.", exception);
        }
    }

    void InnerProcess(PropertyDefinition property)
    {
        var localValidationFlags = ValidationFlags;

        if (!property.PropertyType.IsRefType())
            return;

        var attribute = property.DeclaringType.GetNullGuardAttribute();
        if (attribute != null)
        {
            localValidationFlags = (ValidationFlags)attribute.ConstructorArguments[0].Value;
        }

        if (!localValidationFlags.HasFlag(ValidationFlags.Properties))
        {
            return;
        }

        if (nullabilityAnalyzer.AllowsNull(property))
        {
            return;
        }

        var getMethod = property.GetMethod;
        var getBody = getMethod?.Body;
        if (getBody != null)
        {
            getMethod.Body.SimplifyMacros();

            if ((localValidationFlags.HasFlag(ValidationFlags.NonPublic)
                || (getMethod.IsPublic && property.DeclaringType.IsPublicOrNestedPublic())
                || getMethod.IsOverrideOrImplementationOfPublicMember())
                && !nullabilityAnalyzer.AllowsGetMethodToReturnNull(property, getMethod))
            {
                InjectPropertyGetterGuard(getMethod, property);
            }

            getMethod.UpdateDebugInfo();
            getBody.InitLocals = true;
            getBody.OptimizeMacros();
        }

        var setMethod = property.SetMethod;
        var setBody = setMethod?.Body;
        if (setBody != null)
        {
            setBody.SimplifyMacros();

            if (localValidationFlags.HasFlag(ValidationFlags.NonPublic)
                || (setMethod.IsPublic && property.DeclaringType.IsPublicOrNestedPublic())
                || setMethod.IsOverrideOrImplementationOfPublicMember())
            {
                InjectPropertySetterGuard(setMethod, property);
            }

            setMethod.UpdateDebugInfo();
            setBody.InitLocals = true;
            setBody.OptimizeMacros();
        }
    }

    void InjectPropertyGetterGuard(MethodDefinition getMethod, PropertyReference property)
    {
        var returnPoints = getMethod.Body.Instructions
            .Select((o, i) => new { o, i })
            .Where(a => a.o.OpCode == OpCodes.Ret)
            .Select(a => a.i)
            .OrderByDescending(i => i);

        foreach (var ret in returnPoints)
        {
            var returnInstruction = getMethod.Body.Instructions[ret];
            var errorMessage = $"[NullGuard] Return value of property '{property.FullName}' is null.";

            var guardInstructions = new List<Instruction>();

            var propertyType = property.PropertyType;

            if (isDebug)
            {
                DuplicateReturnValue(guardInstructions, propertyType);

                CallDebugAssertInstructions(guardInstructions, errorMessage);
            }

            DuplicateReturnValue(guardInstructions, propertyType);

            IfNull(guardInstructions, returnInstruction, i =>
            {
                // Clean up the stack since we're about to throw up.
                i.Add(Instruction.Create(OpCodes.Pop));

                LoadInvalidOperationException(i, errorMessage);

                // Throw the top item off the stack
                i.Add(Instruction.Create(OpCodes.Throw));
            });

            getMethod.Body.InsertAtMethodReturnPoint(ret, guardInstructions);
        }
    }

    void InjectPropertySetterGuard(MethodDefinition setMethod, PropertyDefinition property)
    {
        var valueParameter = setMethod.GetPropertySetterValueParameter();
        if (!valueParameter.MayNotBeNull())
            return;

        if (nullabilityAnalyzer.AllowsSetMethodToAcceptNull(property, setMethod, valueParameter))
            return;

        var guardInstructions = new List<Instruction>();
        var entry = setMethod.Body.Instructions.First();

        string errorMessage = null;
        string GetErrorMessage() => errorMessage ??= $"[NullGuard] Cannot set the value of property '{property.FullName}' to null.";

        if (isDebug)
        {
            LoadArgumentOntoStack(guardInstructions, valueParameter);

            CallDebugAssertInstructions(guardInstructions, GetErrorMessage());
        }

        LoadArgumentOntoStack(guardInstructions, valueParameter);

        IfNull(guardInstructions, entry, i =>
        {
            LoadArgumentNullException(i, valueParameter.Name, useSystemNullArgumentMessage ? null : GetErrorMessage());

            // Throw the top item off the stack
            i.Add(Instruction.Create(OpCodes.Throw));
        });

        setMethod.Body.Instructions.Prepend(guardInstructions);
    }
}