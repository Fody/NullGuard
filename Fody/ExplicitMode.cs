using System.Collections.Generic;
using System.Linq;

using Mono.Cecil;
using Mono.Cecil.Rocks;
using Mono.Collections.Generic;

internal static class ExplicitMode
{
    private const string NotNullAttributeTypeName = "NotNullAttribute";
    private const string CanBeNullAttributeTypeName = "CanBeNullAttribute";
    private const string JetBrainsAnnotationsAssemblyName = "JetBrains.Annotations";

    public static NullGuardMode AutoDetectMode(this ModuleDefinition moduleDefinition)
    {
        // If we are referencing JetBrains.Annotations and using NotNull attributes, use explicit mode as default.

        if (moduleDefinition.AssemblyReferences.All(ar => ar.Name != JetBrainsAnnotationsAssemblyName))
            return NullGuardMode.Implicit;

        foreach (var typeDefinition in moduleDefinition.GetTypes())
        {
            foreach (var method in typeDefinition.GetMethods())
            {
                if (method.HasNotNullAttribute() || method.Parameters.Any(parameter => parameter.HasNotNullAttribute()))
                {
                    return NullGuardMode.Explicit;
                }
            }

            if (typeDefinition.Properties.Any(property => property.HasNotNullAttribute()))
            {
                return NullGuardMode.Explicit;
            }
        }

        return NullGuardMode.Implicit;
    }

    public static bool AllowsNull(PropertyDefinition property)
    {
        if (property.HasNotNullAttribute())
            return false;

        if (!property.HasThis)
            return true;

        if (property.HasNotNullAttributeOnImplicitImplementedInterface())
            return false;

        if (property.EnumerateOverrides().Any(p => !AllowsNull(p)))
            return false;

        return true;
    }

    public static bool AllowsNull(ParameterDefinition parameter, MethodDefinition method)
    {
        if (parameter.HasNullabilityAnnotation(out var value))
            return value;

        if (!method.HasThis)
            return true;

        if (HasNullabilityAnnotationOnImplicitImplementedInterface(parameter, method, out var allowsNull))
            return allowsNull;

        if (method.EnumerateOverrides().Any(m => !AllowsNull(m.Parameters[parameter.Index], m)))
            return false;

        return true;
    }

    public static bool AllowsNull(MethodDefinition method)
    {
        if (method.HasNotNullAttribute())
            return false;

        if (!method.HasThis)
            return true;

        if (HasNotNullAttributeOnImplicitImplementedInterface(method))
            return false;

        if (method.EnumerateOverrides().Any(m => !AllowsNull(m)))
            return false;

        return true;
    }

    private static bool HasNotNullAttributeOnImplicitImplementedInterface(this PropertyDefinition property)
    {
        var declaringType = property.DeclaringType;

        foreach (var interfaceType in declaringType.GetInterfaces())
        {
            var interfaceProperty = interfaceType.Properties.Find(property);
            if (interfaceProperty == null)
                continue;

            if (declaringType.FindExplicitInterfaceImplementation(interfaceProperty) != null)
                continue;

            if (interfaceProperty.HasNotNullAttribute())
                return true;
        }
        return false;
    }

    private static bool HasNullabilityAnnotationOnImplicitImplementedInterface(ParameterReference parameter, MethodDefinition method, out bool allowsNull)
    {
        allowsNull = true;

        var declaringType = method.DeclaringType;
        var parameterIndex = parameter.Index;

        foreach (var interfaceType in declaringType.GetInterfaces())
        {
            var interfaceMethod = interfaceType.Methods.Find(method);
            if (interfaceMethod == null)
                continue;

            if (declaringType.FindExplicitInterfaceImplementation(interfaceMethod) != null)
                continue;

            if (interfaceMethod.Parameters[parameterIndex].HasNullabilityAnnotation(out allowsNull))
            {
                return true;
            }
        }

        return false;
    }

