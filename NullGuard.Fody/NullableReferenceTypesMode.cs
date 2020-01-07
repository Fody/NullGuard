using System.Linq;

using Mono.Cecil;

public class NullableReferenceTypesMode : INullabilityAnalyzer
{
    // https://github.com/dotnet/roslyn/blob/master/docs/features/nullable-metadata.md
    const string NullableContextAttributeTypeName = "NullableContextAttribute";
    const string NullableAttributeTypeName = "NullableAttribute";
    const byte NullableAnnotated = 2;
    const string SystemByteFullTypeName = "System.Byte";

    public bool AllowsNull(PropertyDefinition property)
    {
        return HasNullableReferenceType(property, NullableAttributeTypeName);
    }

    public bool AllowsNull(ParameterDefinition parameter, MethodDefinition method)
    {
        return HasNullableReferenceType(parameter, NullableAttributeTypeName);
    }

    public bool AllowsNullReturnValue(MethodDefinition method)
    {
        return HasNullableReferenceType(method, NullableContextAttributeTypeName);
    }

    public bool AllowsGetMethodToReturnNull(PropertyDefinition property, MethodReturnType getMethod)
    {
        // TODO: check MaybeNullAttribute
        return false;
    }

    public bool AllowsSetMethodToAcceptNull(PropertyDefinition property, MethodDefinition setMethod, ParameterDefinition valueParameter)
    {
        // TODO: check AllowNullAttribute
        return false;
    }

    static bool HasNullableReferenceType(ICustomAttributeProvider target, string attributeTypeName)
    {
        var attributes = target.CustomAttributes;

        return attributes
            .Where(a => a.AttributeType.Name == attributeTypeName)
            .SelectMany(a => a.ConstructorArguments)
            .Where(ca => ca.Type.FullName == SystemByteFullTypeName)
            .Any(ca => (byte) ca.Value == NullableAnnotated);
    }
}

