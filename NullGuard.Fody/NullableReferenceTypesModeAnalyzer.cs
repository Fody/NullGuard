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

    public bool AllowsNull(ParameterDefinition parameter, MethodDefinition method)
    {
        return GetItemAllowsNull(parameter)
               ?? GetContextAllowsNull(method)
               ?? true;
    }

    public bool AllowsNullReturnValue(MethodDefinition method)
    {
        return GetItemAllowsNull(method.MethodReturnType)
               ?? GetContextAllowsNull(method)
               ?? true;
    }

    public bool AllowsGetMethodToReturnNull(PropertyDefinition property, MethodDefinition getMethod)
    {
        return GetItemAllowsNull(getMethod.MethodReturnType)
               ?? GetContextAllowsNull(getMethod)
               ?? true;
    }

    public bool AllowsSetMethodToAcceptNull(PropertyDefinition property, MethodDefinition setMethod, ParameterDefinition valueParameter)
    {
        return GetItemAllowsNull(valueParameter)
               ?? GetContextAllowsNull(setMethod)
               ?? true;
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