using System;

public class ClassWithImplicitInterfaceImplementation : IComparable<string>, IComparable<int>
{
    public int CompareTo(string other)
    {
        return 0;
    }

    public int CompareTo(int other)
    {
        return 0;
    }
}