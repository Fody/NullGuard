using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;

public static class CecilExtensions
{
    public static IEnumerable<MethodDefinition> AbstractMethods(this TypeDefinition type)
    {
        return type.Methods.Where(x => x.IsAbstract);
    }

    public static IEnumerable<MethodDefinition> MethodsWithBody(this TypeDefinition type)
    {
        return type.Methods.Where(x => x.Body != null);
    }

    public static IEnumerable<PropertyDefinition> AbstractProperties(this TypeDefinition type)
    {
        return type.Properties.Where(x => (x.GetMethod != null && x.GetMethod.IsAbstract) || (x.SetMethod != null && x.SetMethod.IsAbstract));
    }

    public static IEnumerable<PropertyDefinition> ConcreteProperties(this TypeDefinition type)
    {
        return type.Properties.Where(x => (x.GetMethod == null || !x.GetMethod.IsAbstract) && (x.SetMethod == null || !x.SetMethod.IsAbstract));
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
        return value.CustomAttributes.Any(a => a.AttributeType.Name == "AllowNullAttribute" || a.AttributeType.Name == "CanBeNullAttribute");
    }

    public static bool MayNotBeNull(this ParameterDefinition arg)
    {
        return !arg.AllowsNull() && !arg.IsOptional && arg.ParameterType.IsRefType() && !arg.IsOut;
    }

    public static bool IsRefType(this TypeReference arg)
    {
        if (arg.IsValueType)
        {
            return false;
        }
        var byReferenceType = arg as ByReferenceType;
        if (byReferenceType != null && byReferenceType.ElementType.IsValueType)
        {
            return false;
        }

        var pointerType = arg as PointerType;
        if (pointerType != null && pointerType.ElementType.IsValueType)
        {
            return false;
        }

        return true;
    }

    public static bool IsCompilerGenerated(this ICustomAttributeProvider value)
    {
        return value.CustomAttributes.Any(a => a.AttributeType.Name == "CompilerGeneratedAttribute");
    }

    public static bool IsAsyncStateMachine(this ICustomAttributeProvider value)
    {
        return value.CustomAttributes.Any(a => a.AttributeType.Name == "AsyncStateMachineAttribute");
    }

    public static bool IsIteratorStateMachine(this ICustomAttributeProvider value)
    {
        // Only works on VB not C# but it's something.
        return value.CustomAttributes.Any(a => a.AttributeType.Name == "IteratorStateMachineAttribute");
    }
}