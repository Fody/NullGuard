using System;

using NullGuard;

public class ClassWithImplicitInterface : IComparable<string>
{
    public int CompareTo(string other)
    {
        return 0;
    }

    public int CallInteralClassWithPrivateInterface([AllowNull] string other)
    {
        return new ClassWithImplicitPrivateInterface().CompareTo(other);
    }

    public int CallInteralClassWithPublicInterface([AllowNull] string other)
    {
        return new ClassWithImplicitPublicInterface().CompareTo(other);
    }

    private interface IPrivate
    {
        int CompareTo(string other);
    }

    private class ClassWithImplicitPrivateInterface : IPrivate
    {
        public int CompareTo(string other1)
        {
            return 0;
        }
    }

    private class ClassWithImplicitPublicInterface : IComparable<string>
    {
        public int CompareTo(string other1)
        {
            return 0;
        }
    }
}