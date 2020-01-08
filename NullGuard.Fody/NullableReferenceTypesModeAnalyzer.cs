using System.Linq;

using Mono.Cecil;

public class NullableReferenceTypesModeAnalyzer : INullabilityAnalyzer
{
    // https://github.com/dotnet/roslyn/blob/master/docs/features/nullable-metadata.md
    const string NullableContextAttributeTypeName = "System.Runtime.CompilerServices.NullableContextAttribute";
    const string NullableAttributeTypeName = "System.Runtime.CompilerServices.NullableAttribute";
    const int NullableUnknown = -1;
    const int NullableOblivious = 0;
    const int NullableNotAnnotated = 1;
    const int NullableAnnotated = 2;
    const string SystemByteFullTypeName = "System.Byte";
    static readonly int[] NullableMemberArgs = { NullableOblivious, NullableAnnotated };

    public bool AllowsNull(PropertyDefinition property)
    {
        // For now we don't enforce not-null on properties, as it is legal when using nullable reference types
        // to allow constructor to assign default! to a non-null property
        var typeHasNullableContextAttribute = property.DeclaringType.CustomAttributes
            .Any(a => a.AttributeType.FullName == NullableContextAttributeTypeName);

        return typeHasNullableContextAttribute;
    }

    public bool AllowsNull(ParameterDefinition parameter, MethodDefinition method)
    {
        return GetDefaultNullableContext(method)
            || GetNullableAnnotation(parameter, NullableAttributeTypeName) == NullableAnnotated;
    }

    public bool AllowsNullReturnValue(MethodDefinition method)
    {
        return GetDefaultNullableContext(method)
            || GetNullableAnnotation(method.MethodReturnType, NullableAttributeTypeName) == NullableAnnotated;
    }

    public bool AllowsGetMethodToReturnNull(PropertyDefinition property, MethodDefinition getMethod)
    {
        // TODO: check MaybeNullAttribute
        return false;
    }

    public bool AllowsSetMethodToAcceptNull(PropertyDefinition property, MethodDefinition setMethod, ParameterDefinition valueParameter)
    {
        // TODO: check AllowNullAttribute
        return false;
    }

    static int GetNullableAnnotation(ICustomAttributeProvider customAttributeProvider, string attributeTypeName)
    {
        return customAttributeProvider.CustomAttributes.Where(a => a.AttributeType.FullName == attributeTypeName)
            .SelectMany(a => a.ConstructorArguments)
            .Where(ca => ca.Type.FullName == SystemByteFullTypeName)
            .Select(ca => (int)(byte)ca.Value)
            .DefaultIfEmpty(NullableUnknown)
            .Single();
    }

    static bool GetDefaultNullableContext(IMemberDefinition member)
    {
        var nullableContext = GetNullableAnnotation(member, NullableContextAttributeTypeName);

        switch (nullableContext)
        {
            case NullableNotAnnotated:
                return false;
            case NullableAnnotated:
            case NullableOblivious:
                return true;
        }

        var defaultContext = GetNullableAnnotation(member.DeclaringType, NullableContextAttributeTypeName);

        switch (defaultContext)
        {
            case NullableNotAnnotated:
                return false;
            case NullableAnnotated:
            case NullableOblivious:
                return true;
        }

        return true;
    }
}

