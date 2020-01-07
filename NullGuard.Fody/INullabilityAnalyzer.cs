using Mono.Cecil;

public interface INullabilityAnalyzer
{
    bool AllowsNull(PropertyDefinition property);
    bool AllowsNull(ParameterDefinition parameter, MethodDefinition method);
    bool AllowsNullReturnValue(MethodDefinition method);
    bool AllowsGetMethodToReturnNull(PropertyDefinition property, MethodReturnType getMethod);
    bool AllowsSetMethodToAcceptNull(PropertyDefinition property, MethodDefinition setMethod, ParameterDefinition valueParameter);
}