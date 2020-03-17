using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using Mono.Cecil;
using Mono.Cecil.Rocks;

public class ExplicitModeAnalyzer : INullabilityAnalyzer
{
    readonly MemberNullabilityCache memberNullabilityCache = new MemberNullabilityCache();

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

[Flags]
enum NullabilityAttributes
{
    None = 0,
    CanBeNull = 1,
    NotNull = 2,
    ItemNotNull = 4,
    ItemCanBeNull = 8
}

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
        if (moduleDefinition.AssemblyReferences.All(ar => ar.Name != JetBrainsAnnotationsAssemblyName))
        {
            return NullGuardMode.Implicit;
        }

        foreach (var typeDefinition in moduleDefinition.GetTypes())
        {
            foreach (var method in typeDefinition.Methods)
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

    internal static NullabilityAttributes GetNullabilityAttributes(this ICustomAttributeProvider value)
    {
        return value?.CustomAttributes
            .Select(GetNullabilityAttribute)
            .DefaultIfEmpty()
            .Aggregate((s, a) => s | a) ?? NullabilityAttributes.None;
    }

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

    static MethodDefinition Find(this TypeReference declaringType, MethodReference reference)
    {
        return declaringType.Resolve().Methods
            .FirstOrDefault(method => HasSameSignature(declaringType, method, reference.DeclaringType, reference.Resolve()));
    }

    static PropertyDefinition Find(this TypeReference declaringType, PropertyReference reference)
    {
        return declaringType.Resolve().Properties
            .FirstOrDefault(property => HasSameSignature(declaringType, property, reference.DeclaringType, reference.Resolve()));
    }

    static bool HasSameSignature(TypeReference declaringType1, MethodDefinition method1, TypeReference declaringType2, MethodDefinition method2)
    {
        var resolveGenericParameter1 = method1.ReturnType.ResolveGenericParameter(declaringType1);
        var resolveGenericParameter2 = method2.ReturnType.ResolveGenericParameter(declaringType2);
        var areaAllParametersOfSameType = AreaAllParametersOfSameType(declaringType1, method1, declaringType2, method2);
        var referenceEquals = resolveGenericParameter1.FullName == resolveGenericParameter2.FullName;
        return method1.Name == method2.Name
               && referenceEquals
               && method1.GenericParameters.Count == method2.GenericParameters.Count
               && areaAllParametersOfSameType;
    }

    static bool HasSameSignature(TypeReference declaringType1, PropertyDefinition property1, TypeReference declaringType2, PropertyDefinition property2)
    {
        var resolveGenericParameter1 = property1.PropertyType.ResolveGenericParameter(declaringType1);
        var resolveGenericParameter2 = property2.PropertyType.ResolveGenericParameter(declaringType2);
        return property1.Name == property2.Name
               && resolveGenericParameter1.FullName == resolveGenericParameter2.FullName
               && AreaAllParametersOfSameType(declaringType1, property1, declaringType2, property2);
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
            { return false; }
        }

        return true;
    }

    static bool HasExplicitInterfaceImplementation(this TypeDefinition type, MethodDefinition method)
    {
        if (method == null)
            return false;

        return method.DeclaringType.Methods
            .Where(m => m != method && m.HasOverrides)
            .SelectMany(m => m.Overrides)
            .Any(methodReference => HasSameSignature(type, method, methodReference.DeclaringType, methodReference.Resolve()));
    }

    static bool HasExplicitInterfaceImplementation(this TypeDefinition type, PropertyDefinition property)
    {
        return type.HasExplicitInterfaceImplementation(property.GetMethod);
    }

    static PropertyDefinition GetBaseProperty(this PropertyDefinition property)
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

    static IEnumerable<MethodReference> EnumerateOverrides(this MethodDefinition method)
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

