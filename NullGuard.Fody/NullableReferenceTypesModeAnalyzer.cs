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
        return GetAllowsNull(parameter, NullableAttributeTypeName)
            ?? GetAllowsNull(method, NullableContextAttributeTypeName)
            ?? GetAllowsNull(method.DeclaringType, NullableContextAttributeTypeName)
            ?? true;
    }

    public bool AllowsNullReturnValue(MethodDefinition method)
    {
        return GetAllowsNull(method.MethodReturnType, NullableAttributeTypeName)
            ?? GetAllowsNull(method, NullableContextAttributeTypeName)
            ?? GetAllowsNull(method.DeclaringType, NullableContextAttributeTypeName)
            ?? true;
    }

    public bool AllowsGetMethodToReturnNull(PropertyDefinition property, MethodDefinition getMethod)
    {
        return GetAllowsNull(getMethod.MethodReturnType, NullableAttributeTypeName)
            ?? GetAllowsNull(getMethod, NullableContextAttributeTypeName)
            ?? GetAllowsNull(getMethod.DeclaringType, NullableContextAttributeTypeName)
            ?? true;
    }

    public bool AllowsSetMethodToAcceptNull(PropertyDefinition property, MethodDefinition setMethod, ParameterDefinition valueParameter)
    {
        return GetAllowsNull(valueParameter, NullableAttributeTypeName)
            ?? GetAllowsNull(setMethod, NullableContextAttributeTypeName)
            ?? GetAllowsNull(setMethod.DeclaringType, NullableContextAttributeTypeName)
            ?? true;
    }

    static Nullable GetNullableAnnotation(ICustomAttributeProvider customAttributeProvider, string attributeTypeName)
    {
        return customAttributeProvider.CustomAttributes.Where(a => a.AttributeType.FullName == attributeTypeName && a.ConstructorArguments.Count == 1)
            .Select(a => a.ConstructorArguments[0])
            .Select(GetConstructorArgumentValue)
            .DefaultIfEmpty(Nullable.Unknown)
            .Single();
    }

    static Nullable GetConstructorArgumentValue(CustomAttributeArgument arg, int index = 0)
    {
        switch (arg.Type.FullName)
        {
            case "System.Byte":
                return (Nullable)(byte)arg.Value;

            case "System.Byte[]":
                return (Nullable)(byte)((CustomAttributeArgument[])arg.Value).Take(index + 1).Last().Value;

            default:
                throw new InvalidOperationException("unexpected type: " + arg.Type.FullName);
        }
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
}

