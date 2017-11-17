using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using Mono.Cecil;
using Mono.Cecil.Rocks;

public static class ExplicitMode
{
    private const string NotNullAttributeTypeName = "NotNullAttribute";
    private const string ItemNotNullAttributeTypeName = "ItemNotNullAttribute";
    private const string CanBeNullAttributeTypeName = "CanBeNullAttribute";
    private const string JetBrainsAnnotationsAssemblyName = "JetBrains.Annotations";

    private enum Nullability : byte
    {
        Undefined,
        CanBeNull,
        NotNull
    }

    [Flags]
    private enum NullabilityAttributes
    {
        None = 0,
        CanBeNull = 1,
        NotNull = 2,
        ItemNotNull = 4,
        ItemCanBeNull = 8
    }

    private static readonly MemberNullabilityCache _memberNullabilityCache = new MemberNullabilityCache();

    public static NullGuardMode AutoDetectMode(this ModuleDefinition moduleDefinition)
    {
        // If we are referencing JetBrains.Annotations and using NotNull attributes, use explicit mode as default.

        if (moduleDefinition.AssemblyReferences.All(ar => ar.Name != JetBrainsAnnotationsAssemblyName))
            return NullGuardMode.Implicit;

        foreach (var typeDefinition in moduleDefinition.GetTypes())
        {
            foreach (var method in typeDefinition.GetMethods())
            {
                if (method.GetNullabilityAttributes().HasFlag(NullabilityAttributes.NotNull) || method.Parameters.Any(parameter => parameter.GetNullabilityAttributes().HasFlag(NullabilityAttributes.NotNull)))
                {
                    return NullGuardMode.Explicit;
                }
            }

            if (typeDefinition.Properties.Any(property => property.GetNullabilityAttributes().HasFlag(NullabilityAttributes.NotNull)))
            {
                return NullGuardMode.Explicit;
            }
        }

        return NullGuardMode.Implicit;
    }

    public static bool AllowsNull(PropertyDefinition property)
    {
        var nullability = _memberNullabilityCache.GetOrCreate(property);

        return nullability.Nullability != Nullability.NotNull;
    }

    public static bool AllowsNull(ParameterDefinition parameter, MethodDefinition method)
    {
        var nullability = _memberNullabilityCache.GetOrCreate(method);

        return nullability.GetParameterNullability(parameter.Index) != Nullability.NotNull;
    }

    public static bool AllowsNull(MethodDefinition method)
    {
        var nullability = _memberNullabilityCache.GetOrCreate(method);

        return nullability.ReturnValue != Nullability.NotNull;
    }

    private static NullabilityAttributes GetNullabilityAttributes(this ICustomAttributeProvider value)
    {
        return value?.CustomAttributes
            .Select(GetNullabilityAttribute)
            .DefaultIfEmpty()
            .Aggregate((s, a) => s | a) ?? NullabilityAttributes.None;
    }

    private static NullabilityAttributes GetNullabilityAttribute(CustomAttribute attribute)
    {
        switch (attribute.AttributeType.Name)
        {
            case CanBeNullAttributeTypeName:
                return NullabilityAttributes.CanBeNull;
            case NotNullAttributeTypeName:
                return NullabilityAttributes.NotNull;
            case ItemNotNullAttributeTypeName:
                return NullabilityAttributes.ItemNotNull;
            default:
                return NullabilityAttributes.None;
        }
    }

    public static IEnumerable<TypeReference> EnumerateInterfaces(this TypeDefinition typeDefinition, TypeReference typeReference)
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
            yield break;

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
                continue;

            if (declaringType.HasExplicitInterfaceImplementation(method))
                continue;

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
            yield break;

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

    public static MethodReference FindBase(this MethodDefinition method)
    {
        if (!method.IsVirtual || method.IsNewSlot)
            return null;

        TypeReference type = method.DeclaringType;

        for (type = type.Resolve().BaseType?.ResolveGenericArguments(type); type != null; type = type.Resolve().BaseType?.ResolveGenericArguments(type))
        {
            var matchingMethod = type.Find(method);
            if (matchingMethod != null)
                return matchingMethod;
        }

        return null;
    }

