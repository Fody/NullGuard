using System.Linq;

using Mono.Cecil;

public class NullableReferenceTypesModeAnalyzer : INullabilityAnalyzer
{
    // https://github.com/dotnet/roslyn/blob/master/docs/features/nullable-metadata.md
    const string NullableContextAttributeTypeName = "System.Runtime.CompilerServices.NullableContextAttribute";
    const string NullableAttributeTypeName = "System.Runtime.CompilerServices.NullableAttribute";
    const string SystemByteFullTypeName = "System.Byte";

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
        return GetDefaultNullableContext(method)
            || GetNullableAnnotation(parameter, NullableAttributeTypeName) == Nullable.Annotated;
    }

    public bool AllowsNullReturnValue(MethodDefinition method)
    {
        return GetDefaultNullableContext(method)
            || GetNullableAnnotation(method.MethodReturnType, NullableAttributeTypeName) == Nullable.Annotated;
    }

    public bool AllowsGetMethodToReturnNull(PropertyDefinition property, MethodDefinition getMethod)
    {
        // TODO: check MaybeNullAttribute
        return GetDefaultNullableContext(getMethod);
    }

    public bool AllowsSetMethodToAcceptNull(PropertyDefinition property, MethodDefinition setMethod, ParameterDefinition valueParameter)
    {
        // TODO: check AllowNullAttribute
        return GetDefaultNullableContext(setMethod);
    }

    static Nullable GetNullableAnnotation(ICustomAttributeProvider customAttributeProvider, string attributeTypeName)
    {
        return customAttributeProvider.CustomAttributes.Where(a => a.AttributeType.FullName == attributeTypeName)
            .SelectMany(a => a.ConstructorArguments)
            .Where(ca => ca.Type.FullName == SystemByteFullTypeName)
            .Select(ca => (Nullable)(byte)ca.Value)
            .DefaultIfEmpty(Nullable.Unknown)
            .Single();
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

