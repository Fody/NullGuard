using System;

public class ClassWithExplicitAndImplicitInterfaceImplementation : IComparable<string>, IComparable<int>
{
    int IComparable<string>.CompareTo(string other)
    {
        return 0;
    }

    public int CompareTo(string other)
    {
        return 0;
    }

    public int CompareTo(int other)
    {
        return 0;
    }
}