    public static MethodDefinition Find(this TypeReference declaringType, MethodReference reference)
    {
        return declaringType.Resolve().Methods.FirstOrDefault(method => HasSameSignature(declaringType, method, reference.DeclaringType, reference.Resolve()));
    }

    public static PropertyDefinition Find(this TypeReference declaringType, PropertyReference reference)
    {
        return declaringType.Resolve().Properties.FirstOrDefault(property => HasSameSignature(declaringType, property, reference.DeclaringType, reference.Resolve()));
    }

    private static bool HasSameSignature(TypeReference declaringType1, MethodDefinition method1, TypeReference declaringType2, MethodDefinition method2)
    {
        return method1.Name == method2.Name
               && TypeReferenceEqualityComparer.Default.Equals(method1.ReturnType.ResolveGenericParameter(declaringType1), method2.ReturnType.ResolveGenericParameter(declaringType2))
               && method1.GenericParameters.Count == method2.GenericParameters.Count
               && AreaAllParametersOfSameType(declaringType1, method1, declaringType2, method2);
    }

    private static bool HasSameSignature(TypeReference declaringType1, PropertyDefinition property1, TypeReference declaringType2, PropertyDefinition property2)
    {
        return property1.Name == property2.Name
               && TypeReferenceEqualityComparer.Default.Equals(property1.PropertyType.ResolveGenericParameter(declaringType1), property2.PropertyType.ResolveGenericParameter(declaringType2))
               && AreaAllParametersOfSameType(declaringType1, property1, declaringType2, property2);
    }

    private static bool AreaAllParametersOfSameType(TypeReference declaringType1, IMethodSignature method1, TypeReference declaringType2, IMethodSignature method2)
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

