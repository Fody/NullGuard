using System;

public class ClassWithExplicitInterface : IComparable<string>
{
    int IComparable<string>.CompareTo([NotNull] string other)
    {
        return 0;
    }
}