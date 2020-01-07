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
        var type = AssemblyWeaver.Assembly.GetType(nameof(NullableReferenceTypeClass));
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() => { sample.SomeMethod(null, ""); });
        Assert.Equal("nonNullArg", exception.ParamName);
        return Verify(exception.Message);
    }

    [Fact]
    public void AllowsNullWhenNullableReferenceTypeUsed()
    {
        var type = AssemblyWeaver.Assembly.GetType(nameof(NullableReferenceTypeClass));
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.SomeMethod("", null);
    }

    [Fact]
    public Task RequiresNonNullMethodReturnValueWhenNullableReferenceTypeNotUsed()
    {
        var type = AssemblyWeaver.Assembly.GetType(nameof(NullableReferenceTypeClass));
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<InvalidOperationException>(() => sample.MethodWithReturnValue(true));
        return Verify(exception.Message);
    }

    [Fact]
    public void AllowsNullReturnValueWhenNullableReferenceType()
    {
        var type = AssemblyWeaver.Assembly.GetType(nameof(NullableReferenceTypeClass));
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.MethodAllowsNullReturnValue();
    }

    public RewritingMethods(ITestOutputHelper output) :
        base(output)
    {
    }
}