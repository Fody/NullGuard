using System.Diagnostics;

static class CecilExtensions
{
    const string AllowNullAttributeTypeName = "AllowNullAttribute";
    const string CanBeNullAttributeTypeName = "CanBeNullAttribute";

    public static bool HasInterface(this TypeDefinition type, string interfaceFullName) =>
        type.Interfaces.Any(_ => _.InterfaceType.FullName.Equals(interfaceFullName)) ||
        type.BaseType != null &&
        type.BaseType.Resolve().HasInterface(interfaceFullName);

    public static IEnumerable<MethodDefinition> AbstractMethods(this TypeDefinition type) =>
        type.Methods.Where(_ => _.IsAbstract);

    public static IEnumerable<MethodDefinition> MethodsWithBody(this TypeDefinition type) =>
        type.Methods.Where(_ => _.Body != null);

    public static IEnumerable<PropertyDefinition> ConcreteProperties(this TypeDefinition type) =>
        type.Properties.Where(_ => _.GetMethod is not {IsAbstract: true} &&
                                   _.SetMethod is not {IsAbstract: true});

    public static CustomAttribute GetNullGuardAttribute(this ICustomAttributeProvider value) =>
        value.CustomAttributes.FirstOrDefault(_ => _.AttributeType.Name == "NullGuardAttribute");

    public static ParameterDefinition GetPropertySetterValueParameter(this MethodDefinition method)
    {
        Debug.Assert(method.IsSetter);
        // The last parameter of a property setter "value" parameter (see ECMA-335 (2012) I.8.11.3, CLS rule 27):
        return method.Parameters.Last();
    }

    public static bool ImplicitAllowsNull(this ICustomAttributeProvider value) =>
        value.CustomAttributes.Any(_ =>
            _.AttributeType.Name is
                AllowNullAttributeTypeName or
                CanBeNullAttributeTypeName);

    public static bool AllowsNullReturnValue(this MethodDefinition methodDefinition) =>
        methodDefinition.MethodReturnType.CustomAttributes.Any(_ => _.AttributeType.Name == AllowNullAttributeTypeName) ||
        // ReSharper uses a *method* attribute for CanBeNull for the return value
        methodDefinition.CustomAttributes.Any(_ => _.AttributeType.Name == CanBeNullAttributeTypeName);

    public static bool ContainsAllowNullAttribute(this ICustomAttributeProvider definition) =>
        definition.CustomAttributes.Any(_ => _.AttributeType.Name == AllowNullAttributeTypeName);

    public static void RemoveAllNullGuardAttributes(this ICustomAttributeProvider definition)
    {
        var customAttributes = definition.CustomAttributes;

        var attributes = customAttributes
            .Where(_ => _.AttributeType.Namespace is "NullGuard" or "NullGuard.CodeAnalysis")
            .ToArray();

        foreach (var attribute in attributes)
        {
            customAttributes.Remove(attribute);
        }
    }

    public static bool MayNotBeNull(this ParameterDefinition arg) =>
        !arg.IsOptionalArgumentWithNullDefaultValue() &&
        arg.ParameterType.IsRefType() &&
        !arg.IsOut;

    static bool IsOptionalArgumentWithNullDefaultValue(this ParameterDefinition arg) =>
        arg.IsOptional &&
        arg.Constant == null;

    public static bool IsRefType(this TypeReference arg)
    {
        if (arg.IsValueType)
        {
            return false;
        }

        if (arg is ByReferenceType or PointerType or RequiredModifierType or OptionalModifierType)
        {
            return IsRefType(((TypeSpecification) arg).ElementType);
        }

        if (arg is GenericParameter genericParameter)
        {
            return !genericParameter.HasNotNullableValueTypeConstraint;
        }

        return arg.MetadataType != MetadataType.Void;
    }

    public static bool NeedsBoxing(this TypeReference elementType)
    {
        if (elementType == null)
            return false;

        if (!elementType.IsGenericParameter)
            return false;

        if (elementType is not GenericParameter genericParameter)
            return false;

        if (genericParameter.HasNotNullableValueTypeConstraint)
            return true;

        if (genericParameter.HasReferenceTypeConstraint)
            return false;

        return true;
    }

    public static bool IsGeneratedCode(this ICustomAttributeProvider value) =>
        value.CustomAttributes.Any(_ =>
            _.AttributeType.Name is
                "CompilerGeneratedAttribute" or
                "GeneratedCodeAttribute");

    public static bool IsAsyncStateMachine(this ICustomAttributeProvider value) =>
        value.CustomAttributes.Any(_ => _.AttributeType.Name == "AsyncStateMachineAttribute");

    public static bool IsIteratorStateMachine(this ICustomAttributeProvider value) =>
        // Only works on VB not C# but it's something.
        value.CustomAttributes.Any(_ => _.AttributeType.Name == "IteratorStateMachineAttribute");

    public static bool IsIAsyncStateMachine(this TypeDefinition typeDefinition) =>
        typeDefinition.Interfaces.Any(_ => _.InterfaceType.Name == "IAsyncStateMachine");

    public static bool IsPublicOrNestedPublic(this TypeDefinition arg) =>
        arg.IsPublic ||
        arg.IsNestedPublic &&
        arg.DeclaringType.IsPublicOrNestedPublic();

    public static bool IsOverrideOrImplementationOfPublicMember(this MethodDefinition member) =>
        member.EnumerateOverridesAndImplementations().Any(_ => _.IsPublicVisible());

    static bool IsPublicVisible(this MethodDefinition member)
    {
        var type = member.DeclaringType.Resolve();

        return type.IsPublicOrNestedPublic() &&
               (type.IsInterface || member.IsPublic);
    }

    public static void AddRange<T>(this IList<T> collection, IEnumerable<T> values)
    {
        foreach (var value in values)
        {
            collection.Add(value);
        }
    }
}