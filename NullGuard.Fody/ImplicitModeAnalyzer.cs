using Mono.Cecil;

public class ImplicitModeAnalyzer : INullabilityAnalyzer
{
    public bool AllowsNull(PropertyDefinition property)
    {
        return property.ImplicitAllowsNull();
    }

    public bool AllowsNull(ParameterDefinition parameter, MethodDefinition method)
    {
        return parameter.ImplicitAllowsNull();
    }

    public bool AllowsNullReturnValue(MethodDefinition method)
    {
        return method.AllowsNullReturnValue();
    }

    public bool AllowsGetMethodToReturnNull(PropertyDefinition property, MethodReturnType getMethod)
    {
        return getMethod.ImplicitAllowsNull();
    }

    public bool AllowsSetMethodToAcceptNull(PropertyDefinition property, MethodDefinition setMethod, ParameterDefinition valueParameter)
    {
        return valueParameter.ImplicitAllowsNull();
    }
}

