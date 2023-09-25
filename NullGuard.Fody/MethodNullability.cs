class MethodNullability : MemberNullability
{
    readonly MethodDefinition method;
    readonly NullabilityAttributes[] parameterAttributes;
    NullabilityAttributes returnValueAttributes;
    bool isInheritanceResolved;

    public MethodNullability(MemberNullabilityCache memberNullabilityCache, MethodDefinition method, XElement externalAnnotation)
        : base(memberNullabilityCache)
    {
        this.method = method;
        parameterAttributes = method.Parameters.Select(item => item.GetNullabilityAttributes()).ToArray();
        returnValueAttributes = method.GetNullabilityAttributes();

        if (externalAnnotation == null)
        {
            return;
        }

        returnValueAttributes |= externalAnnotation.GetNullabilityAttributes();

        foreach (var childElement in externalAnnotation.Elements("parameter"))
        {
            var parameterName = childElement.Attribute("name")?.Value;
            if (parameterName == null)
            {
                continue;
            }

            var parameter = method.Parameters.FirstOrDefault(_ => _.Name == parameterName);
            if (parameter == null)
            {
                continue;
            }

            var parameterIndex = parameter.Index;
            parameterAttributes[parameterIndex] |= childElement.GetNullabilityAttributes();
        }
    }

    public bool ReturnValueAllowsNull
    {
        get
        {
            ResolveInheritance();

            var effectiveAttribute = method.IsAsyncStateMachine() ? NullabilityAttributes.ItemNotNull : NullabilityAttributes.NotNull;

            return !returnValueAttributes.HasFlag(effectiveAttribute);
        }
    }

    public bool ParameterAllowsNull(int index)
    {
        ResolveInheritance();

        var attributes = parameterAttributes[index];
        var parameter = method.Parameters[index];

        if (parameter.IsOut)
        {
            return !attributes.HasFlag(NullabilityAttributes.NotNull);
        }

        return !attributes.HasFlag(NullabilityAttributes.NotNull) || attributes.HasFlag(NullabilityAttributes.CanBeNull);
    }

    void MergeFrom(MethodNullability baseMethod)
    {
        if (baseMethod == null)
        {
            return;
        }

        baseMethod.ResolveInheritance();

        returnValueAttributes |= baseMethod.returnValueAttributes;

        for (var i = 0; i < parameterAttributes.Length; i++)
        {
            parameterAttributes[i] |= baseMethod.parameterAttributes[i];
        }
    }

    void ResolveInheritance()
    {
        if (isInheritanceResolved)
        {
            return;
        }

        isInheritanceResolved = true;

        if (!method.HasThis)
        {
            return;
        }

        foreach (var method in method.EnumerateOverridesAndImplementations())
        {
            var nullability = MemberNullabilityCache.GetOrCreate(method.Resolve());

            MergeFrom(nullability);
        }
    }

    public override string ToString()
    {
        var parameters = string.Join(", ", parameterAttributes);
        return $"{returnValueAttributes} {method.Name}({parameters})";
    }
}