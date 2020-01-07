using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable
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
#pragma warning disable CS8603 // Possible null reference return.
        return returnNull ? null : "";
#pragma warning restore CS8603 // Possible null reference return.
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
#nullable disable
