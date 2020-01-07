using System;

public class NullableReferenceTypeClass
{
    /*
    public void SomeMethod(string nonNullArg, [Nullable(2)] string nullArg)
    {
        Console.WriteLine(nonNullArg);
    }
    */
    public void SomeMethod(string nonNullArg, string? nullArg)
    {
        Console.WriteLine(nonNullArg);
    }

    public string MethodWithReturnValue(bool returnNull)
    {
        return (returnNull ? null : "")!;
    }

    /*
    [NullableContext(2)]
    public string MethodAllowsNullReturnValue()
    {
        return (string) null;
    }
    */
    public string? MethodAllowsNullReturnValue()
    {
        return null;
    }
}
