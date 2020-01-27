using System;

using VerifyXunit;

using Xunit;
using Xunit.Abstractions;

public class SystemNullMessage :
    VerifyBase
{
    public SystemNullMessage(ITestOutputHelper output) :
        base(output)
    {
    }

    [Fact]
    public void UsesSystemDefaultArgumentNullExceptionMessage()
    {
        var sample = new ClassWithNullableContext2();
        var exception = Assert.Throws<ArgumentNullException>(() => sample.SomeMethod(null, null));

        Assert.Equal("nonNullArg", exception.ParamName);
        Assert.DoesNotContain("[", exception.Message); // ensure '[NullGuard]' is not in the message string
    }
}
