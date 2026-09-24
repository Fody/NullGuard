using TestsCommon;
using System;
using System.Threading.Tasks;
using VerifyTUnit;

public class RewritingMethods
{
    [Test]
    public async Task HandlesMethodsWithManyParameters()
    {
        var sample = new ClassWithNullableContext1();

        var exception = Shared.Throws<ArgumentNullException>(() => sample.MethodWithManyParameters("", null, "", ""));
        await Assert.That(exception.ParamName).IsEqualTo("nonNullArg2");

        exception = Shared.Throws<ArgumentNullException>(() => sample.MethodWithManyParameters("", "", "", null));
        await Assert.That(exception.ParamName).IsEqualTo("nonNullArg4");

        sample.MethodWithManyParameters(null, "", "", "");
        sample.MethodWithManyParameters("", "", null, "");
    }

    [Test]
    public async Task AllowsNullRefReturnValueFromUnconstainedGeneric()
    {
        var sample = new ClassWithRefReturns.Generic<string>();
        var ret = sample.GetMaybeNullUnconstrainedRef();
        await Assert.That(ret).IsNull();
    }

    [Test]
    public async Task AllowsNullRefReturnValue()
    {
        var sample = new ClassWithRefReturns();
        var ret = sample.GetNullRef();
        await Assert.That(ret).IsNull();
    }

    [Test]
    public void RequiresNonNullRefReturnValue()
    {
        var sample = new ClassWithRefReturns();
        var exception = Shared.Throws<InvalidOperationException>(() => { sample.GetNonNullRef(); });
    }

    [Test]
    public void RequiresNonNullRefReturnValueFromNonNullGeneric()
    {
        var sample = new ClassWithRefReturns.GenericNonNull<string>(null);
        var exception = Shared.Throws<InvalidOperationException>(() => { sample.GetNonNullRef(); });
    }

    [Test]
    public void AllowsNonNullRefReturnValueFromNonNullGeneric()
    {
        var sample = new ClassWithRefReturns.GenericNonNull<string>(string.Empty);
        sample.GetNonNullRef();
    }

