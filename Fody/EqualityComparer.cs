using System.Collections.Generic;

using Mono.Cecil;

internal class TypeReferenceEqualityComparer : IEqualityComparer<TypeReference>
{
    private TypeReferenceEqualityComparer()
    {
    }

    public static IEqualityComparer<TypeReference> Default { get; } = new TypeReferenceEqualityComparer();

    public bool Equals(TypeReference x, TypeReference y)
    {
        return GetKey(x) == GetKey(y);
    }

    public int GetHashCode(TypeReference obj)
    {
        return GetKey(obj)?.GetHashCode() ?? 0;
    }

    private static string GetKey(TypeReference obj)
    {
        if (obj == null)
            return null;

        return GetAssemblyName(obj.Scope) + "|" + obj.FullName;
    }

    private static string GetAssemblyName(IMetadataScope scope)
    {
        if (scope == null)
            return null;

        if (scope is ModuleDefinition md)
        {
            return md.Assembly.FullName;
        }

        return scope.ToString();
    }
}

internal class MemberReferenceEqualityComparer : IEqualityComparer<MemberReference>
{
    private MemberReferenceEqualityComparer()
    {
    }

    public static IEqualityComparer<MemberReference> Default { get; } = new MemberReferenceEqualityComparer();

    public bool Equals(MemberReference x, MemberReference y)
    {
        return GetKey(x) == GetKey(y);
    }

    public int GetHashCode(MemberReference obj)
    {
        return GetKey(obj)?.GetHashCode() ?? 0;
    }

    private static string GetKey(MemberReference obj)
    {
        if (obj == null)
            return null;

        return GetAssemblyName(obj.DeclaringType.Scope) + "|" + obj.FullName;
    }

    private static string GetAssemblyName(IMetadataScope scope)
    {
        if (scope == null)
            return null;

        if (scope is ModuleDefinition md)
        {
            return md.Assembly.FullName;
        }

        return scope.ToString();
    }
}

