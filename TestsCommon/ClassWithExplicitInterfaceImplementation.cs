using System;

public class ClassWithExplicitInterfaceImplementation : IComparable<string>, IComparable<int>
{
    int IComparable<string>.CompareTo(string other)
    {
        return 0;
    }

    int IComparable<int>.CompareTo(int other)
    {
        return 0;
    }
}