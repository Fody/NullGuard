using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;
using Xunit.Abstractions;

public class RewritingMethods :
    VerifyBase
{
    public RewritingMethods(ITestOutputHelper output) :
         base(output)
    {
    }

    [Fact]
    public void HandlesMethodsWithManyParameters()
    {
        var sample = new ClassWithNullableContext1();

        var exception = Assert.Throws<ArgumentNullException>(() => sample.MethodWithManyParameters("", null, "", ""));
        Assert.Equal("nonNullArg2", exception.ParamName);

        exception = Assert.Throws<ArgumentNullException>(() => sample.MethodWithManyParameters("", "", "", null));
        Assert.Equal("nonNullArg4", exception.ParamName);

        sample.MethodWithManyParameters(null, "", "", "");
        sample.MethodWithManyParameters("", "", null, "");
    }

    [Fact]
    public void AllowsNullRefReturnValueFromUnconstainedGeneric()
    {
        var sample = new ClassWithRefReturns.Generic<string>();
        var ret = sample.GetMaybeNullUnconstrainedRef();
        Assert.Null(ret);
    }

    [Fact]
    public void AllowsNullRefReturnValue()
    {
        var sample = new ClassWithRefReturns();
        var ret = sample.GetNullRef();
        Assert.Null(ret);
    }

    [Fact]
    public void RequiresNonNullRefReturnValue()
    {
        var sample = new ClassWithRefReturns();
        var exception = Assert.Throws<InvalidOperationException>(() => { sample.GetNonNullRef(); });
    }

    [Fact]
    public void RequiresNonNullRefReturnValueFromNonNullGeneric()
    {
        var sample = new ClassWithRefReturns.GenericNonNull<string>(null);
        var exception = Assert.Throws<InvalidOperationException>(() => { sample.GetNonNullRef(); });
    }

    [Fact]
    public void AllowsNonNullRefReturnValueFromNonNullGeneric()
    {
        var sample = new ClassWithRefReturns.GenericNonNull<string>(string.Empty);
        sample.GetNonNullRef();
    }

    [Fact]
    public void AllowsNullOutputForNestedGenericMaybeNullOutArgumentWhenFalse()
    {
        var sample = new ClassWithGenericNestedClass.NestedNotNull<string>();
        var ret = sample.MaybeNullOutValueWhenFalse(out var result);
        Assert.False(ret);
        Assert.Null(result);
    }

    [Fact]
    public void RequiresNotNullForNestedGenericDisallowNullRefArgument()
    {
        var sample = new ClassWithGenericNestedClass.NestedUnconstrained<string>();
        var exception = Assert.Throws<ArgumentNullException>(() => {
            string value = null;
            sample.DisallowedNullAndNotNullRefValue(ref value);
        });
        Assert.Equal("nonNullArg", exception.ParamName);
    }

    [Fact]
    public void RequiresNotNullForNestedGenericNotNullRefArgument()
    {
        var sample = new ClassWithGenericNestedClass.NestedUnconstrained<string>();
        var exception = Assert.Throws<InvalidOperationException>(() => {
            var value = "";
            sample.DisallowedNullAndNotNullRefValue(ref value);
        });
    }

    [Fact]
    public void AllowsNullReturnValueForMaybeNullGenericReturnValue()
    {
        var sample = new ClassWithNullableContext1();
        var result = sample.GenericMaybeNullReturnValue<string>();
        Assert.Null(result);
    }

    [Fact]
    public void AllowsNullArgumentAndReturnValueForClassConstrainedGenericWithNullableParameter()
    {
        var sample = new ClassWithNullableContext2();
        var result = sample.GenericClassWithNullableParam<string>(null);
        Assert.Null(result);
    }

    [Fact]
    public void AllowsNullArgumentAndReturnValueForNullableClassConstrainedGeneric()
    {
        var sample = new ClassWithNullableContext2();
        var result = sample.GenericNullableClassWithNotNullableParam<string>(null);
        Assert.Null(result);
    }

    [Fact]
    public void RequiresNonNullReturnForNotNullReturnValue()
    {
        var sample = new ClassWithNullableContext2();
        var exception = Assert.Throws<InvalidOperationException>(() => { sample.GenericNotNullReturnValue<string>(); });
    }

    [Fact]
    public void AllowsNullArgumentAndReturnValueForUnconstrainedGeneric()
    {
        var sample = new ClassWithNullableContext1();
        var result = sample.UnconstrainedGeneric<string>(null);
        Assert.Null(result);
    }

    [Fact]
    public void RequiresNonNullArgumentForNonNullGenericConstraint()
    {
        var sample = new ClassWithNullableContext1();
        var exception = Assert.Throws<ArgumentNullException>(() => { sample.NotNullGeneric<string>(null); });
        Assert.Equal("nonNullArg", exception.ParamName);
    }

    [Fact]
    public void RequiresNonNullReturnForNonNullGenericConstraint()
    {
        var sample = new ClassWithNullableContext1();
        var exception = Assert.Throws<InvalidOperationException>(() => { sample.NotNullGeneric<string>(""); });
    }

    [Fact]
    public void RequiresNonNullArgumentWhenNullableReferenceTypeNotUsedInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        var exception = Assert.Throws<ArgumentNullException>(() => { sample.SomeMethod(null, ""); });
        Assert.Equal("nonNullArg", exception.ParamName);
    }

    [Fact]
    public void RequiresNonNullArgumentWhenNullableReferenceTypeNotUsedInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        var exception = Assert.Throws<ArgumentNullException>(() => { sample.SomeMethod(null, ""); });
        Assert.Equal("nonNullArg", exception.ParamName);
    }

    [Fact]
    public void AllowsNullWhenNullableReferenceTypeUsed()
    {
        var sample = new ClassWithNullableContext1();
        sample.SomeMethod("", null);
    }

    [Fact]
    public void AllowsNullWhenNullableReferenceTypeUsedInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        sample.SomeMethod("", null);
    }

    [Fact]
    public void AllowsNullWithoutAttributeWhenNullableReferenceTypeUsedInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        sample.MethodWillNullableArg(null);
    }

    [Fact]
    public Task RequiresNonNullMethodReturnValueWhenNullableReferenceTypeNotUsedInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        var exception = Assert.Throws<InvalidOperationException>(() => sample.MethodWithReturnValue(true));
        return Verify(exception.Message);
    }

    [Fact]
    public Task RequiresNonNullMethodReturnValueWhenNullableReferenceTypeNotUsedInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        var exception = Assert.Throws<InvalidOperationException>(() => sample.MethodWithReturnValue(true));
        return Verify(exception.Message);
    }

    [Fact]
    public void AllowsNullReturnValueWhenNullableReferenceTypeUsedInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        sample.MethodAllowsNullReturnValue();
    }

    [Fact]
    public void AllowsNullReturnValueWhenNullableReferenceTypeUsedInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        sample.MethodAllowsNullReturnValue();
    }

    [Fact]
    public void AllowsNullReturnValueWhenNullableReferenceTypeUsedInClassWithNullableReferenceMethod()
    {
        var sample = new ClassWithNullableReferenceMethod();
        Assert.Null(sample.MethodAllowsNullReturnValue(""));
    }

    [Fact]
    public void AllowsNullReturnValueWhenNullableDisabledInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        Assert.Null(sample.MethodWithNullableContext0());
    }

    [Fact]
    public void AllowsNullReturnValueWhenNullableDisabledInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        Assert.Null(sample.MethodWithNullableContext0());
    }

    [Fact]
    public void AllowsNullReturnValueWhenStaticNullableReferenceTypeUsedInClassWithNullableContext1()
    {
        Assert.Null(ClassWithNullableContext1.StaticMethodAllowsNullReturnValue(""));
    }

    [Fact]
    public void AllowsNullReturnValueWhenStaticNullableReferenceTypeUsedInClassWithNullableContext2()
    {
        Assert.Null(ClassWithNullableContext2.StaticMethodAllowsNullReturnValue(""));
    }
}