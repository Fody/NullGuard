using System;

#nullable enable

// Roslyn compiler adds [NullableContext(2)] because majority of methods use nullable reference types

// [NullableContext(2)]
// [Nullable(0)]
public class ClassWithNullableContext2
{
    // [NullableContext(1)]
    public void SomeMethod(string nonNullArg, /* [Nullable(2)] */ string? nullArg)
    {
        Console.WriteLine(nonNullArg);
    }

    // [NullableContext(1)]
    public string MethodWithReturnValue(bool returnNull)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return returnNull ? null : "";
#pragma warning restore CS8603 // Possible null reference return.
    }

    public string? MethodAllowsNullReturnValue()
    {
        return null;
    }

    public static string? StaticMethodAllowsNullReturnValue(string nonNullArg)
    {
        return null;
    }

    public void AnotherMethod(string? nullArg)
    {

    }

    public void AndAnotherMethod(string? nullArg)
    {

    }

    public string? AndEventAnotherMethod(string? nullArg)
    {
        return nullArg;
    }

    public string? NullProperty { get; set; }

/*
    [Nullable(1)]
    public string NonNullableProperty
    {
        [NullableContext(1), CompilerGenerated]
        get
        {
            return this.\u003CNonNullableProperty\u003Ek__BackingField;
        }
        [NullableContext(1), CompilerGenerated]
        set
        {
            this.\u003CNonNullableProperty\u003Ek__BackingField = value;
        }
    }
*/
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
