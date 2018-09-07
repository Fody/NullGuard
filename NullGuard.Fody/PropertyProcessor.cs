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
    const string ReturnValueOfPropertyIsNull = "[NullGuard] Return value of property '{0}' is null.";
    const string CannotSetTheValueOfPropertyToNull = "[NullGuard] Cannot set the value of property '{0}' to null.";

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

        if (property.AllowsNull(explicitMode))
        {
            return;
        }

        var getMethod = property.GetMethod;
        if (getMethod?.Body != null)
        {
            getMethod.Body.SimplifyMacros();

            if ((localValidationFlags.HasFlag(ValidationFlags.NonPublic)
                || (getMethod.IsPublic && property.DeclaringType.IsPublicOrNestedPublic())
                || getMethod.IsOverrideOrImplementationOfPublicMember())
                && !getMethod.MethodReturnType.ImplicitAllowsNull())
            {
                InjectPropertyGetterGuard(getMethod, property);
            }

            getMethod.Body.InitLocals = true;
            getMethod.Body.OptimizeMacros();
        }

        var setMethod = property.SetMethod;
        if (setMethod?.Body != null)
        {
            var setBody = setMethod.Body;

            setBody.SimplifyMacros();

            if (localValidationFlags.HasFlag(ValidationFlags.NonPublic)
                || (setMethod.IsPublic && property.DeclaringType.IsPublicOrNestedPublic())
                || setMethod.IsOverrideOrImplementationOfPublicMember())
            {
                InjectPropertySetterGuard(setMethod, property);
            }

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
            var errorMessage = string.Format(CultureInfo.InvariantCulture, ReturnValueOfPropertyIsNull, property.FullName);

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
        var valueParameter = property.SetMethod.GetPropertySetterValueParameter();

        if (!valueParameter.MayNotBeNull(setMethod, null))
            return;

        var guardInstructions = new List<Instruction>();
        var errorMessage = string.Format(CultureInfo.InvariantCulture, CannotSetTheValueOfPropertyToNull, property.FullName);
        var entry = setMethod.Body.Instructions.First();

        if (isDebug)
        {
            LoadArgumentOntoStack(guardInstructions, valueParameter);

            CallDebugAssertInstructions(guardInstructions, errorMessage);
        }

        LoadArgumentOntoStack(guardInstructions, valueParameter);

        IfNull(guardInstructions, entry, i =>
        {
            LoadArgumentNullException(i, valueParameter.Name, errorMessage);

            // Throw the top item off the stack
            i.Add(Instruction.Create(OpCodes.Throw));
        });

        setMethod.Body.Instructions.Prepend(guardInstructions);
    }
}