    static IEnumerable<PropertyDefinition> EnumerateOverrides(this PropertyDefinition property)
    {
        var getMethod = property.GetMethod;
        foreach (var getOverride in getMethod.EnumerateOverrides())
        {
            var typeDefinition = getOverride.DeclaringType.Resolve();
            var ovr = typeDefinition.Properties.FirstOrDefault(p => p.GetMethod == getOverride.Resolve());
            if (ovr != null)
            {
                yield return ovr;
            }
        }
    }

    static TypeReference ResolveGenericParameter(this TypeReference parameterType, TypeReference declaringType)
    {
        if (parameterType.IsGenericParameter && declaringType.IsGenericInstance)
        {
            var parameterIndex = ((GenericParameter)parameterType).Position;
            parameterType = ((GenericInstanceType)declaringType).GenericArguments[parameterIndex];
        }

        return parameterType;
    }

    static TypeReference ResolveGenericArguments(this TypeReference baseType, TypeReference derivedType)
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

    internal static NullabilityAttributes GetNullabilityAttributes(this XElement element)
    {
        return element.Elements("attribute")
            .Select(GetNullabilityAttribute)
            .DefaultIfEmpty()
            .Aggregate((s, v) => s | v);
    }
}

class MemberNullability
{
    protected MemberNullabilityCache MemberNullabilityCache { get; }

    public MemberNullability(MemberNullabilityCache memberNullabilityCache)
    {
        MemberNullabilityCache = memberNullabilityCache;
    }
}

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
            return;

        returnValueAttributes |= externalAnnotation.GetNullabilityAttributes();

        foreach (var childElement in externalAnnotation.Elements("parameter"))
        {
            var parameterName = childElement.Attribute("name")?.Value;
            if (parameterName == null)
                continue;

            var parameter = method.Parameters.FirstOrDefault(p => p.Name == parameterName);
            if (parameter == null)
                continue;

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
            return !attributes.HasFlag(NullabilityAttributes.NotNull);

        return !attributes.HasFlag(NullabilityAttributes.NotNull) || attributes.HasFlag(NullabilityAttributes.CanBeNull);
    }

    void MergeFrom(MethodNullability baseMethod)
    {
        if (baseMethod == null)
            return;

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
            return;

        isInheritanceResolved = true;

        if (!method.HasThis)
            return;

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

class MemberNullabilityCache
{
    readonly Dictionary<string, AssemblyCache> cache = new Dictionary<string, AssemblyCache>();

    public MethodNullability GetOrCreate(MethodDefinition method)
    {
        return (MethodNullability)GetOrCreate(method, externalAnnotation => new MethodNullability(this, method, externalAnnotation));
    }

    public PropertyNullability GetOrCreate(PropertyDefinition property)
    {
        return (PropertyNullability)GetOrCreate(property, externalAnnotation => new PropertyNullability(this, property, externalAnnotation));
    }

    MemberNullability GetOrCreate(MemberReference member, Func<XElement, MemberNullability> createNew)
    {
        var module = member.Module;
        var assemblyName = module.Assembly.Name.Name;

        var key = DocCommentId.GetDocCommentId((IMemberDefinition)member);

        if (!cache.TryGetValue(assemblyName, out var assemblyCache))
        {
            assemblyCache = new AssemblyCache(module.FileName);
            cache.Add(assemblyName, assemblyCache);
        }

        return assemblyCache.GetOrCreate(key, createNew);
    }

    class AssemblyCache
    {
        readonly Dictionary<string, MemberNullability> cache = new Dictionary<string, MemberNullability>();
        readonly Dictionary<string, XElement> externalAnnotations;

        public AssemblyCache(string moduleFileName)
        {
            var annotations = Path.ChangeExtension(moduleFileName, ".ExternalAnnotations.xml");

            if (!File.Exists(annotations))
                return;

            try
            {
                externalAnnotations = XDocument.Load(annotations)
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
            if (cache.TryGetValue(key, out var value))
                return value;

            XElement externalAnnotation = null;
            externalAnnotations?.TryGetValue(key, out externalAnnotation);

            value = createNew(externalAnnotation);

            cache.Add(key, value);

            return value;
        }
    }
}
