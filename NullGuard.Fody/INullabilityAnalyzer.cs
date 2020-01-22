using Mono.Cecil;

public interface INullabilityAnalyzer
{
    bool AllowsNull(PropertyDefinition property);
    bool AllowsNullInput(ParameterDefinition parameter, MethodDefinition method);
    bool AllowsNullOutput(ParameterDefinition parameter, MethodDefinition method);
    bool AllowsNullReturnValue(MethodDefinition method);
    bool AllowsNullAsyncTaskResult(MethodDefinition method, TypeReference resultType);
    bool AllowsGetMethodToReturnNull(PropertyDefinition property, MethodDefinition getMethod);
    bool AllowsSetMethodToAcceptNull(PropertyDefinition property, MethodDefinition setMethod, ParameterDefinition valueParameter);
}