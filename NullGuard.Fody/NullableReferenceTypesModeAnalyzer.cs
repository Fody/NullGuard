// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
#nullable enable

using System;
using System.Linq;

using Mono.Cecil;

public class NullableReferenceTypesModeAnalyzer : INullabilityAnalyzer
{
    // https://github.com/dotnet/roslyn/blob/master/docs/features/nullable-metadata.md
    const string NullableContextAttributeTypeName = "System.Runtime.CompilerServices.NullableContextAttribute";
    const string NullableAttributeTypeName = "System.Runtime.CompilerServices.NullableAttribute";

    // Input Preconditions:

    const string AllowNullAttributeTypeName = "System.Diagnostics.CodeAnalysis.AllowNullAttribute";
    const string DisallowNullAttributeTypeName = "System.Diagnostics.CodeAnalysis.DisallowNullAttribute";

    // Output Postconditions:

    // Ignore the following attributes since they are conditional and thus the value may still be null under some
    // circumstances but we eventually want to add checks for these as well:
    // - System.Diagnostics.CodeAnalysis.NotNullIfNotNull
    // - System.Diagnostics.CodeAnalysis.NotNullWhen

    const string NotNullAttributeTypeName = "System.Diagnostics.CodeAnalysis.NotNull";

    // Treat all MaybeNull attributes as marking the type as nullable:

    const string MaybeNullAttributeTypeName = "System.Diagnostics.CodeAnalysis.MaybeNull";
    const string MaybeNullWhenAttributeTypeName = "System.Diagnostics.CodeAnalysis.MaybeNullWhen";

    enum Nullable
    {
        Unknown = -1,
        Oblivious = 0,
        NotAnnotated = 1,
        Annotated = 2
    }

    public bool AllowsNull(PropertyDefinition property)
    {
        // => do all checks for get/set method
        return false;
    }

    public bool AllowsNullInput(ParameterDefinition parameter, MethodDefinition method)
    {
        return GetItemPreconditionAllowsNull(parameter)
               ?? GetItemAllowsNull(parameter)
               ?? GetContextAllowsNull(method)
               ?? true;
    }

    public bool AllowsNullOutput(ParameterDefinition parameter, MethodDefinition method)
    {
        return GetItemPostconditionAllowsNull(parameter)
               ?? GetItemAllowsNull(parameter)
               ?? GetContextAllowsNull(method)
               ?? true;
    }

    public bool AllowsNullReturnValue(MethodDefinition method)
    {
        return GetItemPostconditionAllowsNull(method.MethodReturnType)
               ?? GetItemAllowsNull(method.MethodReturnType)
               ?? GetContextAllowsNull(method)
               ?? true;
    }

    public bool AllowsGetMethodToReturnNull(PropertyDefinition property, MethodDefinition getMethod)
    {
        return GetItemPostconditionAllowsNull(getMethod.MethodReturnType)
               ?? GetItemAllowsNull(getMethod.MethodReturnType)
               ?? GetContextAllowsNull(getMethod)
               ?? true;
    }

    public bool AllowsSetMethodToAcceptNull(PropertyDefinition property, MethodDefinition setMethod, ParameterDefinition valueParameter)
    {
        return GetItemPreconditionAllowsNull(valueParameter)
               ?? GetItemAllowsNull(valueParameter)
               ?? GetContextAllowsNull(setMethod)
               ?? true;
    }

    static bool? GetItemPreconditionAllowsNull(ICustomAttributeProvider customAttributeProvider)
    {
        return ContainsAttribute(customAttributeProvider, AllowNullAttributeTypeName) ? true :
               ContainsAttribute(customAttributeProvider, DisallowNullAttributeTypeName) ? false :
               (bool?)null;
    }

    static bool? GetItemPostconditionAllowsNull(ICustomAttributeProvider customAttributeProvider)
    {
        return ContainsAnyAttribute(customAttributeProvider, MaybeNullAttributeTypeName, MaybeNullWhenAttributeTypeName) ? true :
               ContainsAttribute(customAttributeProvider, NotNullAttributeTypeName) ? false :
               (bool?) null;
    }

    static bool? GetItemAllowsNull(ICustomAttributeProvider customAttributeProvider)
    {
        return GetAllowsNull(customAttributeProvider, NullableAttributeTypeName);
    }

    static bool? GetContextAllowsNull(MethodDefinition method)
    {
        return GetAllowsNull(method, NullableContextAttributeTypeName)
               ?? GetContextAllowsNull(method.DeclaringType);
    }

    static bool? GetContextAllowsNull(TypeDefinition type)
    {
        if (type == null)
            return null;

        return GetAllowsNull(type, NullableContextAttributeTypeName)
               ?? GetContextAllowsNull(type.DeclaringType);
    }

    static bool ContainsAttribute(ICustomAttributeProvider customAttributeProvider, string fullName)
    {
        return customAttributeProvider.CustomAttributes.Any(a => a.AttributeType.FullName == fullName);
    }

    static bool ContainsAnyAttribute(ICustomAttributeProvider customAttributeProvider, params string[] fullNames)
    {
        return fullNames.Any(n => ContainsAttribute(customAttributeProvider, n));
    }

    static bool? GetAllowsNull(ICustomAttributeProvider customAttributeProvider, string attributeTypeName)
    {
        var nullable = GetNullableAnnotation(customAttributeProvider, attributeTypeName);

        return nullable switch
        {
            Nullable.NotAnnotated => false,
            Nullable.Annotated => true,
            Nullable.Oblivious => true,
            // compiler bug? => omitting the "(bool?)" makes everything fail.
            _ => default(bool?)
        };
    }

    static Nullable GetNullableAnnotation(ICustomAttributeProvider customAttributeProvider, string attributeTypeName)
    {
        return (customAttributeProvider.CustomAttributes.Where(a => a.AttributeType.FullName == attributeTypeName && a.ConstructorArguments.Count == 1)
            .Select(a => a.ConstructorArguments[0])
            .Select(GetConstructorArgumentValue)
            .DefaultIfEmpty(Nullable.Unknown)
            .Single());
    }

    static Nullable GetConstructorArgumentValue(CustomAttributeArgument arg, int index = 0)
    {
        var value = arg.Type.FullName switch
        {
            "System.Byte" => (byte)arg.Value,
            "System.Byte[]" => (byte)(((CustomAttributeArgument[])arg.Value).Take(index + 1).Last().Value),
            _ => throw new InvalidOperationException("unexpected type: " + arg.Type.FullName)
        };

        return (Nullable)value;
    }
}