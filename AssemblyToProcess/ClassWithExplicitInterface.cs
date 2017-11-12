using System;

using NullGuard;

public class ClassWithExplicitInterface : IComparable<string>
{
    int IComparable<string>.CompareTo(string other)
    {
        return 0;
    }

    public int CallInteralClassWithPrivateInterface([AllowNull] string other)
    {
        return ((IPrivate) new ClassWithExplicitPrivateInterface()).CompareTo(other);
    }

    public int CallInteralClassWithPublicInterface([AllowNull] string other)
    {
        return ((IComparable<string>) new ClassWithExplicitPublicInterface()).CompareTo(other);
    }


    private interface IPrivate
    {
        int CompareTo(string other);
    }

    private class ClassWithExplicitPrivateInterface : IPrivate
    {
        int IPrivate.CompareTo(string other1)
        {
            return 0;
        }
    }

    private class ClassWithExplicitPublicInterface : IComparable<string>
    {
        int IComparable<string>.CompareTo(string other1)
        {
            return 1;
        }
    }
}