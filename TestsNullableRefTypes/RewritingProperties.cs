﻿using System;

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
    public void PropertySetterThrowForNestedGenericWithDisallowNull()
    {
        var sample = new ClassWithNullableContext2.NestedUnconstrained<string>();
        Assert.Throws<ArgumentNullException>(() => sample.PossiblyNullPropertyWithDisallowNull = null);
    }

    [Fact]
    public void PropertyGetterThrowsOnNullReturnForNestedGenericWithNotNull()
    {
        var sample = new ClassWithNullableContext2.NestedUnconstrained<string>();
        Assert.Throws<InvalidOperationException>(() =>
        {
            var dummy = sample.PossiblyNullPropertyWithNotNull;
        });
    }

    [Fact]
    public void PropertySetterAllowsNullArgumentForNestedNotNullGenericWithAllowNull()
    {
        var sample = new ClassWithNullableContext2.NestedNotNull<string>();
        sample.NotNullPropertyWithAllowNull = null;
    }

    [Fact]
    public void PropertyGetterThrowsOnNullReturnForNestedNotNullGenericWithAllowNull()
    {
        var sample = new ClassWithNullableContext2.NestedNotNull<string>();
        Assert.Throws<InvalidOperationException>(() =>
        {
            var dummy = sample.NotNullPropertyWithAllowNull;
        });
    }

    [Fact]
    public void PropertyGetterAllowsNullReturnForNestedNotNullGenericWithMaybeNull()
    {
        var sample = new ClassWithNullableContext2.NestedNotNull<string>();
        var value = sample.NotNullPropertyWithMaybeNull;
        Assert.Null(value);
    }

    [Fact]
    public void PropertySetterThrowsOnNullArgumentForNestedNotNullGenericWithMaybeNull()
    {
        var sample = new ClassWithNullableContext2.NestedNotNull<string>();
        Assert.Throws<ArgumentNullException>(() => sample.NotNullPropertyWithMaybeNull = null);
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

public class RewritingProperties2 :
    VerifyBase
{
    public RewritingProperties2(ITestOutputHelper output) :
        base(output)
    {
    }

    [Fact]
    public void PropertySetterAllowsNullArgumentForNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        sample.MixedNullProperty = null;
    }

    [Fact]
    public void PropertySetterAllowsNullArgumentForNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        sample.MixedNullProperty = null;
    }

    [Fact]
    public void PropertyGetterReturnsNullForNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        Assert.Null(sample.MixedNullProperty);
    }

    [Fact]
    public void PropertyGetterReturnsNullForNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        Assert.Null(sample.MixedNullProperty);
    }

    [Fact]
    public void PropertySetterThrowsOnNullArgumentForNonNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        Assert.Throws<ArgumentNullException>(() => sample.MixedNonNullProperty = null);
    }

    [Fact]
    public void PropertySetterThrowsOnNullArgumentForNonNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        Assert.Throws<ArgumentNullException>(() => sample.MixedNonNullProperty = null);
    }

    [Fact]
    public void PropertyGetterThrowsOnNonNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        Assert.Throws<InvalidOperationException>(() =>
        {
            var dummy = sample.MixedNonNullProperty;
        });
    }

    [Fact]
    public void PropertyGetterThrowsOnNonNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        Assert.Throws<InvalidOperationException>(() =>
        {
            var dummy = sample.MixedNonNullProperty;
        });
    }

    [Fact]
    public void PropertyGetterReturnsValueForNonNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        var value = new Tuple<string, string>("a", "b");
        sample.MixedNonNullProperty = value;
        Assert.Equal(value, sample.MixedNonNullProperty);
    }

    [Fact]
    public void PropertyGetterReturnsValueForNonNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        var value = new Tuple<string, string>("a", "b");
        sample.MixedNonNullProperty = value;
        Assert.Equal(value, sample.MixedNonNullProperty);
    }
}