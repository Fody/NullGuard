using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

public static class CecilExtensions
{
    public static bool HasInterface(this TypeDefinition type, string interfaceFullName)
    {
        return (type.Interfaces.Any(i => i.FullName.Equals(interfaceFullName))
                || (type.BaseType != null && type.BaseType.Resolve().HasInterface(interfaceFullName)));
    }

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

    public static bool ContainsAllowNullAttribute(this ICustomAttributeProvider definition)
    {
        var customAttributes = definition.CustomAttributes;

        return customAttributes.Any(x => x.AttributeType.Name == "AllowNullAttribute");
    }

    public static void RemoveAllNullGuardAttributes(this ICustomAttributeProvider definition)
    {
        var customAttributes = definition.CustomAttributes;

        var attributes = customAttributes.Where(x => x.AttributeType.Namespace == "NullGuard").ToArray();

        foreach (var attribute in attributes)
        {
            customAttributes.Remove(attribute);
        }
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

    public static bool IsGeneratedCode(this ICustomAttributeProvider value)
    {
        return value.CustomAttributes.Any(a => a.AttributeType.Name == "CompilerGeneratedAttribute" || a.AttributeType.Name == "GeneratedCodeAttribute");
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

    public static bool IsIAsyncStateMachine(this TypeDefinition typeDefinition)
    {
        return typeDefinition.Interfaces.Any(x => x.Name == "IAsyncStateMachine");
    }

    public static void HideLineFromDebugger(this Instruction i, SequencePoint seqPoint)
    {
        if (seqPoint == null)
            return;

        HideLineFromDebugger(i, seqPoint.Document);
    }

    public static void HideLineFromDebugger(this Instruction i, Document doc)
    {
        if (doc == null)
            return;

        // This tells the debugger to ignore and step through
        // all the following instructions to the next instruction
        // with a valid SequencePoint. That way IL can be hidden from
        // the Debugger. See
        // http://blogs.msdn.com/b/abhinaba/archive/2005/10/10/479016.aspx
        i.SequencePoint = new SequencePoint(doc);
        i.SequencePoint.StartLine = 0xfeefee;
        i.SequencePoint.EndLine = 0xfeefee;
    }
}