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
        switch (GetNullableAnnotation(parameter, NullableAttributeTypeName))
        {
            case Nullable.NotAnnotated:
                return false;
            case Nullable.Annotated:
            case Nullable.Oblivious:
                return true;
        }

        return GetDefaultNullableContext(method);
    }

    public bool AllowsNullReturnValue(MethodDefinition method)
    {
        switch (GetNullableAnnotation(method.MethodReturnType, NullableAttributeTypeName))
        {
            case Nullable.NotAnnotated:
                return false;
            case Nullable.Annotated:
            case Nullable.Oblivious:
                return true;
        }

        return GetDefaultNullableContext(method);
    }

    public bool AllowsGetMethodToReturnNull(PropertyDefinition property, MethodDefinition getMethod)
    {
        switch (GetNullableAnnotation(getMethod.MethodReturnType, NullableAttributeTypeName))
        {
            case Nullable.NotAnnotated:
                return false;
            case Nullable.Annotated:
            case Nullable.Oblivious:
                return true;
        }

        // TODO: check MaybeNullAttribute
        return GetDefaultNullableContext(getMethod);
    }

    public bool AllowsSetMethodToAcceptNull(PropertyDefinition property, MethodDefinition setMethod, ParameterDefinition valueParameter)
    {
        switch (GetNullableAnnotation(valueParameter, NullableAttributeTypeName))
        {
            case Nullable.NotAnnotated:
                return false;
            case Nullable.Annotated:
            case Nullable.Oblivious:
                return true;
        }

        // TODO: check AllowNullAttribute
        return GetDefaultNullableContext(setMethod);
    }

    static Nullable GetNullableAnnotation(ICustomAttributeProvider customAttributeProvider, string attributeTypeName)
    {
        return customAttributeProvider.CustomAttributes.Where(a => a.AttributeType.FullName == attributeTypeName && a.ConstructorArguments.Count == 1)
            .Select(a => a.ConstructorArguments[0])
            .Select(GetConstructorArgumentValue)
            .DefaultIfEmpty(Nullable.Unknown)
            .Single();
    }

    private static Nullable GetConstructorArgumentValue(CustomAttributeArgument arg)
    {
        switch (arg.Type.FullName)
        {
            case "System.Byte":
                return (Nullable)(byte)arg.Value;

            case "System.Byte[]":
                return (Nullable)(byte)((CustomAttributeArgument[])arg.Value)[0].Value;

            default:
                throw new InvalidOperationException("unexpected type: " + arg.Type.FullName);
        }
    }

    static bool GetDefaultNullableContext(IMemberDefinition member)
    {
        var nullableContext = GetNullableAnnotation(member, NullableContextAttributeTypeName);

        switch (nullableContext)
        {
            case Nullable.NotAnnotated:
                return false;
            case Nullable.Annotated:
            case Nullable.Oblivious:
                return true;
        }

        var defaultContext = GetNullableAnnotation(member.DeclaringType, NullableContextAttributeTypeName);

        switch (defaultContext)
        {
            case Nullable.NotAnnotated:
                return false;
            case Nullable.Annotated:
            case Nullable.Oblivious:
                return true;
        }

        return true;
    }
}

