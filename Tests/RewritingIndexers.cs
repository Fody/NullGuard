using System;
using ApprovalTests;
using NUnit.Framework;

[TestFixture]
public class RewritingIndexers
{
    [Test]
    public void NonNullableIndexerSetterWithFirstArgumentNull()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => instance[nonNullParam1: null, nonNullParam2: null] = "value");
        Approvals.Verify(exception.Message);
    }

    [Test]
    public void NonNullableIndexerSetterWithSecondArgumentNull()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => instance[nonNullParam1: "arg 1", nonNullParam2: null] = "value");
        Approvals.Verify(exception.Message);
    }

    [Test]
    public void NonNullableIndexerSetterWithValueArgumentNull()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"] = null);
        Approvals.Verify(exception.Message);
    }

    [Test]
    public void NonNullableIndexerSetterWithNonNullArguments()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"] = "value";
    }

    [Test]
    public void NonNullableIndexerGetterWithFirstArgumentNull()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => IgnoreValue(instance[nonNullParam1: null, nonNullParam2: null]));
        Approvals.Verify(exception.Message);
    }

    [Test]
    public void NonNullableIndexerGetterWithSecondArgumentNull()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => IgnoreValue(instance[nonNullParam1: "arg 1", nonNullParam2: null]));
        Approvals.Verify(exception.Message);
    }

    [Test]
    public void NonNullableIndexerGetterWithNonNullArguments()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        Assert.AreEqual("return value of NonNullable", instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"]);
    }

    [Test]
    public void PassThroughGetterReturnValueWithNullArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("PassThroughGetterReturnValue"));
        var exception = Assert.Throws<InvalidOperationException>(() => IgnoreValue(instance[returnValue: null]));
        Approvals.Verify(exception.Message);
    }

    [Test]
    public void PassThroughGetterReturnValueWithNonNullArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("PassThroughGetterReturnValue"));
        Assert.AreEqual("not null", instance[returnValue: "not null"]);
    }

    [Test]
    public void AllowedNullsIndexerSetter()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("AllowedNulls"));
        instance[allowNull: null, nullableInt: null] = null;
    }

    [Test]
    public void AllowedNullsIndexerGetter()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("AllowedNulls"));
        Assert.AreEqual(null, instance[allowNull: null, nullableInt: null]);
    }

    // ReSharper disable once UnusedParameter.Local
    void IgnoreValue(object value)
    {
    }
}