            if (!TypeReferenceEqualityComparer.Default.Equals(p1, p2))
                return false;
        }

        return true;
    }

    private static bool AreaAllParametersOfSameType(TypeReference declaringType1, PropertyDefinition property1, TypeReference declaringType2, PropertyDefinition property2)
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

            if (!TypeReferenceEqualityComparer.Default.Equals(p1, p2))
                return false;
        }

        return true;
    }

    public static bool HasExplicitInterfaceImplementation(this TypeDefinition type, MethodDefinition method)
    {
        if (method == null)
            return false;

        return method.DeclaringType.Methods
            .Where(m => m != method && m.HasOverrides)
            .SelectMany(m => m.Overrides)
            .Any(methodReference => HasSameSignature(type, method, methodReference.DeclaringType, methodReference.Resolve()));
    }

    public static bool HasExplicitInterfaceImplementation(this TypeDefinition type, PropertyDefinition property)
    {
        return type.HasExplicitInterfaceImplementation(property.GetMethod);
    }

    private static PropertyDefinition GetBaseProperty(this PropertyDefinition property)
    {
        var getMethod = property.GetMethod;
        var getMethodBase = getMethod?.FindBase();

        if (getMethodBase != null)
        {
            var baseProperty = getMethodBase.DeclaringType.Resolve().Properties.FirstOrDefault(p => p.GetMethod == getMethodBase);
            if (baseProperty != null)
                return baseProperty;
        }

        return null;
    }

    private static IEnumerable<MethodReference> EnumerateOverrides(this MethodDefinition method)
    {
        if (method == null)
            yield break;

        if (method.HasOverrides)
        {
            // Explicit interface implementations...
            foreach (var reference in method.Overrides)
            {
                yield return reference;
            }
        }
    }

    private static IEnumerable<PropertyDefinition> EnumerateOverrides(this PropertyDefinition property)
    {
        var getMethod = property.GetMethod;
        foreach (var getOverride in getMethod.EnumerateOverrides())
        {
            var typeDefinition = getOverride.DeclaringType.Resolve();
            var ovr = typeDefinition.Properties.FirstOrDefault(p => MemberReferenceEqualityComparer.Default.Equals(p.GetMethod, getOverride));
            if (ovr != null)
                yield return ovr;
        }
    }

    private static TypeReference ResolveGenericParameter(this TypeReference parameterType, TypeReference declaringType)
    {
        if (parameterType.IsGenericParameter && declaringType.IsGenericInstance)
        {
            var parameterIndex = ((GenericParameter)parameterType).Position;
            parameterType = ((GenericInstanceType)declaringType).GenericArguments[parameterIndex];
        }

        return parameterType;
    }

    private static TypeReference ResolveGenericArguments(this TypeReference baseType, TypeReference derivedType)
    {
        if (!baseType.IsGenericInstance)
            return baseType;

        if (!derivedType.IsGenericInstance)
            return baseType;

        var genericBase = (GenericInstanceType)baseType;
        if (!genericBase.HasGenericArguments)
            return baseType;

        var result = new GenericInstanceType(baseType);

        result.GenericArguments.AddRange(genericBase.GenericArguments.Select(arg => ResolveGenericParameter(arg, derivedType)));

        return result;
    }

    private static NullabilityAttributes GetNullabilityAttribute(this XElement element)
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

    private static NullabilityAttributes GetNullabilityAttributes(this XElement element)
    {
        return element.Elements("attribute")
            .Select(GetNullabilityAttribute)
            .DefaultIfEmpty()
            .Aggregate((s, v) => s | v);
    }

    private class MemberNullability
    {
    }

    private class MethodNullability : MemberNullability
    {
        private readonly MethodDefinition _method;
        private readonly NullabilityAttributes[] _parameters;
        private NullabilityAttributes _returnValue;
        private bool _isInheritanceResolved;

        public MethodNullability(MethodDefinition method, XElement externalAnnotation)
        {
            _method = method;
            _parameters = method.Parameters.Select(GetNullabilityAttributes).ToArray();
            _returnValue = method.GetNullabilityAttributes();

            if (externalAnnotation == null)
                return;

            _returnValue |= externalAnnotation.GetNullabilityAttributes();

            foreach (var childElement in externalAnnotation.Elements("parameter"))
            {
                var parameterName = childElement.Attribute("name")?.Value;
                if (parameterName == null)
                    continue;

                var parameter = method.Parameters.FirstOrDefault(p => p.Name == parameterName);
                if (parameter == null)
                    continue;

                var parameterIndex = parameter.Index;
                _parameters[parameterIndex] |= childElement.GetNullabilityAttributes();
            }
        }

        public Nullability ReturnValue
        {
            get
            {
                ResolveInheritance();

                var value = _method.IsAsyncStateMachine() ? NullabilityAttributes.ItemNotNull : NullabilityAttributes.NotNull;

                return _returnValue.HasFlag(value) ? Nullability.NotNull : Nullability.CanBeNull;
            }
        }

        public Nullability GetParameterNullability(int index)
        {
            ResolveInheritance();

            var p = _parameters[index];

            return (p.HasFlag(NullabilityAttributes.NotNull) && !p.HasFlag(NullabilityAttributes.CanBeNull)) ? Nullability.NotNull : Nullability.CanBeNull;
        }

        private void MergeFrom(MethodNullability baseMethod)
        {
            if (baseMethod == null)
                return;

            baseMethod.ResolveInheritance();

            _returnValue |= baseMethod._returnValue;

            for (var i = 0; i < _parameters.Length; i++)
            {
                _parameters[i] |= baseMethod._parameters[i];
            }
        }

        private void ResolveInheritance()
        {
            if (_isInheritanceResolved)
                return;

            _isInheritanceResolved = true;

            if (!_method.HasThis)
                return;

            foreach (var method in _method.EnumerateOverridesAndImplementations())
            {
                var nullability = _memberNullabilityCache.GetOrCreate(method.Resolve());

                MergeFrom(nullability);
            }
        }

        public override string ToString()
        {
            var parms = string.Join(", ", _parameters);
            return $"{_returnValue} {_method.Name}({parms})";
        }
    }

    private class PropertyNullability : MemberNullability
    {
        private readonly PropertyDefinition _property;
        private NullabilityAttributes _nullability;
        private bool _isInheritanceResolved;

        public PropertyNullability(PropertyDefinition property, XElement externalAnnotation)
        {
            _property = property;
            _nullability = property.GetNullabilityAttributes();

            if (externalAnnotation == null)
                return;

            _nullability |= externalAnnotation.GetNullabilityAttributes();
        }

        public Nullability Nullability
        {
            get
            {
                ResolveInheritance();

                return _nullability.HasFlag(NullabilityAttributes.NotNull) ? Nullability.NotNull : Nullability.CanBeNull;
            }
        }

        private void MergeFrom(PropertyNullability baseProperty)
        {
            if (baseProperty == null)
                return;

            baseProperty.ResolveInheritance();

            _nullability |= baseProperty._nullability;
        }

        private void ResolveInheritance()
        {
            if (_isInheritanceResolved)
                return;

            _isInheritanceResolved = true;

            if (!_property.HasThis)
                return;

            foreach (var property in _property.EnumerateOverridesAndImplementations())
            {
                var nullability = _memberNullabilityCache.GetOrCreate(property.Resolve());

                MergeFrom(nullability);
            }
        }

        public override string ToString()
        {
            return $"{_nullability} {_property.Name}";
        }
    }

    private class MemberNullabilityCache
    {
        private readonly Dictionary<string, AssemblyCache> _cache = new Dictionary<string, AssemblyCache>();

        public MethodNullability GetOrCreate(MethodDefinition method)
        {
            return (MethodNullability)GetOrCreate(method, externalAnnotation => new MethodNullability(method, externalAnnotation));
        }

        public PropertyNullability GetOrCreate(PropertyDefinition property)
        {
            return (PropertyNullability)GetOrCreate(property, externalAnnotation => new PropertyNullability(property, externalAnnotation));
        }

        private MemberNullability GetOrCreate(MemberReference member, Func<XElement, MemberNullability> createNew)
        {
            var module = member.Module;
            var assemblyName = module.Assembly.Name.Name;

            var key = DocCommentId.GetDocCommentId((IMemberDefinition)member);

            if (!_cache.TryGetValue(assemblyName, out var assmblyCache))
            {
                assmblyCache = new AssemblyCache(module.FileName);
                _cache.Add(assemblyName, assmblyCache);
            }

            return assmblyCache.GetOrCreate(key, createNew);
        }

        private class AssemblyCache
        {
            private readonly Dictionary<string, MemberNullability> _cache = new Dictionary<string, MemberNullability>();
            private readonly Dictionary<string, XElement> _externalAnnotations;

            public AssemblyCache(string moduleFileName)
            {
                var externalAnnotations = Path.ChangeExtension(moduleFileName, ".ExternalAnnotations.xml");

                if (!File.Exists(externalAnnotations))
                    return;

                try
                {
                    _externalAnnotations = XDocument.Load(externalAnnotations)
                        .Element("assembly")?
                        .Elements("member")
                        .ToDictionary(member => member.Attribute("name")?.Value);
                }
                catch
                {
                    // invalid file, ignore (TODO: log something?)
                }
            }

            public MemberNullability GetOrCreate(string key, Func<XElement, MemberNullability> createNew)
            {
                if (_cache.TryGetValue(key, out var value))
                    return value;

                XElement externalAnnotation = null;
                _externalAnnotations?.TryGetValue(key, out externalAnnotation);

                value = createNew(externalAnnotation);

                _cache.Add(key, value);

                return value;
            }
        }
    }
}