    [Test]
    public async Task AllowsNullOutputForNestedGenericMaybeNullOutArgumentWhenFalse()
    {
        var sample = new ClassWithGenericNestedClass.NestedNotNull<string>();
        var ret = sample.MaybeNullOutValueWhenFalse(out var result);
        await Assert.That(ret).IsFalse();
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task RequiresNotNullForNestedGenericDisallowNullRefArgument()
    {
        var sample = new ClassWithGenericNestedClass.NestedUnconstrained<string>();
        var exception = Shared.Throws<ArgumentNullException>(() => {
            string value = null;
            sample.DisallowedNullAndNotNullRefValue(ref value);
        });
        await Assert.That(exception.ParamName).IsEqualTo("nonNullArg");
    }

    [Test]
    public void RequiresNotNullForNestedGenericNotNullRefArgument()
    {
        var sample = new ClassWithGenericNestedClass.NestedUnconstrained<string>();
        var exception = Shared.Throws<InvalidOperationException>(() => {
            var value = "";
            sample.DisallowedNullAndNotNullRefValue(ref value);
        });
    }

    [Test]
    public async Task AllowsNullReturnValueForMaybeNullGenericReturnValue()
    {
        var sample = new ClassWithNullableContext1();
        var result = sample.GenericMaybeNullReturnValue<string>();
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task AllowsNullArgumentAndReturnValueForClassConstrainedGenericWithNullableParameter()
    {
        var sample = new ClassWithNullableContext2();
        var result = sample.GenericClassWithNullableParam<string>(null);
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task AllowsNullArgumentAndReturnValueForNullableClassConstrainedGeneric()
    {
        var sample = new ClassWithNullableContext2();
        var result = sample.GenericNullableClassWithNotNullableParam<string>(null);
        await Assert.That(result).IsNull();
    }

    [Test]
    public void RequiresNonNullReturnForNotNullReturnValue()
    {
        var sample = new ClassWithNullableContext2();
        var exception = Shared.Throws<InvalidOperationException>(() => { sample.GenericNotNullReturnValue<string>(); });
    }

    [Test]
    public async Task AllowsNullArgumentAndReturnValueForUnconstrainedGeneric()
    {
        var sample = new ClassWithNullableContext1();
        var result = sample.UnconstrainedGeneric<string>(null);
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task RequiresNonNullArgumentForNonNullGenericConstraint()
    {
        var sample = new ClassWithNullableContext1();
        var exception = Shared.Throws<ArgumentNullException>(() => { sample.NotNullGeneric<string>(null); });
        await Assert.That(exception.ParamName).IsEqualTo("nonNullArg");
    }

    [Test]
    public void RequiresNonNullReturnForNonNullGenericConstraint()
    {
        var sample = new ClassWithNullableContext1();
        var exception = Shared.Throws<InvalidOperationException>(() => { sample.NotNullGeneric(""); });
    }

    [Test]
    public async Task RequiresNonNullArgumentWhenNullableReferenceTypeNotUsedInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        var exception = Shared.Throws<ArgumentNullException>(() => { sample.SomeMethod(null, ""); });
        await Assert.That(exception.ParamName).IsEqualTo("nonNullArg");
    }

    [Test]
    public async Task RequiresNonNullArgumentWhenNullableReferenceTypeNotUsedInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        var exception = Shared.Throws<ArgumentNullException>(() => { sample.SomeMethod(null, ""); });
        await Assert.That(exception.ParamName).IsEqualTo("nonNullArg");
    }

    [Test]
    public void AllowsNullWhenNullableReferenceTypeUsed()
    {
        var sample = new ClassWithNullableContext1();
        sample.SomeMethod("", null);
    }

    [Test]
    public void AllowsNullWhenNullableReferenceTypeUsedInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        sample.SomeMethod("", null);
    }

    [Test]
    public void AllowsNullWithoutAttributeWhenNullableReferenceTypeUsedInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        sample.MethodWillNullableArg(null);
    }

    [Test]
    public async Task RequiresNonNullMethodReturnValueWhenNullableReferenceTypeNotUsedInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        var exception = Shared.Throws<InvalidOperationException>(() => sample.MethodWithReturnValue(true));
        await Verifier.Verify(exception.Message);
    }

    [Test]
    public async Task RequiresNonNullMethodReturnValueWhenNullableReferenceTypeNotUsedInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        var exception = Shared.Throws<InvalidOperationException>(() => sample.MethodWithReturnValue(true));
        await Verifier.Verify(exception.Message);
    }

    [Test]
    public void AllowsNullReturnValueWhenNullableReferenceTypeUsedInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        sample.MethodAllowsNullReturnValue();
    }

    [Test]
    public void AllowsNullReturnValueWhenNullableReferenceTypeUsedInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        sample.MethodAllowsNullReturnValue();
    }

    [Test]
    public async Task AllowsNullReturnValueWhenNullableReferenceTypeUsedInClassWithNullableReferenceMethod()
    {
        var sample = new ClassWithNullableReferenceMethod();
        await Assert.That(sample.MethodAllowsNullReturnValue("")).IsNull();
    }

    [Test]
    public async Task AllowsNullReturnValueWhenNullableDisabledInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        await Assert.That(sample.MethodWithNullableContext0()).IsNull();
    }

    [Test]
    public async Task AllowsNullReturnValueWhenNullableDisabledInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        await Assert.That(sample.MethodWithNullableContext0()).IsNull();
    }

    [Test]
    public async Task AllowsNullReturnValueWhenStaticNullableReferenceTypeUsedInClassWithNullableContext1()
    {
        await Assert.That(ClassWithNullableContext1.StaticMethodAllowsNullReturnValue("")).IsNull();
    }

    [Test]
    public async Task AllowsNullReturnValueWhenStaticNullableReferenceTypeUsedInClassWithNullableContext2()
    {
        await Assert.That(ClassWithNullableContext2.StaticMethodAllowsNullReturnValue("")).IsNull();
    }
}