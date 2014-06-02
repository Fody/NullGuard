using System;

public class ClassWithExplicitInterface : IComparable<string>
{
    int IComparable<string>.CompareTo(string other)
    {
        return 0;
    }
}