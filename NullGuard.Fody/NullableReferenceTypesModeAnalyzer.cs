using System.Linq;

using Mono.Cecil;

public class NullableReferenceTypesModeAnalyzer : INullabilityAnalyzer
{
    // https://github.com/dotnet/roslyn/blob/master/docs/features/nullable-metadata.md
    const string NullableContextAttributeTypeName = "NullableContextAttribute";
    const string NullableAttributeTypeName = "NullableAttribute";
    const byte NullableOblivious = 0;
    const byte NullableNotAnnotated = 1;
    const byte NullableAnnotated = 2;
    const string SystemByteFullTypeName = "System.Byte";

    public bool AllowsNull(PropertyDefinition property)
    {
        // For now we don't enforce not-null on properties, as it is legal when using nullable reference types
        // to allow constructor to assign default! to a non-null property
        var typeHasNullableContextAttribute = property.DeclaringType.CustomAttributes
            .Any(a => a.AttributeType.Name == NullableContextAttributeTypeName);

        return typeHasNullableContextAttribute;
    }

    public bool AllowsNull(ParameterDefinition parameter, MethodDefinition method)
    {
        return GetDefaultNullableContext(method)
            || HasNullableReferenceTypeAnnotation(parameter, NullableAttributeTypeName, NullableAnnotated);
    }

    public bool AllowsNullReturnValue(MethodDefinition method)
    {
        return GetDefaultNullableContext(method)
            || HasNullableReferenceTypeAnnotation(method.MethodReturnType, NullableAttributeTypeName, NullableAnnotated);
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

    static bool HasNullableReferenceTypeAnnotation(ICustomAttributeProvider customAttributeProvider, string attributeTypeName, params byte[] annotations)
    {
        var value = customAttributeProvider.CustomAttributes;

        return value
            .Where(a => a.AttributeType.Name == attributeTypeName)
            .SelectMany(a => a.ConstructorArguments)
            .Where(ca => ca.Type.FullName == SystemByteFullTypeName)
            .Any(ca => annotations.Contains((byte)ca.Value));
    }

    static bool GetDefaultNullableContext(IMemberDefinition member)
    {
        // Class's nullable context is 0 or 2
        var defaultNullable = HasNullableReferenceTypeAnnotation(member.DeclaringType, NullableContextAttributeTypeName, NullableOblivious, NullableAnnotated);

        var nullableContextAttributes = member.CustomAttributes
            .Where(ca => ca.AttributeType.Name == NullableContextAttributeTypeName)
            .SelectMany(a => a.ConstructorArguments)
            .Where(ca => ca.Type.FullName == SystemByteFullTypeName)
            .ToList();

        // Method's nullable context is 0 or 2
        defaultNullable |= nullableContextAttributes
            .Select(ca => (byte)ca.Value)
            .Any(value => value == NullableOblivious || value == NullableAnnotated);

        // Method's nullable context is 1, so force false
        if (nullableContextAttributes
            .Any(ca => (byte)ca.Value == NullableNotAnnotated))
        {
            defaultNullable = false;
        }

        return defaultNullable;
    }
}

