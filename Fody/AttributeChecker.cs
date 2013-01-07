using System.Linq;
using Mono.Cecil;

public static class AttributeChecker
{
    public static bool ContainsAllowNullAttribute(this ICustomAttributeProvider definition)
    {
        var customAttributes = definition.CustomAttributes;

        return customAttributes.Any(x => x.AttributeType.Name == "AllowNullAttribute");
    }

    public static void RemoveAllowNullAttribute(this ICustomAttributeProvider definition)
    {
        var customAttributes = definition.CustomAttributes;

        var attribute = customAttributes.FirstOrDefault(x => x.AttributeType.Name == "AllowNullAttribute");

        if (attribute != null)
        {
            customAttributes.Remove(attribute);
        }

    }
}