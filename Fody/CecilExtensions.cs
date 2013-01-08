using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;

public static class CecilExtensions
{
    public static IEnumerable<MethodDefinition> AbstractMethods(this TypeDefinition type)
    {
        return type.Methods.Where(x => x.IsAbstract);
    }

    public static IEnumerable<MethodDefinition> ConcreteMethods(this TypeDefinition type)
    {
        return type.Methods.Where(x => !x.IsAbstract);
    }

    public static IEnumerable<PropertyDefinition> AbstractProperties(this TypeDefinition type)
    {
        return type.Properties.Where(x => x.GetMethod.IsAbstract || x.SetMethod.IsAbstract);
    }

    public static IEnumerable<PropertyDefinition> ConcreteProperties(this TypeDefinition type)
    {
        return type.Properties.Where(x => !x.GetMethod.IsAbstract && !x.SetMethod.IsAbstract);
    }


    public static CustomAttribute GetNullGuardAttribute(this ICustomAttributeProvider value)
    {
        return value.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "NullGuardAttribute");
    }

    public static bool IsProperty(this MethodDefinition method)
    {
        return method.IsSpecialName && (method.Name.StartsWith("set_") || method.Name.StartsWith("get_"));
    }

    public static bool AllowsNull(this ICustomAttributeProvider value)
    {
        return value.CustomAttributes.Any(a => a.AttributeType.Name == "AllowNullAttribute");
    }

    public static bool MayNotBeNull(this ParameterDefinition arg)
    {
        return !arg.AllowsNull() && !arg.IsOptional && !arg.ParameterType.IsValueType && !arg.IsOut;
    }
}