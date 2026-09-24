using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using TestsCommon;

public class RewritingGenerics
{
    [Test]
    [Arguments("GenericClassFactory")]
    [Arguments("GenericClassWithValueTypeConstraintsFactory")]
    public async Task GenericClassDoesNotThrowOnIntegerValueType(string factoryName)
    {
        var type = AssemblyWeaver.Assembly.GetType(factoryName);
        var factory = (dynamic)Activator.CreateInstance(type);
        var sample = factory.Integer;

        sample.NonNullProperty = 0;

        await Assert.That((object)sample.NonNullProperty).IsEqualTo(0);
        await Assert.That((object)sample.NonNullMethod()).IsEqualTo(0);
        await Assert.That((object)sample.GenericMethod<int>(0, 0)).IsEqualTo(0);
    }

    [Test]
    [Arguments("GenericClassFactory")]
    [Arguments("GenericClassWithValueTypeConstraintsFactory")]
    public async Task GenericClassDoesNotThrowOnStructValueType(string factoryName)
    {
        var valueType = default(KeyValuePair<string, string>);

        var type = AssemblyWeaver.Assembly.GetType(factoryName);
        var factory = (dynamic)Activator.CreateInstance(type);
        var sample = factory.Struct;

        sample.NonNullProperty = valueType;

        await Assert.That((object)sample.NonNullProperty).IsEqualTo(valueType);
        await Assert.That((object)sample.NonNullMethod()).IsEqualTo(valueType);
        await Assert.That((object)sample.GenericMethod<KeyValuePair<string, string>>(valueType, valueType)).IsEqualTo(valueType);
    }

    [Test]
    [Arguments("GenericClass")]
    [Arguments("GenericClassWithReferenceTypeConstraints")]
    public async Task GenericClassThrowsOnNullReferenceType(string className)
    {
        object[] nullValue = null;
        var notNullValue = Array.Empty<object>();

        var factoryName = className + "Factory";
        var factoryType = AssemblyWeaver.Assembly.GetType(factoryName);
        var factory = (dynamic)Activator.CreateInstance(factoryType);
        var sample = factory.Object;

        var exceptions = new List<Exception>
        {
            Shared.Throws<ArgumentNullException>(() => sample.NonNullProperty = nullValue),
            Shared.Throws<InvalidOperationException>(() => sample.NonNullProperty)
        };

        sample.NonNullProperty = notNullValue;
        await Assert.That((bool)(notNullValue == sample.NonNullProperty)).IsTrue();

        exceptions.Add(Shared.Throws<InvalidOperationException>(() => sample.NonNullMethod()));
        sample.CanBeNullProperty = notNullValue;
        await Assert.That((bool)(notNullValue == sample.NonNullMethod())).IsTrue();

        exceptions.Add(Shared.Throws<ArgumentNullException>(() => sample.GenericMethod<Array>(nullValue, nullValue)));
        exceptions.Add(Shared.Throws<ArgumentNullException>(() => sample.GenericMethod<Array>(notNullValue, nullValue)));
        exceptions.Add(Shared.Throws<ArgumentNullException>(() => sample.GenericMethod<Array>(nullValue, notNullValue)));
        exceptions.Add(Shared.Throws<InvalidOperationException>(() => sample.GenericMethod<Array>(notNullValue, notNullValue)));

        await Assert.That((bool)(notNullValue == sample.GenericMethodReturnsParameter<Array>(notNullValue, notNullValue))).IsTrue();
        exceptions.Add(Shared.Throws<InvalidOperationException>(() => sample.GenericMethodReturnsParameter<Array>(notNullValue, nullValue)));

        // approvals don't work for [Test], just do it inline...
        var expected = """
                       [NullGuard] Cannot set the value of property 'T ClassName`1::NonNullProperty()' to null.|Parameter name: value
                       [NullGuard] Return value of property 'T ClassName`1::NonNullProperty()' is null.
                       [NullGuard] Return value of method 'T ClassName`1::NonNullMethod()' is null.
                       [NullGuard] t is null.|Parameter name: t
                       [NullGuard] u is null.|Parameter name: u
                       [NullGuard] t is null.|Parameter name: t
                       [NullGuard] Return value of method 'U ClassName`1::GenericMethod(T,U)' is null.
                       [NullGuard] Return value of method 'U ClassName`1::GenericMethodReturnsParameter(T,U)' is null.
                       """;

        var messages = exceptions.Select(ex => Shared.NormalizeArgumentExceptionText(ex.Message).Replace(Environment.NewLine, "|"));
        var signature = string.Join(Environment.NewLine, messages).Replace(className, "ClassName");

        await Assert.That(signature.Replace("\r\n", "\n")).IsEqualTo(expected.Replace("\r\n", "\n"));
    }

    [Test]
    public async Task GenericClassWithAsyncValueTypeLambdaDoesNotThrow()
    {
        var factoryType = AssemblyWeaver.Assembly.GetType("GenericClassFactory");
        var factory = (dynamic)Activator.CreateInstance(factoryType);

        var result = factory.GetThingAsync();
        await Assert.That((object)result).IsEqualTo(0);

        result = factory.GetThingAsync2();
        await Assert.That((object)result).IsEqualTo(0);
    }

    [Test]
    public void GenericClassWithTypeConstraintDoesNotThrow()
    {
        var factoryType = AssemblyWeaver.Assembly.GetType("GenericClassWithReferenceTypeConstraintsFactory");
        var factory = (dynamic)Activator.CreateInstance(factoryType);
        var sample = factory.Object;

        sample.GenericMethodVoid("test", Array.Empty<string>());
        sample.GenericMethodVoid("test", ImmutableArray<string>.Empty);
    }

    [Test]
    public void GenericClassWithValueTypeConstraintDoesNotThrow()
    {
        var factoryType = AssemblyWeaver.Assembly.GetType("GenericClassWithValueTypeConstraintsFactory");
        var factory = (dynamic)Activator.CreateInstance(factoryType);
        var sample = factory.Integer;

        sample.GenericMethod(42, 42m);
    }
}
