using System;
using Xunit;

public class RewritingProperties
{
    [Fact]
    public void PropertySetterThrowForNestedGenericWithDisallowNull()
    {
        var sample = new ClassWithGenericNestedClass.NestedUnconstrained<string>();
        Assert.Throws<ArgumentNullException>(() => sample.PossiblyNullPropertyWithDisallowNull = null);
    }

    [Fact]
    public void PropertyGetterThrowsOnNullReturnForNestedGenericWithNotNull()
    {
        var sample = new ClassWithGenericNestedClass.NestedUnconstrained<string>();
        Assert.Throws<InvalidOperationException>(() =>
        {
            var dummy = sample.PossiblyNullPropertyWithNotNull;
        });
    }

    [Fact]
    public void PropertySetterAllowsNullArgumentForNestedNotNullGenericWithAllowNull()
    {
        var sample = new ClassWithGenericNestedClass.NestedNotNull<string>();
        sample.NotNullPropertyWithAllowNull = null;
    }

    [Fact]
    public void PropertyGetterThrowsOnNullReturnForNestedNotNullGenericWithAllowNull()
    {
        var sample = new ClassWithGenericNestedClass.NestedNotNull<string>();
        Assert.Throws<InvalidOperationException>(() =>
        {
            var dummy = sample.NotNullPropertyWithAllowNull;
        });
    }

    [Fact]
    public void PropertyGetterAllowsNullReturnForNestedNotNullGenericWithMaybeNull()
    {
        var sample = new ClassWithGenericNestedClass.NestedNotNull<string>();
        var value = sample.NotNullPropertyWithMaybeNull;
        Assert.Null(value);
    }

    [Fact]
    public void PropertySetterThrowsOnNullArgumentForNestedNotNullGenericWithMaybeNull()
    {
        var sample = new ClassWithGenericNestedClass.NestedNotNull<string>();
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

public class RewritingProperties2
{
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


    [Fact]
    public void CorrectlyHandlesModreq()
    {
        var sample = new SimpleRecord { InitPropertyWithBackingField = 42 };
        Assert.NotNull(sample);
    }
}
