using System;
using System.Threading.Tasks;
using TestsCommon;
using VerifyTUnit;

// ReSharper disable UnusedParameter.Local
// ReSharper disable MemberCanBeMadeStatic.Local

public class RewritingIndexers
{
    [Test]
    public async Task NonNullableIndexerSetterWithFirstArgumentNull()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        var exception = Shared.Throws<ArgumentNullException>(() => instance[nonNullParam1: null, nonNullParam2: null] = "value");
        await Verifier.Verify(exception.NormalizedArgumentExceptionMessage());
    }

    [Test]
    public async Task NonNullableIndexerSetterWithSecondArgumentNull()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        var exception = Shared.Throws<ArgumentNullException>(() => instance[nonNullParam1: "arg 1", nonNullParam2: null] = "value");
        await Verifier.Verify(exception.NormalizedArgumentExceptionMessage());
    }

    [Test]
    public async Task NonNullableIndexerSetterWithValueArgumentNull()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        var exception = Shared.Throws<ArgumentNullException>(() => instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"] = null);
        await Verifier.Verify(exception.NormalizedArgumentExceptionMessage());
    }

    [Test]
    public void NonNullableIndexerSetterWithNonNullArguments()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"] = "value";
    }

    [Test]
    public async Task NonNullableIndexerGetterWithFirstArgumentNull()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        var exception = Shared.Throws<ArgumentNullException>(() => IgnoreValue(instance[nonNullParam1: null, nonNullParam2: null]));
        await Verifier.Verify(exception.NormalizedArgumentExceptionMessage());
    }

    [Test]
    public async Task NonNullableIndexerGetterWithSecondArgumentNull()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        var exception = Shared.Throws<ArgumentNullException>(() => IgnoreValue(instance[nonNullParam1: "arg 1", nonNullParam2: null]));
        await Verifier.Verify(exception.NormalizedArgumentExceptionMessage());
    }

    [Test]
    public async Task NonNullableIndexerGetterWithNonNullArguments()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("NonNullable"));
        await Assert.That((object)instance[nonNullParam1: "arg 1", nonNullParam2: "arg 2"]).IsEqualTo("return value of NonNullable");
    }

    [Test]
    public async Task PassThroughGetterReturnValueWithNullArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("PassThroughGetterReturnValue"));
        var exception = Shared.Throws<InvalidOperationException>(() => IgnoreValue(instance[returnValue: null]));
        await Verifier.Verify(exception.Message);
    }

    [Test]
    public async Task PassThroughGetterReturnValueWithNonNullArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("PassThroughGetterReturnValue"));
        await Assert.That((object)instance[returnValue: "not null"]).IsEqualTo("not null");
    }

    [Test]
    public void AllowedNullsIndexerSetter()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("AllowedNulls"));
        instance[allowNull: null, nullableInt: null] = null;
    }

    [Test]
    public async Task AllowedNullsIndexerGetter()
    {
        var type = AssemblyWeaver.Assembly.GetType("Indexers");
        var instance = (dynamic) Activator.CreateInstance(type.GetNestedType("AllowedNulls"));
        await Assert.That((object)instance[allowNull: null, nullableInt: null]).IsNull();
    }

    void IgnoreValue(object value)
    {
    }
}