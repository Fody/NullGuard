public static class ExplicitModeExtensions
{
    const string NotNullAttributeTypeName = "NotNullAttribute";
    const string ItemNotNullAttributeTypeName = "ItemNotNullAttribute";
    const string CanBeNullAttributeTypeName = "CanBeNullAttribute";
    const string JetBrainsAnnotationsAssemblyName = "JetBrains.Annotations";
    const string NullableContextAttributeFullName = "System.Runtime.CompilerServices.NullableContextAttribute";

    public static NullGuardMode AutoDetectMode(this ModuleDefinition moduleDefinition)
    {
        if (moduleDefinition.GetTypes().Any(typeDefinition => typeDefinition.CustomAttributes.Any(attr => attr.AttributeType.FullName == NullableContextAttributeFullName)))
        {
            return NullGuardMode.NullableReferenceTypes;
        }

        // If we are referencing JetBrains.Annotations and using NotNull attributes, use explicit mode as default.
        if (moduleDefinition.AssemblyReferences.All(_ => _.Name != JetBrainsAnnotationsAssemblyName))
        {
            return NullGuardMode.Implicit;
        }

        foreach (var typeDefinition in moduleDefinition.GetTypes())
        {
            foreach (var method in typeDefinition.Methods)
            {
                if (method.GetNullabilityAttributes().HasFlag(NullabilityAttributes.NotNull) ||
                    method.Parameters.Any(_ => _.GetNullabilityAttributes().HasFlag(NullabilityAttributes.NotNull)))
                {
                    return NullGuardMode.Explicit;
                }
            }

            if (typeDefinition.Properties.Any(_ => _.GetNullabilityAttributes().HasFlag(NullabilityAttributes.NotNull)))
            {
                return NullGuardMode.Explicit;
            }
        }

        return NullGuardMode.Implicit;
    }

    internal static NullabilityAttributes GetNullabilityAttributes(this ICustomAttributeProvider value) =>
        value?.CustomAttributes
            .Select(GetNullabilityAttribute)
            .DefaultIfEmpty()
            .Aggregate((s, a) => s | a) ?? NullabilityAttributes.None;

    static NullabilityAttributes GetNullabilityAttribute(CustomAttribute attribute)
    {
        return attribute.AttributeType.Name switch
        {
            CanBeNullAttributeTypeName => NullabilityAttributes.CanBeNull,
            NotNullAttributeTypeName => NullabilityAttributes.NotNull,
            ItemNotNullAttributeTypeName => NullabilityAttributes.ItemNotNull,
            _ => NullabilityAttributes.None,
        };
    }

    static IEnumerable<TypeReference> EnumerateInterfaces(this TypeDefinition typeDefinition, TypeReference typeReference)
    {
        foreach (var implementation in typeDefinition.Interfaces)
        {
            var interfaceType = implementation.InterfaceType;

            yield return interfaceType.ResolveGenericArguments(typeReference);
        }
    }

    public static IEnumerable<MethodDefinition> EnumerateOverridesAndImplementations(this MethodDefinition method)
    {
        if (!method.HasThis)
        {
            yield break;
        }

        if (method.IsPrivate)
        {
            if (method.HasOverrides)
            {
                foreach (var methodOverride in method.Overrides)
                {
                    yield return methodOverride.Resolve();
                }
            }

            yield break;
        }

        var declaringType = method.DeclaringType;

        foreach (var interfaceType in declaringType.EnumerateInterfaces(declaringType))
        {
            var interfaceMethod = interfaceType.Find(method);
            if (interfaceMethod == null)
            {
                continue;
            }

            if (declaringType.HasExplicitInterfaceImplementation(method))
            {
                continue;
            }

            yield return interfaceMethod;
        }

        var baseMethod = method.FindBase()?.Resolve();
        if (baseMethod != null)
        {
            yield return baseMethod;

            foreach (var baseImplementation in baseMethod.EnumerateOverridesAndImplementations())
            {
                yield return baseImplementation;
            }
        }
    }

    public static IEnumerable<PropertyDefinition> EnumerateOverridesAndImplementations(this PropertyDefinition property)
    {
        if (!property.HasThis)
        {
            yield break;
        }

        var propertyOverrides = property.EnumerateOverrides().ToArray();
        if (propertyOverrides.Any())
        {
            foreach (var propertyOverride in propertyOverrides)
            {
                yield return propertyOverride;
            }

            yield break;
        }

        var declaringType = property.GetMethod?.DeclaringType;
        if (declaringType != null)
        {
            foreach (var interfaceType in declaringType.EnumerateInterfaces(declaringType))
            {
                var interfaceProperty = interfaceType.Find(property);
                if (interfaceProperty == null)
                    continue;

                if (declaringType.HasExplicitInterfaceImplementation(property))
                    continue;

                yield return interfaceProperty;
            }
        }

        var baseProperty = property.GetBaseProperty();
        if (baseProperty != null)
        {
            yield return baseProperty;

            foreach (var baseImplementation in baseProperty.EnumerateOverridesAndImplementations())
            {
                yield return baseImplementation;
            }
        }
    }

    static MethodReference FindBase(this MethodDefinition method)
    {
        if (!method.IsVirtual || method.IsNewSlot)
        {
            return null;
        }

        TypeReference type = method.DeclaringType;

        for (type = type.Resolve().BaseType?.ResolveGenericArguments(type); type != null; type = type.Resolve().BaseType?.ResolveGenericArguments(type))
        {
            var matchingMethod = type.Find(method);
            if (matchingMethod != null)
            {
                return matchingMethod;
            }
        }

        return null;
    }

    static MethodDefinition Find(this TypeReference declaringType, MethodReference reference) =>
        declaringType.Resolve().Methods
            .FirstOrDefault(_ => HasSameSignature(declaringType, _, reference.DeclaringType, reference.Resolve()));

    static PropertyDefinition Find(this TypeReference declaringType, PropertyReference reference) =>
        declaringType.Resolve().Properties
            .FirstOrDefault(_ => HasSameSignature(declaringType, _, reference.DeclaringType, reference.Resolve()));

    static bool HasSameSignature(TypeReference declaringType1, MethodDefinition method1, TypeReference declaringType2, MethodDefinition method2)
    {
        var resolveGenericParameter1 = method1.ReturnType.ResolveGenericParameter(declaringType1);
        var resolveGenericParameter2 = method2.ReturnType.ResolveGenericParameter(declaringType2);
        var areaAllParametersOfSameType = AreaAllParametersOfSameType(declaringType1, method1, declaringType2, method2);
        var referenceEquals = resolveGenericParameter1.FullName == resolveGenericParameter2.FullName;
        return method1.Name == method2.Name &&
               referenceEquals &&
               method1.GenericParameters.Count == method2.GenericParameters.Count &&
               areaAllParametersOfSameType;
    }

    static bool HasSameSignature(TypeReference declaringType1, PropertyDefinition property1, TypeReference declaringType2, PropertyDefinition property2)
    {
        var resolveGenericParameter1 = property1.PropertyType.ResolveGenericParameter(declaringType1);
        var resolveGenericParameter2 = property2.PropertyType.ResolveGenericParameter(declaringType2);
        return property1.Name == property2.Name &&
               resolveGenericParameter1.FullName == resolveGenericParameter2.FullName &&
               AreaAllParametersOfSameType(declaringType1, property1, declaringType2, property2);
    }

    static bool AreaAllParametersOfSameType(TypeReference declaringType1, IMethodSignature method1, TypeReference declaringType2, IMethodSignature method2)
    {
        if (!method2.HasParameters)
            return !method1.HasParameters;

        if (!method1.HasParameters)
            return false;

        if (method1.Parameters.Count != method2.Parameters.Count)
            return false;

        for (var i = 0; i < method1.Parameters.Count; i++)
        {
            var p1 = method1.Parameters[i].ParameterType.ResolveGenericParameter(declaringType1);

            var p2 = method2.Parameters[i].ParameterType.ResolveGenericParameter(declaringType2);

            if (p1.FullName != p2.FullName)
            {
                return false;
            }
        }

        return true;
    }

    static bool AreaAllParametersOfSameType(TypeReference declaringType1, PropertyDefinition property1, TypeReference declaringType2, PropertyDefinition property2)
    {
        if (!property2.HasParameters)
            return !property1.HasParameters;

        if (!property1.HasParameters)
            return false;

        if (property1.Parameters.Count != property2.Parameters.Count)
            return false;

        for (var i = 0; i < property1.Parameters.Count; i++)
        {
            var p1 = property1.Parameters[i].ParameterType.ResolveGenericParameter(declaringType1);

            var p2 = property2.Parameters[i].ParameterType.ResolveGenericParameter(declaringType2);

            if (p1.FullName != p2.FullName)
            {
                return false;
            }
        }

        return true;
    }

    static bool HasExplicitInterfaceImplementation(this TypeDefinition type, MethodDefinition method)
    {
        if (method == null)
        {
            return false;
        }

        return method.DeclaringType.Methods
            .Where(_ => _ != method &&
                        _.HasOverrides)
            .SelectMany(_ => _.Overrides)
            .Any(methodReference => HasSameSignature(type, method, methodReference.DeclaringType, methodReference.Resolve()));
    }

    static bool HasExplicitInterfaceImplementation(this TypeDefinition type, PropertyDefinition property) => type.HasExplicitInterfaceImplementation(property.GetMethod);

    static PropertyDefinition GetBaseProperty(this PropertyDefinition property)
    {
        var getMethod = property.GetMethod;
        var getMethodBase = getMethod?.FindBase();

        return getMethodBase?.DeclaringType
            .Resolve()
            .Properties
            .FirstOrDefault(_ => _.GetMethod == getMethodBase);
    }

    static IEnumerable<MethodReference> EnumerateOverrides(this MethodDefinition method)
    {
        if (method == null)
        {
            yield break;
        }

        if (method.HasOverrides)
        {
            // Explicit interface implementations...
            foreach (var reference in method.Overrides)
            {
                yield return reference;
            }
        }
    }

    static IEnumerable<PropertyDefinition> EnumerateOverrides(this PropertyDefinition property)
    {
        var getMethod = property.GetMethod;
        foreach (var getOverride in getMethod.EnumerateOverrides())
        {
            var typeDefinition = getOverride.DeclaringType.Resolve();
            var ovr = typeDefinition.Properties.FirstOrDefault(_ => _.GetMethod == getOverride.Resolve());
            if (ovr != null)
            {
                yield return ovr;
            }
        }
    }

    static TypeReference ResolveGenericParameter(this TypeReference parameterType, TypeReference declaringType)
    {
        if (parameterType.IsGenericParameter &&
            declaringType.IsGenericInstance)
        {
            var parameterIndex = ((GenericParameter) parameterType).Position;
            parameterType = ((GenericInstanceType) declaringType).GenericArguments[parameterIndex];
        }

        return parameterType;
    }

    static TypeReference ResolveGenericArguments(this TypeReference baseType, TypeReference derivedType)
    {
        if (!baseType.IsGenericInstance)
        {
            return baseType;
        }

        if (!derivedType.IsGenericInstance)
        {
            return baseType;
        }

        var genericBase = (GenericInstanceType) baseType;
        if (!genericBase.HasGenericArguments)
        {
            return baseType;
        }

        var result = new GenericInstanceType(baseType);

        result.GenericArguments.AddRange(genericBase.GenericArguments.Select(arg => ResolveGenericParameter(arg, derivedType)));

        return result;
    }

    static NullabilityAttributes GetNullabilityAttribute(this XElement element)
    {
        var value = element.Attribute("ctor")?.Value;
        if (value == null)
            return NullabilityAttributes.None;

        if (value.EndsWith(".NotNullAttribute.#ctor"))
            return NullabilityAttributes.NotNull;

        if (value.EndsWith(".ItemNotNullAttribute.#ctor"))
            return NullabilityAttributes.ItemNotNull;

        if (value.EndsWith(".CanBeNullAttribute.#ctor"))
            return NullabilityAttributes.CanBeNull;

        if (value.EndsWith(".ItemCanBeNullAttribute.#ctor"))
            return NullabilityAttributes.ItemCanBeNull;

        return NullabilityAttributes.None;
    }

    internal static NullabilityAttributes GetNullabilityAttributes(this XElement element) =>
        element.Elements("attribute")
            .Select(GetNullabilityAttribute)
            .DefaultIfEmpty()
            .Aggregate((s, v) => s | v);
}