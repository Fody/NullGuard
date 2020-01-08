using System;

#nullable enable

// Roslyn compiler adds [NullableContext(1)] because majority of methods do not use nullable reference types

// [NullableContext(1)]
// [Nullable(0)]
public class ClassWithNullableContext1
{
    public void SomeMethod(string nonNullArg, /* [Nullable(2)] */ string? nullArg)
    {
        Console.WriteLine(nonNullArg);
    }

    public string MethodWithReturnValue(bool returnNull)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return returnNull ? null : "";
#pragma warning restore CS8603 // Possible null reference return.
    }

    // [NullableContext(2)]
    public string? MethodAllowsNullReturnValue()
    {
        return null;
    }

    public static string? StaticMethodAllowsNullReturnValue(string nonNullArg)
    {
        return null;
    }

    public string AnotherMethod(string nonNullArg)
    {
        return nonNullArg;
    }

    public void AndAnotherMethod(string nonNullArg)
    {

    }

    /*
        [Nullable(2)]
        public string NullProperty
        {
            [NullableContext(2), CompilerGenerated]
            get
            {
                return this.\u003CNullableProperty\u003Ek__BackingField;
            }
            [NullableContext(2), CompilerGenerated]
            set
            {
                this.\u003CNullableProperty\u003Ek__BackingField = value;
            }
        }
    */

    public string? NullProperty { get; set; }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string NonNullProperty { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

#nullable disable
    // [NullableContext(0)]
    public string MethodWithNullableContext0()
    {
        return null;
    }
}
