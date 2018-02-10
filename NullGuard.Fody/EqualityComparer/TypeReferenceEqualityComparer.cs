using System.Collections.Generic;
using Mono.Cecil;

class TypeReferenceEqualityComparer : IEqualityComparer<TypeReference>
{
    TypeReferenceEqualityComparer()
    {
    }

    public static IEqualityComparer<TypeReference> Default { get; } = new TypeReferenceEqualityComparer();

    public bool Equals(TypeReference x, TypeReference y)
    {
        var typeDefinition = x.Resolve();
        return GetKey(x) == GetKey(y);
    }

    public int GetHashCode(TypeReference obj)
    {
        return GetKey(obj)?.GetHashCode() ?? 0;
    }

    static string GetKey(TypeReference obj)
    {
        if (obj == null)
            return null;

        return GetAssemblyName(obj.Scope) + "|" + obj.FullName;
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