public class ExplicitModeAnalyzer : INullabilityAnalyzer
{
    readonly MemberNullabilityCache memberNullabilityCache = new();

    public void CheckForBadAttributes(List<TypeDefinition> types, Action<string> logError)
    {
    }

    public bool AllowsNull(PropertyDefinition property)
    {
        var nullability = memberNullabilityCache.GetOrCreate(property);

        return nullability.AllowsNull;
    }

    public bool AllowsNullInput(ParameterDefinition parameter, MethodDefinition method)
    {
        var nullability = memberNullabilityCache.GetOrCreate(method);

        return nullability.ParameterAllowsNull(parameter.Index);
    }

    public bool AllowsNullOutput(ParameterDefinition parameter, MethodDefinition method)
    {
        // Maintain legacy behavior in non-NRT modes which does not check ref output values
        if (!parameter.IsOut)
            return true;

        var nullability = memberNullabilityCache.GetOrCreate(method);

        return nullability.ParameterAllowsNull(parameter.Index);
    }

    public bool AllowsNullReturnValue(MethodDefinition method)
    {
        var nullability = memberNullabilityCache.GetOrCreate(method);

        return nullability.ReturnValueAllowsNull;
    }

    public bool AllowsNullAsyncTaskResult(MethodDefinition method, TypeReference resultType)
    {
        var nullability = memberNullabilityCache.GetOrCreate(method);

        return nullability.ReturnValueAllowsNull;
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