    private static bool HasNotNullAttributeOnImplicitImplementedInterface(MethodDefinition method)
    {
        var declaringType = method.DeclaringType;

        foreach (var interfaceType in declaringType.GetInterfaces())
        {
            var interfaceMethod = interfaceType.Methods.Find(method);
            if (interfaceMethod == null)
                continue;

            if (declaringType.FindExplicitInterfaceImplementation(interfaceMethod) != null)
                continue;

            if (interfaceMethod.HasNotNullAttribute())
                return true;
        }
        return false;
    }

    private static bool HasNullabilityAnnotation(this ParameterDefinition value, out bool allowsNull)
    {
        allowsNull = true;

        if (value == null)
            return false;

        // Liskov: weakening of preconditions is OK, stop searching for NotNull if parameter is CanBeNull.
        if (!value.IsOut && value.HasCanBeNullAttribute())
        {
            return true;
        }

        if (value.HasNotNullAttribute())
        {
            allowsNull = false;
            return true;
        }

        return false;
    }

    private static bool HasNotNullAttribute(this ICustomAttributeProvider value)
    {
        return value?.CustomAttributes.Any(a => a.AttributeType.Name == NotNullAttributeTypeName) ?? false;
    }

    private static bool HasCanBeNullAttribute(this ICustomAttributeProvider value)
    {
        return value?.CustomAttributes.Any(a => a.AttributeType.Name == CanBeNullAttributeTypeName) ?? false;
    }

    private static IEnumerable<TypeDefinition> GetInterfaces(this TypeDefinition typeDefinition)
    {
        if (typeDefinition.IsInterface)
            yield return typeDefinition;

        foreach (var interfaceType in typeDefinition.Interfaces.Select(i => i.InterfaceType.Resolve()).SelectMany(i => i.GetInterfaces()))
        {
            yield return interfaceType;
        }
    }

    private static MethodDefinition Find(this Collection<MethodDefinition> methods, MethodReference reference)
    {
        return MetadataResolver.GetMethod(methods, reference);
    }

    private static PropertyDefinition Find(this ICollection<PropertyDefinition> properties, PropertyReference reference)
    {
        return properties.FirstOrDefault(property => property.Name == reference.Name
            && property.PropertyType == reference.PropertyType
            && property.Parameters.Select(parameter => parameter.ParameterType.Resolve()).SequenceEqual(reference.Parameters.Select(parameter => parameter.ParameterType.Resolve())));
    }

    private static MethodDefinition FindExplicitInterfaceImplementation(this TypeDefinition type, MethodDefinition interfaceMethod)
    {
        return type.Methods.FirstOrDefault(m => m.EnumerateOverrides().Any(o => o == interfaceMethod));
    }

    private static PropertyDefinition FindExplicitInterfaceImplementation(this TypeDefinition type, PropertyDefinition interfaceProperty)
    {
        return type.Properties.FirstOrDefault(p => p.EnumerateOverrides().Any(o => o == interfaceProperty));
    }

    private static IEnumerable<MethodDefinition> EnumerateOverrides(this MethodDefinition method)
    {
        if (method == null)
            yield break;

        if (method.HasOverrides)
        {
            // Explicit interface implementations...
            foreach (var reference in method.Overrides)
            {
                yield return reference.Resolve();
            }
        }

        // override of base class method...
        var baseMethod = method.GetBaseMethod();
        if (baseMethod != method)
            yield return baseMethod;
    }

    private static IEnumerable<PropertyDefinition> EnumerateOverrides(this PropertyDefinition property)
    {
        var getMethod = property.GetMethod;
        foreach (var getOverride in getMethod.EnumerateOverrides())
        {
            yield return getOverride.DeclaringType.Properties.FirstOrDefault(p => p.GetMethod == getOverride);
        }

        var setMethod = property.SetMethod;
        foreach (var setOverride in setMethod.EnumerateOverrides())
        {
            yield return setOverride.DeclaringType.Properties.FirstOrDefault(p => p.SetMethod == setOverride);
        }
    }
}