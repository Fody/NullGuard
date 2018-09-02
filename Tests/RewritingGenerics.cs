using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

public class RewritingGenerics
{
    [Theory]
    [InlineData("GenericClassFactory")]
    [InlineData("GenericClassWithValueTypeConstraintsFactory")]
    void GenericClassDoesNotThrowOnIntegerValueType(string factoryName)
    {
        var type = AssemblyWeaver.Assembly.GetType(factoryName);
        var factory = (dynamic)Activator.CreateInstance(type);
        var sample = factory.Integer;

        sample.NonNullProperty = 0;

        Assert.Equal(0, sample.NonNullProperty);
        Assert.Equal(0, sample.NonNullMethod());
        Assert.Equal(0, sample.GenericMethod<int>(0, 0));
    }

    [Theory]
    [InlineData("GenericClassFactory")]
    [InlineData("GenericClassWithValueTypeConstraintsFactory")]
    void GenericClassDoesNotThrowOnStructValueType(string factoryName)
    {
        var valueType = default(KeyValuePair<string, string>);

        var type = AssemblyWeaver.Assembly.GetType(factoryName);
        var factory = (dynamic)Activator.CreateInstance(type);
        var sample = factory.Struct;

        sample.NonNullProperty = valueType;

        Assert.Equal(valueType, sample.NonNullProperty);
        Assert.Equal(valueType, sample.NonNullMethod());
        Assert.Equal(valueType, sample.GenericMethod<KeyValuePair<string, string>>(valueType, valueType));
    }

    [Theory]
    [InlineData("GenericClass")]
    [InlineData("GenericClassWithReferenceTypeConstraints")]
    void GenericClassThrowsOnNullReferenceType(string className)
    {
        object[] nullValue = null;
        var notNullValue = new object[0];

        var factoryName = className + "Factory";
        var factoryType = AssemblyWeaver.Assembly.GetType(factoryName);
        var factory = (dynamic)Activator.CreateInstance(factoryType);
        var sample = factory.Object;

        var exceptions = new List<Exception>();

        exceptions.Add(Assert.Throws<ArgumentNullException>(() => sample.NonNullProperty = nullValue));
        exceptions.Add(Assert.Throws<InvalidOperationException>(() => sample.NonNullProperty));

        sample.NonNullProperty = notNullValue;
        Assert.Equal(notNullValue, sample.NonNullProperty);

        exceptions.Add(Assert.Throws<InvalidOperationException>(() => sample.NonNullMethod()));
        sample.CanBeNullProperty = notNullValue;
        Assert.Equal(notNullValue, sample.NonNullMethod());

        exceptions.Add(Assert.Throws<ArgumentNullException>(() => sample.GenericMethod<Array>(nullValue, nullValue)));
        exceptions.Add(Assert.Throws<ArgumentNullException>(() => sample.GenericMethod<Array>(notNullValue, nullValue)));
        exceptions.Add(Assert.Throws<ArgumentNullException>(() => sample.GenericMethod<Array>(nullValue, notNullValue)));
        exceptions.Add(Assert.Throws<InvalidOperationException>(() => sample.GenericMethod<Array>(notNullValue, notNullValue)));

        Assert.Equal(notNullValue, sample.GenericMethodReturnsParameter<Array>(notNullValue, notNullValue));
        exceptions.Add(Assert.Throws<InvalidOperationException>(() => sample.GenericMethodReturnsParameter<Array>(notNullValue, nullValue)));

        // approvals don't work for [Theorie], just do it inline...
        var expected = @"[NullGuard] Cannot set the value of property 'T ClassName`1::NonNullProperty()' to null.|Parameter name: value
[NullGuard] Return value of property 'T ClassName`1::NonNullProperty()' is null.
[NullGuard] Return value of method 'T ClassName`1::NonNullMethod()' is null.
[NullGuard] t is null.|Parameter name: t
[NullGuard] u is null.|Parameter name: u
[NullGuard] t is null.|Parameter name: t
[NullGuard] Return value of method 'U ClassName`1::GenericMethod(T,U)' is null.
[NullGuard] Return value of method 'U ClassName`1::GenericMethodReturnsParameter(T,U)' is null.";

        var messages = exceptions.Select(ex => ex.Message.Replace(Environment.NewLine, "|"));
        var signature = string.Join(Environment.NewLine, messages).Replace(className, "ClassName");

        Assert.Equal(expected, signature);
    }

    [Fact]
    void GenericClassWithAsyncValueTypeLambdaDoesNotThrow()
    {
        var factoryType = AssemblyWeaver.Assembly.GetType("GenericClassFactory");
        var factory = (dynamic)Activator.CreateInstance(factoryType);

        var result = factory.GetThingAsync();
        Assert.Equal(0, result);

        result = factory.GetThingAsync2();
        Assert.Equal(0, result);
    }
}
