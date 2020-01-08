using System;

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

    [Fact]
    public void PropertySetterThrowsOnNullArgumentForNonNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        Assert.Throws<ArgumentNullException>(() => sample.NonNullProperty = null);
    }

    [Fact]
    public void PropertySetterThrowsOnNullArgumentForNonNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        Assert.Throws<ArgumentNullException>(() => sample.NonNullProperty = null);
    }

    [Fact]
    public void PropertyGetterThrowsOnNonNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        Assert.Throws<InvalidOperationException>(() =>
        {
            var dummy = sample.NonNullProperty;
        });
    }

    [Fact]
    public void PropertyGetterThrowsOnNonNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        Assert.Throws<InvalidOperationException>(() =>
        {
            var dummy = sample.NonNullProperty;
        });
    }

    [Fact]
    public void PropertyGetterReturnsValueForNonNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        const string value = "Test";
        sample.NonNullProperty = value;
        Assert.Equal(value, sample.NonNullProperty);
    }

    [Fact]
    public void PropertyGetterReturnsValueForNonNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        const string value = "Test";
        sample.NonNullProperty = value;
        Assert.Equal(value, sample.NonNullProperty);
    }
}