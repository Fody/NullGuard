class PropertyNullability : MemberNullability
{
    readonly PropertyDefinition property;
    NullabilityAttributes nullabilityAttributes;
    bool isInheritanceResolved;

    public PropertyNullability(MemberNullabilityCache memberNullabilityCache, PropertyDefinition property, XElement externalAnnotation)
        : base(memberNullabilityCache)
    {
        this.property = property;
        nullabilityAttributes = property.GetNullabilityAttributes();

        if (externalAnnotation == null)
            return;

        nullabilityAttributes |= externalAnnotation.GetNullabilityAttributes();
    }

    public bool AllowsNull
    {
        get
        {
            ResolveInheritance();

            return !nullabilityAttributes.HasFlag(NullabilityAttributes.NotNull);
        }
    }

    void MergeFrom(PropertyNullability baseProperty)
    {
        if (baseProperty == null)
            return;

        baseProperty.ResolveInheritance();

        nullabilityAttributes |= baseProperty.nullabilityAttributes;
    }

    void ResolveInheritance()
    {
        if (isInheritanceResolved)
            return;

        isInheritanceResolved = true;

        if (!property.HasThis)
            return;

        foreach (var property in property.EnumerateOverridesAndImplementations())
        {
            var nullability = MemberNullabilityCache.GetOrCreate(property.Resolve());

            MergeFrom(nullability);
        }
    }

    public override string ToString()
    {
        return $"{nullabilityAttributes} {property.Name}";
    }
}