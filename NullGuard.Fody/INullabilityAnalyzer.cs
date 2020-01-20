using Mono.Cecil;

public interface INullabilityAnalyzer
{
    bool AllowsNull(PropertyDefinition property);
    bool AllowsNullInput(ParameterDefinition parameter, MethodDefinition method);
    bool AllowsNullOutput(ParameterDefinition parameter, MethodDefinition method);
    bool AllowsNullReturnValue(MethodDefinition method);
    bool AllowsGetMethodToReturnNull(PropertyDefinition property, MethodDefinition getMethod);
    bool AllowsSetMethodToAcceptNull(PropertyDefinition property, MethodDefinition setMethod, ParameterDefinition valueParameter);
}