using System;
using System.Collections.Generic;
using Mono.Cecil;

public interface INullabilityAnalyzer
{
    void CheckForBadAttributes(List<TypeDefinition> types, Action<string> logError);
    bool AllowsNull(PropertyDefinition property);
    bool AllowsNullInput(ParameterDefinition parameter, MethodDefinition method);
    bool AllowsNullOutput(ParameterDefinition parameter, MethodDefinition method);
    bool AllowsNullReturnValue(MethodDefinition method);
    bool AllowsNullAsyncTaskResult(MethodDefinition method, TypeReference resultType);
    bool AllowsGetMethodToReturnNull(PropertyDefinition property, MethodDefinition getMethod);
    bool AllowsSetMethodToAcceptNull(PropertyDefinition property, MethodDefinition setMethod, ParameterDefinition valueParameter);
}