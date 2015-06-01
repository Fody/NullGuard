using System;
using NullGuard;

public class SimpleClass
{
    public string Field1;

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

    public SimpleClass(SimpleClass sc, SimpleClass sc1) : this(sc.Field1, null)
    {
    }

    public SimpleClass(SimpleClass sc, SimpleClass sc1, SimpleClass sc2) : this(sc.Field1, sc2.NullProperty)
    {
    }

    public SimpleClass(SimpleClass sc, string notUsed, string used) : this (sc.NullProperty, used)
    {
    }

    public SimpleClass(string astr, string str2, string str3) : this(int.Parse(astr).ToString(), null)
    {
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

    public void MethodWithGenericRef<T>(ref T returnNull)
    {
    }

    [return: AllowNull]
    public string MethodAllowsNullReturnValue()
    {
        return null;
    }

    [CanBeNull]
    public string MethodWithCanBeNullResult()
    {
        return null;
    }

    public void MethodWithOutValue(out string nonNullOutArg)
    {
        nonNullOutArg = null;
    }

    public void MethodWithAllowedNullOutValue([AllowNull]out string nonNullOutArg)
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

    public void MethodWithOptionalParameter(string optional = null)
    {
    }

    public void MethodWithOptionalParameterWithNonNullDefaultValue(string optional = "default")
    {
    }

    public void MethodWithOptionalParameterWithNonNullDefaultValueButAllowNullAttribute([AllowNull] string optional = "default")
    {
    }

    public void MethodWithGenericOut<T>(out T item)
    {
        item = default(T);
    }

    public T MethodWithGenericReturn<T>(bool returnNull)
    {
        return returnNull ? default(T) : Activator.CreateInstance<T>();
    }

    public object MethodWithOutAndReturn(out string prefix)
    {
        prefix = null;
        return null;
    }

    public void MethodWithExistingArgumentGuard(string x)
    {
        if (String.IsNullOrEmpty(x))
            throw new ArgumentException("x is null or empty.", "x");

        Console.WriteLine(x);
    }

    public void MethodWithExistingArgumentNullGuard(string x)
    {
        if (String.IsNullOrEmpty(x))
            throw new ArgumentNullException("x");

        Console.WriteLine(x);
    }

    public void MethodWithExistingArgumentNullGuardWithMessage(string x)
    {
        if (String.IsNullOrEmpty(x))
            throw new ArgumentNullException("x", "x is null or empty.");

        Console.WriteLine(x);
    }
}