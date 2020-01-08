using System;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;

public class RewritingMethods :
    VerifyBase
{
    [Fact]
    public Task RequiresNonNullArgumentWhenNullableReferenceTypeNotUsed()
    {
        var sample = new NullableReferenceTypeClass();
        var exception = Assert.Throws<ArgumentNullException>(() => { sample.SomeMethod(null, ""); });
        Assert.Equal("nonNullArg", exception.ParamName);
        return Verify(exception.Message);
    }

    [Fact]
    public void AllowsNullWhenNullableReferenceTypeUsed()
    {
        var sample = new NullableReferenceTypeClass();
        sample.SomeMethod("", null);
    }

    [Fact]
    public Task RequiresNonNullMethodReturnValueWhenNullableReferenceTypeNotUsed()
    {
        var sample = new NullableReferenceTypeClass();
        var exception = Assert.Throws<InvalidOperationException>(() => sample.MethodWithReturnValue(true));
        return Verify(exception.Message);
    }

    [Fact]
    public void AllowsNullReturnValueWhenNullableReferenceType()
    {
        var sample = new NullableReferenceTypeClass();
        sample.MethodAllowsNullReturnValue();
    }

    public RewritingMethods(ITestOutputHelper output) :
        base(output)
    {
    }
}