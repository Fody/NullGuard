using TestsCommon;
using System;

public class RewritingProperties
{
    [Test]
    public void PropertySetterThrowForNestedGenericWithDisallowNull()
    {
        var sample = new ClassWithGenericNestedClass.NestedUnconstrained<string>();
        Shared.Throws<ArgumentNullException>(() => sample.PossiblyNullPropertyWithDisallowNull = null);
    }

    [Test]
    public void PropertyGetterThrowsOnNullReturnForNestedGenericWithNotNull()
    {
        var sample = new ClassWithGenericNestedClass.NestedUnconstrained<string>();
        Shared.Throws<InvalidOperationException>(() =>
        {
            var dummy = sample.PossiblyNullPropertyWithNotNull;
        });
    }

    [Test]
    public void PropertySetterAllowsNullArgumentForNestedNotNullGenericWithAllowNull()
    {
        var sample = new ClassWithGenericNestedClass.NestedNotNull<string>
        {
            NotNullPropertyWithAllowNull = null
        };
    }

    [Test]
    public void PropertyGetterThrowsOnNullReturnForNestedNotNullGenericWithAllowNull()
    {
        var sample = new ClassWithGenericNestedClass.NestedNotNull<string>();
        Shared.Throws<InvalidOperationException>(() =>
        {
            var dummy = sample.NotNullPropertyWithAllowNull;
        });
    }

    [Test]
    public async Task PropertyGetterAllowsNullReturnForNestedNotNullGenericWithMaybeNull()
    {
        var sample = new ClassWithGenericNestedClass.NestedNotNull<string>();
        var value = sample.NotNullPropertyWithMaybeNull;
        await Assert.That(value).IsNull();
    }

    [Test]
    public void PropertySetterThrowsOnNullArgumentForNestedNotNullGenericWithMaybeNull()
    {
        var sample = new ClassWithGenericNestedClass.NestedNotNull<string>();
        Shared.Throws<ArgumentNullException>(() => sample.NotNullPropertyWithMaybeNull = null);
    }


    [Test]
    public void PropertySetterAllowsNullArgumentForNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1
        {
            NullProperty = null
        };
    }

    [Test]
    public void PropertySetterAllowsNullArgumentForNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2
        {
            NullProperty = null
        };
    }

    [Test]
    public async Task PropertyGetterReturnsNullForNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        await Assert.That(sample.NullProperty).IsNull();
    }

    [Test]
    public async Task PropertyGetterReturnsNullForNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        await Assert.That(sample.NullProperty).IsNull();
    }

    [Test]
    public void PropertySetterThrowsOnNullArgumentForNonNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        Shared.Throws<ArgumentNullException>(() => sample.NonNullProperty = null);
    }

    [Test]
    public void PropertySetterThrowsOnNullArgumentForNonNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        Shared.Throws<ArgumentNullException>(() => sample.NonNullProperty = null);
    }

    [Test]
    public void PropertyGetterThrowsOnNonNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        Shared.Throws<InvalidOperationException>(() =>
        {
            var dummy = sample.NonNullProperty;
        });
    }

    [Test]
    public void PropertyGetterThrowsOnNonNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        Shared.Throws<InvalidOperationException>(() =>
        {
            var dummy = sample.NonNullProperty;
        });
    }

    [Test]
    public async Task PropertyGetterReturnsValueForNonNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        const string value = "Test";
        sample.NonNullProperty = value;
        await Assert.That(sample.NonNullProperty).IsEqualTo(value);
    }

    [Test]
    public async Task PropertyGetterReturnsValueForNonNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        const string value = "Test";
        sample.NonNullProperty = value;
        await Assert.That(sample.NonNullProperty).IsEqualTo(value);
    }
}

public class RewritingProperties2
{
    [Test]
    public void PropertySetterAllowsNullArgumentForNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1
        {
            MixedNullProperty = null
        };
    }

    [Test]
    public void PropertySetterAllowsNullArgumentForNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2
        {
            MixedNullProperty = null
        };
    }

    [Test]
    public async Task PropertyGetterReturnsNullForNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        await Assert.That(sample.MixedNullProperty).IsNull();
    }

    [Test]
    public async Task PropertyGetterReturnsNullForNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        await Assert.That(sample.MixedNullProperty).IsNull();
    }

    [Test]
    public void PropertySetterThrowsOnNullArgumentForNonNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        Shared.Throws<ArgumentNullException>(() => sample.MixedNonNullProperty = null);
    }

    [Test]
    public void PropertySetterThrowsOnNullArgumentForNonNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        Shared.Throws<ArgumentNullException>(() => sample.MixedNonNullProperty = null);
    }

    [Test]
    public void PropertyGetterThrowsOnNonNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        Shared.Throws<InvalidOperationException>(() =>
        {
            var dummy = sample.MixedNonNullProperty;
        });
    }

    [Test]
    public void PropertyGetterThrowsOnNonNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        Shared.Throws<InvalidOperationException>(() =>
        {
            var dummy = sample.MixedNonNullProperty;
        });
    }

    [Test]
    public async Task PropertyGetterReturnsValueForNonNullableTypeInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        var value = new Tuple<string, string>("a", "b");
        sample.MixedNonNullProperty = value;
        await Assert.That(sample.MixedNonNullProperty).IsEqualTo(value);
    }

    [Test]
    public async Task PropertyGetterReturnsValueForNonNullableTypeInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        var value = new Tuple<string, string>("a", "b");
        sample.MixedNonNullProperty = value;
        await Assert.That(sample.MixedNonNullProperty).IsEqualTo(value);
    }


    [Test]
    public async Task CorrectlyHandlesModreq()
    {
        var sample = new SimpleRecord { InitPropertyWithBackingField = 42 };
        await Assert.That(sample).IsNotNull();
    }
}
