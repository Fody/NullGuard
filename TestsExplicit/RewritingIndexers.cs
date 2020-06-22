using System;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class RewritingIndexers
{
    [Fact]
    public Task NonNullableIndexerSetterWithFirstArgumentNull()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => instance[nonNullParam1: null, nonNullParam2: null] = "value");
        return Verifier.Verify(exception.Message);
    }

    [Fact]
    public Task NonNullableIndexerSetterWithSecondArgumentNull()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => instance[nonNullParam1: "arg 1", nonNullParam2: null] = "value");
        return Verifier.Verify(exception.Message);
    }

    [Fact]
    public Task NonNullableIndexerSetterWithValueArgumentNull()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"] = null);
        return Verifier.Verify(exception.Message);
    }

    [Fact]
    public void NonNullableIndexerSetterWithNonNullArguments()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"] = "value";
    }

    [Fact]
    public Task NonNullableIndexerGetterWithFirstArgumentNull()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => IgnoreValue(instance[nonNullParam1: null, nonNullParam2: null]));
        return Verifier.Verify(exception.Message);
    }

    [Fact]
    public Task NonNullableIndexerGetterWithSecondArgumentNull()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        var exception = Assert.Throws<ArgumentNullException>(() => IgnoreValue(instance[nonNullParam1: "arg 1", nonNullParam2: null]));
        return Verifier.Verify(exception.Message);
    }

    [Fact]
    public void NonNullableIndexerGetterWithNonNullArguments()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        Assert.Equal("return value of NonNullable", instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"]);
    }

    [Fact]
    public Task PassThroughGetterReturnValueWithNullArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("PassThroughGetterReturnValue"));
        var exception = Assert.Throws<InvalidOperationException>(() => IgnoreValue(instance[returnValue: null]));
        return Verifier.Verify(exception.Message);
    }

    [Fact]
    public void PassThroughGetterReturnValueWithNonNullArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("PassThroughGetterReturnValue"));
        Assert.Equal("not null", instance[returnValue: "not null"]);
    }

    [Fact]
    public void AllowedNullsIndexerSetter()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("AllowedNulls"));
        instance[allowNull: null, nullableInt: null] = null;
    }

    [Fact]
    public void AllowedNullsIndexerGetter()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("AllowedNulls"));
        Assert.Equal(null, instance[allowNull: null, nullableInt: null]);
    }

    // ReSharper disable once UnusedParameter.Local
    void IgnoreValue(object value)
    {
    }
}