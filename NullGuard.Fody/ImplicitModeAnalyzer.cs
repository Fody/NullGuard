using System;
using System.Collections.Generic;
using Mono.Cecil;

public class ImplicitModeAnalyzer : INullabilityAnalyzer
{
    public void CheckForBadAttributes(List<TypeDefinition> types, Action<string> logError)
    {
        foreach (var typeDefinition in types)
        {
            foreach (var method in typeDefinition.AbstractMethods())
            {
                if (method.ContainsAllowNullAttribute())
                {
                    logError($"Method '{method.FullName}' is abstract but has an [AllowNullAttribute]. Remove this attribute.");
                }
                foreach (var parameter in method.Parameters)
                {
                    if (parameter.ContainsAllowNullAttribute())
                    {
                        logError($"Method '{method.FullName}' is abstract but has an [AllowNullAttribute]. Remove this attribute.");
                    }
                }
            }
        }
    }

    public bool AllowsNull(PropertyDefinition property)
    {
        return property.ImplicitAllowsNull();
    }

    public bool AllowsNullInput(ParameterDefinition parameter, MethodDefinition method)
    {
        return parameter.ImplicitAllowsNull();
    }

    public bool AllowsNullOutput(ParameterDefinition parameter, MethodDefinition method)
    {
        // Maintain legacy behavior in non-NRT modes which does not check ref output values
        if (!parameter.IsOut)
            return true;

        return parameter.ImplicitAllowsNull();
    }

    public bool AllowsNullReturnValue(MethodDefinition method)
    {
        return method.AllowsNullReturnValue();
    }

    public bool AllowsNullAsyncTaskResult(MethodDefinition method, TypeReference resultType)
    {
        return method.AllowsNullReturnValue();
    }

    public bool AllowsGetMethodToReturnNull(PropertyDefinition property, MethodDefinition getMethod)
    {
        return getMethod.MethodReturnType.ImplicitAllowsNull();
    }

    public bool AllowsSetMethodToAcceptNull(PropertyDefinition property, MethodDefinition setMethod, ParameterDefinition valueParameter)
    {
        return valueParameter.ImplicitAllowsNull();
    }
}

