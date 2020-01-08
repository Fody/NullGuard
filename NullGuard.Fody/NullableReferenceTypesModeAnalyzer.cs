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

    public bool AllowsNull(PropertyDefinition property)
    {
        // => do all checks for get/set method
        return false; 
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
        return GetDefaultNullableContext(getMethod);
    }

    public bool AllowsSetMethodToAcceptNull(PropertyDefinition property, MethodDefinition setMethod, ParameterDefinition valueParameter)
    {
        // TODO: check AllowNullAttribute
        return GetDefaultNullableContext(setMethod);
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

