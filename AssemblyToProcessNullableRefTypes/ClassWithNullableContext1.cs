#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System;
using System.Diagnostics.CodeAnalysis;

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

    //   [return: Nullable(2)]
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

    [return: MaybeNull]
    public T GenericMaybeNullReturnValue<T>() where T : notnull
    {
        return default!;
    }

    public T UnconstrainedGeneric<T>(T value)
    {
        return default!;
    }

    public T NotNullGeneric<T>(T nonNullArg) where T : notnull
    {
        return default!;
    }

    public void MethodWithManyParameters(string? nullArg1, string nonNullArg2, string? nullArg3, string nonNullArg4)
    {

    }

    // [Nullable(2)]
    // public string NullProperty { [NullableContext(2)] get; [NullableContext(2)] set; }
    public string? NullProperty { get; set; }

    public string NonNullProperty { get; set; }

    // [Nullable(new byte[] {2, 2, 1})]
    // public Tuple<string, string> MixedNullProperty { [return: Nullable(new byte[] {2, 2, 1})] get; [param: Nullable(new byte[] {2, 2, 1})] set; }
    public Tuple<string?, string>? MixedNullProperty { get; set; }

    // [Nullable(new byte[] {1, 2, 1})]
    // public Tuple<string, string> MixedNotNullProperty { [return: Nullable(new byte[] {1, 2, 1})] get; [param: Nullable(new byte[] {1, 2, 1})] set; }
    public Tuple<string?, string> MixedNonNullProperty { get; set; }

#nullable disable
    // [NullableContext(0)]
    public string MethodWithNullableContext0()
    {
        return null;
    }
}
