using System;
using Xunit;

public class RewritingIndexers
{
    Type indexersClassType;

    public RewritingIndexers()
    {
        indexersClassType = AssemblyWeaver.Assemblies[0].GetType("Indexers");

        AssemblyWeaver.TestListener.Reset();
    }

    [Fact]
    public void NonNullableIndexerSetterWithFirstArgumentNull()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => instance[nonNullParam1: null, nonNullParam2: null] = "value");
        Assert.Equal("nonNullParam1", exception.ParamName);
        Assert.Equal("Fail: [NullGuard] nonNullParam1 is null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void NonNullableIndexerSetterWithSecondArgumentNull()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => instance[nonNullParam1: "arg 1", nonNullParam2: null] = "value");
        Assert.Equal("nonNullParam2", exception.ParamName);
        Assert.Equal("Fail: [NullGuard] nonNullParam2 is null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void NonNullableIndexerSetterWithValueArgumentNull()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"] = null);
        Assert.Equal("value", exception.ParamName);
        Assert.Equal(
            "Fail: [NullGuard] Cannot set the value of property 'System.String Indexers/NonNullable::Item(System.String,System.String)' to null.",
            AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void NonNullableIndexerSetterWithNonNullArguments()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"] = "value";
    }

    [Fact]
    public void NonNullableIndexerGetterWithFirstArgumentNull()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => IgnoreValue(instance[nonNullParam1: null, nonNullParam2: null]));
        Assert.Equal("nonNullParam1", exception.ParamName);
        Assert.Equal("Fail: [NullGuard] nonNullParam1 is null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void NonNullableIndexerGetterWithSecondArgumentNull()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => IgnoreValue(instance[nonNullParam1: "arg 1", nonNullParam2: null]));
        Assert.Equal("nonNullParam2", exception.ParamName);
        Assert.Equal("Fail: [NullGuard] nonNullParam2 is null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void NonNullableIndexerGetterWithNonNullArguments()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("NonNullable"));
        Assert.Equal("return value of NonNullable", instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"]);
    }

    [Fact]
    public void PassThroughGetterReturnValueWithNullArgument()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("PassThroughGetterReturnValue"));
        Assert.Throws<InvalidOperationException>(() => IgnoreValue(instance[returnValue: null]));
        Assert.Equal(
            "Fail: [NullGuard] Return value of property 'System.String Indexers/PassThroughGetterReturnValue::Item(System.String)' is null.",
            AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void PassThroughGetterReturnValueWithNonNullArgument()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("PassThroughGetterReturnValue"));
        Assert.Equal("not null", instance[returnValue: "not null"]);
    }

    [Fact]
    public void AllowedNullsIndexerSetter()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("AllowedNulls"));
        instance[allowNull: null, nullableInt: null] = null;
    }

    [Fact]
    public void AllowedNullsIndexerGetter()
    {
        var instance = (dynamic) Activator.CreateInstance(indexersClassType.GetNestedType("AllowedNulls"));
        Assert.Equal(null, instance[allowNull: null, nullableInt: null]);
    }

    // ReSharper disable once UnusedParameter.Local
    void IgnoreValue(object value)
    {
    }
}