using System.Collections.Generic;
using Mono.Cecil;

class MemberReferenceEqualityComparer : IEqualityComparer<MemberReference>
{
    MemberReferenceEqualityComparer()
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

    static string GetKey(MemberReference obj)
    {
        if (obj == null)
            return null;

        return GetAssemblyName(obj.DeclaringType.Scope) + "|" + obj.FullName;
    }

    static string GetAssemblyName(IMetadataScope scope)
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