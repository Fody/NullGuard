using System;
using NUnit.Framework;

[TestFixture]
public class RewritingIndexers
{
    Func<int, Type> indexersClassType;

    [SetUp]
    public void SetUp()
    {
        indexersClassType = index => AssemblyWeaver.RewritingTestAssemblies[index].GetType("Indexers");

        AssemblyWeaver.TestListener.Reset();
    }

    [Test]
    public void NonNullableIndexerSetterWithFirstArgumentNull([Values(0, 1)] int index)
    {
        var instance = (dynamic)Activator.CreateInstance(indexersClassType(index).GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => instance[nonNullParam1: null, nonNullParam2: null] = "value");
        Assert.AreEqual("nonNullParam1", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullParam1 is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void NonNullableIndexerSetterWithSecondArgumentNull([Values(0, 1)] int index)
    {
        var instance = (dynamic)Activator.CreateInstance(indexersClassType(index).GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => instance[nonNullParam1: "arg 1", nonNullParam2: null] = "value");
        Assert.AreEqual("nonNullParam2", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullParam2 is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void NonNullableIndexerSetterWithValueArgumentNull([Values(0, 1)] int index)
    {
        var instance = (dynamic)Activator.CreateInstance(indexersClassType(index).GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"] = null);
        Assert.AreEqual("value", exception.ParamName);
        Assert.AreEqual(
            "Fail: [NullGuard] Cannot set the value of property 'System.String Indexers/NonNullable::Item(System.String,System.String)' to null.",
            AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void NonNullableIndexerSetterWithNonNullArguments([Values(0, 1)] int index)
    {
        var instance = (dynamic)Activator.CreateInstance(indexersClassType(index).GetNestedType("NonNullable"));
        instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"] = "value";
    }

    [Test]
    public void NonNullableIndexerGetterWithFirstArgumentNull([Values(0, 1)] int index)
    {
        var instance = (dynamic)Activator.CreateInstance(indexersClassType(index).GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => IgnoreValue(instance[nonNullParam1: null, nonNullParam2: null]));
        Assert.AreEqual("nonNullParam1", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullParam1 is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void NonNullableIndexerGetterWithSecondArgumentNull([Values(0, 1)] int index)
    {
        var instance = (dynamic)Activator.CreateInstance(indexersClassType(index).GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => IgnoreValue(instance[nonNullParam1: "arg 1", nonNullParam2: null]));
        Assert.AreEqual("nonNullParam2", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullParam2 is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void NonNullableIndexerGetterWithNonNullArguments([Values(0, 1)] int index)
    {
        var instance = (dynamic)Activator.CreateInstance(indexersClassType(index).GetNestedType("NonNullable"));
        Assert.AreEqual("return value of NonNullable", instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"]);
    }

    [Test]
    public void PassThroughGetterReturnValueWithNullArgument([Values(0, 1)] int index)
    {
        var instance = (dynamic)Activator.CreateInstance(indexersClassType(index).GetNestedType("PassThroughGetterReturnValue"));
        Assert.Throws<InvalidOperationException>(() => IgnoreValue(instance[returnValue: null]));
        Assert.AreEqual(
            "Fail: [NullGuard] Return value of property 'System.String Indexers/PassThroughGetterReturnValue::Item(System.String)' is null.",
            AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void PassThroughGetterReturnValueWithNonNullArgument([Values(0, 1)] int index)
    {
        var instance = (dynamic)Activator.CreateInstance(indexersClassType(index).GetNestedType("PassThroughGetterReturnValue"));
        Assert.AreEqual("not null", instance[returnValue: "not null"]);
    }

    [Test]
    public void AllowedNullsIndexerSetter([Values(0, 1)] int index)
    {
        var instance = (dynamic)Activator.CreateInstance(indexersClassType(index).GetNestedType("AllowedNulls"));
        instance[allowNull: null, nullableInt: null] = null;
    }

    [Test]
    public void AllowedNullsIndexerGetter([Values(0, 1)] int index)
    {
        var instance = (dynamic)Activator.CreateInstance(indexersClassType(index).GetNestedType("AllowedNulls"));
        Assert.AreEqual(null, instance[allowNull: null, nullableInt: null]);
    }

    // ReSharper disable once UnusedParameter.Local
    void IgnoreValue(object value)
    {
    }
}