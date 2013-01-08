using System;
using NullGuard;

public class SampleClass
{
    public SampleClass()
    {
    }

    // Why would anyone place an out parameter on a ctor?! I don't know, but I'll support your idiocy.
    public SampleClass(out string nonNullOutArg)
    {
        nonNullOutArg = null;
    }

    public SampleClass(string nonNullArg, [AllowNull] string nullArg)
    {
        Console.WriteLine(nonNullArg + " " + nullArg);
    }

    public void SomeMethod(string nonNullArg, [AllowNull] string nullArg)
    {
        Console.WriteLine(nonNullArg);
    }

    public string NonNullProperty { get; set; }

    [AllowNull]
    public string NullProperty { get; set; }

    public string PropertyAllowsNullGetButDoesNotAllowNullSet { [return: AllowNull] get; set; }

    public string PropertyAllowsNullSetButDoesNotAllowNullGet { get; [param: AllowNull] set; }

    public int? NonNullNullableProperty { get; set; }

    public string MethodWithReturnValue(bool returnNull)
    {
        return returnNull ? null : "";
    }

    [return: AllowNull]
    public string MethodAllowsNullReturnValue()
    {
        return null;
    }

    public void MethodWithOutValue(out string nonNullOutArg)
    {
        nonNullOutArg = null;
    }

    public void PublicWrapperOfPrivateMethod()
    {
        SomePrivateMethod(null);
    }

    private void SomePrivateMethod(string x)
    {
    }
}