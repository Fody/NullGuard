using System;
using NUnit.Framework;

[TestFixture]
public class RewritingIndexers
{
    [SetUp]
    public void SetUp()
    {
        AssemblyWeaver.TestListener.Reset();
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.IndexersClassTypes))]
    public void NonNullableIndexerSetterWithFirstArgumentNull(Type indexersClassType)
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => instance[nonNullParam1: null, nonNullParam2: null] = "value");
        Assert.AreEqual("nonNullParam1", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullParam1 is null.", AssemblyWeaver.TestListener.Message);
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.IndexersClassTypes))]
    public void NonNullableIndexerSetterWithSecondArgumentNull(Type indexersClassType)
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => instance[nonNullParam1: "arg 1", nonNullParam2: null] = "value");
        Assert.AreEqual("nonNullParam2", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullParam2 is null.", AssemblyWeaver.TestListener.Message);
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.IndexersClassTypes))]
    public void NonNullableIndexerSetterWithValueArgumentNull(Type indexersClassType)
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"] = null);
        Assert.AreEqual("value", exception.ParamName);
        Assert.AreEqual(
            "Fail: [NullGuard] Cannot set the value of property 'System.String Indexers/NonNullable::Item(System.String,System.String)' to null.",
            AssemblyWeaver.TestListener.Message);
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.IndexersClassTypes))]
    public void NonNullableIndexerSetterWithNonNullArguments(Type indexersClassType)
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"] = "value";
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.IndexersClassTypes))]
    public void NonNullableIndexerGetterWithFirstArgumentNull(Type indexersClassType)
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => IgnoreValue(instance[nonNullParam1: null, nonNullParam2: null]));
        Assert.AreEqual("nonNullParam1", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullParam1 is null.", AssemblyWeaver.TestListener.Message);
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.IndexersClassTypes))]
    public void NonNullableIndexerGetterWithSecondArgumentNull(Type indexersClassType)
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => IgnoreValue(instance[nonNullParam1: "arg 1", nonNullParam2: null]));
        Assert.AreEqual("nonNullParam2", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullParam2 is null.", AssemblyWeaver.TestListener.Message);
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.IndexersClassTypes))]
    public void NonNullableIndexerGetterWithNonNullArguments(Type indexersClassType)
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        Assert.AreEqual("return value of NonNullable", instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"]);
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.IndexersClassTypes))]
    public void PassThroughGetterReturnValueWithNullArgument(Type indexersClassType)
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("PassThroughGetterReturnValue"));
        Assert.Throws<InvalidOperationException>(() => IgnoreValue(instance[returnValue: null]));
        Assert.AreEqual(
            "Fail: [NullGuard] Return value of property 'System.String Indexers/PassThroughGetterReturnValue::Item(System.String)' is null.",
            AssemblyWeaver.TestListener.Message);
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.IndexersClassTypes))]
    public void PassThroughGetterReturnValueWithNonNullArgument(Type indexersClassType)
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("PassThroughGetterReturnValue"));
        Assert.AreEqual("not null", instance[returnValue: "not null"]);
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.IndexersClassTypes))]
    public void AllowedNullsIndexerSetter(Type indexersClassType)
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("AllowedNulls"));
        instance[allowNull: null, nullableInt: null] = null;
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.IndexersClassTypes))]
    public void AllowedNullsIndexerGetter(Type indexersClassType)
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("AllowedNulls"));
        Assert.AreEqual(null, instance[allowNull: null, nullableInt: null]);
    }

    // ReSharper disable once UnusedParameter.Local
    void IgnoreValue(object value)
    {
    }
}