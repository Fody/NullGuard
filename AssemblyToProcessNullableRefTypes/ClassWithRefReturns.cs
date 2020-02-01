using System;

public class ClassWithRefReturns
{
    private string? _value = null;

    public ref string GetNonNullRef()
    {
        return ref _value!;
    }

    public ref string? GetNullRef()
    {
        return ref _value;
    }

    public class Generic<T>
    {
        private T _value = default!;

        public ref T GetMaybeNullUnconstrainedRef()
        {
            return ref _value;
        }
    }

    #if NETSTANDARD2_1
    public class GenericNonNull<T> where T : notnull
    {
        private T _value = default!;

        public ref T GetNonNullRef()
        {
            return ref _value;
        }
    }
    #endif
}

