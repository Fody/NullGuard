using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Mono.Cecil;
using Mono.Cecil.Rocks;
using Mono.Collections.Generic;

internal static class ExplicitMode
{
    private const string NotNullAttributeTypeName = "NotNullAttribute";
    private const string CanBeNullAttributeTypeName = "CanBeNullAttribute";
    private const string JetBrainsAnnotationsAssemblyName = "JetBrains.Annotations";

    private enum Nullability : byte
    {
        Undefined,
        CanBeNull,
        NotNull
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
        var nullability = _memberNullabilityCache.GetOrCreate(property);

        return nullability.Nullability != Nullability.NotNull;
    }

    public static bool AllowsNull(ParameterDefinition parameter, MethodDefinition method)
    {
        var nullability = _memberNullabilityCache.GetOrCreate(method);

        return nullability.Parameters[parameter.Index] != Nullability.NotNull;
    }

    public static bool AllowsNull(MethodDefinition method)
    {
        var nullability = _memberNullabilityCache.GetOrCreate(method);

        return nullability.ReturnValue != Nullability.NotNull;
    }

    private static Nullability GetNullability(this ParameterDefinition value)
    {
        if (value == null)
            return Nullability.Undefined;

        // Liskov: weakening of preconditions is OK, stop searching for NotNull if parameter is CanBeNull.
        if (!value.IsOut && value.HasCanBeNullAttribute())
        {
            return Nullability.CanBeNull;
        }

        if (value.HasNotNullAttribute())
        {
            return Nullability.NotNull;
        }

        return Nullability.Undefined;
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
            && TypeReferenceEqualityComparer.Default.Equals(property.PropertyType, reference.PropertyType)
            && property.Parameters.Select(parameter => parameter.ParameterType.Resolve()).SequenceEqual(reference.Parameters.Select(parameter => parameter.ParameterType.Resolve()), TypeReferenceEqualityComparer.Default));
    }

    private static MethodDefinition FindExplicitInterfaceImplementation(this TypeDefinition type, MethodDefinition interfaceMethod)
    {
        return type.Methods.FirstOrDefault(m => m.EnumerateOverrides().Any(o => MemberReferenceEqualityComparer.Default.Equals(o, interfaceMethod)));
    }

    private static PropertyDefinition FindExplicitInterfaceImplementation(this TypeDefinition type, PropertyDefinition interfaceProperty)
    {
        return type.Properties.FirstOrDefault(p => p.EnumerateOverrides().Any(o => MemberReferenceEqualityComparer.Default.Equals(o, interfaceProperty)));
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

    private class MemberNullability
    {
    }

    private class MethodNullability : MemberNullability
    {
        private readonly MethodDefinition _method;
        private readonly Nullability[] _parameters;
        private Nullability _returnValue;
        private bool _isInheritanceResolved;

        public MethodNullability(MethodDefinition method)
        {
            _method = method;
            _parameters = method.Parameters.Select(p => p.GetNullability()).ToArray();
            _returnValue = method.HasNotNullAttribute() ? Nullability.NotNull : Nullability.Undefined;
        }

        public Nullability ReturnValue
        {
            get
            {
                ResolveInheritance();
                return _returnValue;
            }
        }

        public IReadOnlyList<Nullability> Parameters
        {
            get
            {
                ResolveInheritance();
                return _parameters;
            }
        }

        private bool IsAnyValueUndefined => _returnValue == Nullability.Undefined || _parameters.Any(p => p == Nullability.Undefined);

        private void MergeFrom(MethodNullability baseMethod)
        {
            if (baseMethod == null)
                return;

            baseMethod.ResolveInheritance();

            if (_returnValue == Nullability.Undefined)
                _returnValue = baseMethod._returnValue;

            for (int i = 0; i < Parameters.Count; i++)
            {
                if (_parameters[i] == Nullability.Undefined)
                {
                    _parameters[i] = baseMethod._parameters[i];
                }
            }
        }

        private void ResolveInheritance()
        {
            if (_isInheritanceResolved)
                return;

            _isInheritanceResolved = true;

            if (!_method.HasThis || !IsAnyValueUndefined)
                return;

            var declaringType = _method.DeclaringType;

            foreach (var interfaceType in declaringType.GetInterfaces())
            {
                var interfaceMethod = interfaceType.Methods.Find(_method);
                if (interfaceMethod == null)
                    continue;

                if (declaringType.FindExplicitInterfaceImplementation(interfaceMethod) != null)
                    continue;

                var interfaceNullability = _memberNullabilityCache.GetOrCreate(interfaceMethod);

                MergeFrom(interfaceNullability);
            }

            foreach (var overrideMethod in _method.EnumerateOverrides())
            {
                var overrideNullability = _memberNullabilityCache.GetOrCreate(overrideMethod);

                MergeFrom(overrideNullability);
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
        private Nullability _nullability;
        private bool _isInheritanceResolved;

        public PropertyNullability(PropertyDefinition property)
        {
            _property = property;
            _nullability = property.HasNotNullAttribute() ? Nullability.NotNull : Nullability.Undefined;
        }

        public Nullability Nullability
        {
            get
            {
                ResolveInheritance();
                return _nullability;
            }
        }

        private bool IsAnyValueUndefined => _nullability == Nullability.Undefined;

        private void MergeFrom(PropertyNullability baseProperty)
        {
            if (baseProperty == null)
                return;

            baseProperty.ResolveInheritance();

            if (_nullability == Nullability.Undefined)
                _nullability = baseProperty._nullability;
        }

        private void ResolveInheritance()
        {
            if (_isInheritanceResolved)
                return;

            _isInheritanceResolved = true;

            if (!_property.HasThis || !IsAnyValueUndefined)
                return;

            var declaringType = _property.DeclaringType;

            foreach (var interfaceType in declaringType.GetInterfaces())
            {
                var interfaceProperty = interfaceType.Properties.Find(_property);
                if (interfaceProperty == null)
                    continue;

                if (declaringType.FindExplicitInterfaceImplementation(interfaceProperty) != null)
                    continue;

                var interfaceNullability = _memberNullabilityCache.GetOrCreate(interfaceProperty);

                MergeFrom(interfaceNullability);
            }

            foreach (var overrideProperty in _property.EnumerateOverrides())
            {
                var overrideNullability = _memberNullabilityCache.GetOrCreate(overrideProperty);

                MergeFrom(overrideNullability);
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
            return (MethodNullability)GetOrCreate(method, () => new MethodNullability(method));
        }

        public PropertyNullability GetOrCreate(PropertyDefinition property)
        {
            return (PropertyNullability)GetOrCreate(property, () => new PropertyNullability(property));
        }

        private MemberNullability GetOrCreate(MemberReference member, Func<MemberNullability> createNew)
        {
            var module = member.Module;
            var assemblyName = module.Assembly.Name.Name;

            var key = DocCommentId.GetDocCommentId((IMemberDefinition)member);

            if (!_cache.TryGetValue(assemblyName, out var assmblyCache))
            {
                var moduleFileName = module.FileName;

                assmblyCache = new AssemblyCache(moduleFileName);
                _cache.Add(assemblyName, assmblyCache);
            }

            if (assmblyCache.TryGetValue(key, out var value))
                return value;

            value = createNew();

            assmblyCache.Add(key, value);

            return value;
        }

        private class AssemblyCache
        {
            private Dictionary<string, MemberNullability> _cache = new Dictionary<string, MemberNullability>();

            public AssemblyCache(string moduleFileName)
            {
                var externalAnnotations = Path.ChangeExtension(moduleFileName, ".ExternalAnnotations.xml");

                if (File.Exists(externalAnnotations))
                {

                }
            }

            public void Add(string key, MemberNullability value)
            {
                _cache.Add(key, value);
            }

            public bool TryGetValue(string key, out MemberNullability value)
            {
                return _cache.TryGetValue(key, out value);
            }
        }
    }
}
