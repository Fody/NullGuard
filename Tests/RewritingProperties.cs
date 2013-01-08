using System;
using NUnit.Framework;

[TestFixture]
public class RewritingProperties
{
    private Type sampleClassType;
    private Type classWithPrivateMethodType;

    public RewritingProperties()
    {
        sampleClassType = AssemblyWeaver.Assembly.GetType("SampleClass");
        classWithPrivateMethodType = AssemblyWeaver.Assembly.GetType("ClassWithPrivateMethod");
    }

    [Test]
    public void PropertySetterRequiresNonNullArgument()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        var exception = Assert.Throws<ArgumentNullException>(() => { sample.NonNullProperty = null; });
        Assert.AreEqual("value", exception.ParamName);
    }

    [Test]
    public void PropertyGetterRequiresNonNullReturnValue()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        Assert.Throws<InvalidOperationException>(() =>
        {
            var temp = sample.NonNullProperty;
        });
    }

    [Test]
    public void PropertyAllowsNullGetButNotSet()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        Assert.Null(sample.PropertyAllowsNullGetButDoesNotAllowNullSet);
        Assert.Throws<ArgumentNullException>(() => { sample.NonNullProperty = null; });
    }

    [Test]
    public void PropertyAllowsNullSetButNotGet()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.PropertyAllowsNullSetButDoesNotAllowNullGet = null;
        Assert.Throws<InvalidOperationException>(() =>
        {
            var temp = sample.PropertyAllowsNullSetButDoesNotAllowNullGet;
        });
    }

    [Test]
    public void PropertySetterRequiresAllowsNullArgumentForNullableType()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.NonNullNullableProperty = null;
    }

    [Test]
    public void DoesNotRequireNullSetterWhenPropertiesNotSpecifiedByAttribute()
    {
        var sample = (dynamic)Activator.CreateInstance(classWithPrivateMethodType);
        sample.SomeProperty = null;
    }
}