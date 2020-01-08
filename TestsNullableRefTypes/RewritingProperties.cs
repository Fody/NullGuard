using VerifyXunit;

using Xunit;
using Xunit.Abstractions;

public class RewritingProperties :
    VerifyBase
{
    public RewritingProperties(ITestOutputHelper output) :
        base(output)
    {
    }

    [Fact]
    public void PropertySetterAllowsNullArgumentForNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        sample.NullProperty = null;
    }

    [Fact]
    public void PropertySetterAllowsNullArgumentForNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        sample.NullProperty = null;
    }

    [Fact]
    public void PropertyGetterReturnsNullForNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        Assert.Null(sample.NullProperty);
    }

    [Fact]
    public void PropertyGetterReturnsNullForNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        Assert.Null(sample.NullProperty);
    }
}