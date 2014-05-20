using System;
using NUnit.Framework;

[TestFixture]
public class RewritingIndexers
{
    private Type indexersClassType;

    [SetUp]
    public void SetUp()
    {
        indexersClassType = AssemblyWeaver.Assemblies[0].GetType("Indexers");

        AssemblyWeaver.TestListener.Reset();
    }

    [Test]
    public void NonNullableIndexerSetterWithFirstArgumentNull()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => instance[nonNullParam1: null, nonNullParam2: null] = "value");
        Assert.AreEqual("nonNullParam1", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullParam1 is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void NonNullableIndexerSetterWithSecondArgumentNull()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => instance[nonNullParam1: "arg 1", nonNullParam2: null] = "value");
        Assert.AreEqual("nonNullParam2", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullParam2 is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void NonNullableIndexerSetterWithValueArgumentNull()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"] = null);
        Assert.AreEqual("value", exception.ParamName);
        Assert.AreEqual(
            "Fail: [NullGuard] Cannot set the value of property 'System.String Indexers/NonNullable::Item(System.String,System.String)' to null.",
            AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void NonNullableIndexerSetterWithNonNullArguments()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"] = "value";
    }

    [Test]
    public void NonNullableIndexerGetterWithFirstArgumentNull()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => IgnoreValue(instance[nonNullParam1: null, nonNullParam2: null]));
        Assert.AreEqual("nonNullParam1", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullParam1 is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void NonNullableIndexerGetterWithSecondArgumentNull()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => IgnoreValue(instance[nonNullParam1: "arg 1", nonNullParam2: null]));
        Assert.AreEqual("nonNullParam2", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullParam2 is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void NonNullableIndexerGetterWithNonNullArguments()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        Assert.AreEqual("return value of NonNullable", instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"]);
    }

    [Test]
    public void PassThroughGetterReturnValueWithNullArgument()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("PassThroughGetterReturnValue"));
        Assert.Throws<InvalidOperationException>(() => IgnoreValue(instance[returnValue: null]));
        Assert.AreEqual(
            "Fail: [NullGuard] Return value of property 'System.String Indexers/PassThroughGetterReturnValue::Item(System.String)' is null.",
            AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void PassThroughGetterReturnValueWithNonNullArgument()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("PassThroughGetterReturnValue"));
        Assert.AreEqual("not null", instance[returnValue: "not null"]);
    }

    [Test]
    public void AllowedNullsIndexerSetter()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("AllowedNulls"));
        instance[allowNull: null, nullableInt: null] = null;
    }

    [Test]
    public void AllowedNullsIndexerGetter()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("AllowedNulls"));
        Assert.AreEqual(null, instance[allowNull: null, nullableInt: null]);
    }

    // ReSharper disable once UnusedParameter.Local
    private void IgnoreValue(object value)
    {
    }
}