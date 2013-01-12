using System;
using NullGuard;

public class SimpleClass
{
    public SimpleClass()
    {
    }

    // Why would anyone place an out parameter on a ctor?! I don't know, but I'll support your idiocy.
    public SimpleClass(out string nonNullOutArg)
    {
        nonNullOutArg = null;
    }

    public SimpleClass(string nonNullArg, [AllowNull] string nullArg)
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

    public void MethodWithRef(ref object returnNull)
    {
    }

    public void MethodWithGeneric<T>(T returnNull)
    {
    }

    public void MethodWithGenericRef<T>(T returnNull) where T : MyAbstractClass
    {
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
        Console.WriteLine(x);
    }

    public void MethodWithTwoRefs(ref string first, ref string second)
    {
    }

    public void MethodWithTwoOuts(out string first, out string second)
    {
        first = null;
        second = null;
    }

    public void MethodWithGenericOut<T>(out T item)
    {
        item = default(T);
    }

    public object MethodWithOutAndReturn(out string prefix)
    {
        prefix = null;
        return null;
    }
}