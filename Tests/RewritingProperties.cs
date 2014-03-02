using System;
using NUnit.Framework;

[TestFixture]
public class RewritingProperties
{
    private Type sampleClassType;
    private Type genericClassType;
    private Type classWithPrivateMethodType;
    private Type classToExcludeType;

    public RewritingProperties()
    {
        sampleClassType = AssemblyWeaver.Assemblies[0].GetType("SimpleClass");
        genericClassType = AssemblyWeaver.Assemblies[0].GetType("GenericClass`1").MakeGenericType(new[] { typeof(string) });
        classWithPrivateMethodType = AssemblyWeaver.Assemblies[0].GetType("ClassWithPrivateMethod");
        classToExcludeType = AssemblyWeaver.Assemblies[1].GetType("ClassToExclude");
    }

    [Test]
    public void PropertySetterRequiresNonNullArgument()
    {
        AssemblyWeaver.TestListener.Reset();
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        var exception = Assert.Throws<ArgumentNullException>(() => { sample.NonNullProperty = null; });
        Assert.AreEqual("value", exception.ParamName);
        Assert.AreEqual("[NullGuard] Cannot set the value of property 'System.String SimpleClass::NonNullProperty()' to null.\r\nParameter name: value", exception.Message);
        Assert.AreEqual("Fail: [NullGuard] Cannot set the value of property 'System.String SimpleClass::NonNullProperty()' to null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void PropertyGetterRequiresNonNullReturnValue()
    {
        AssemblyWeaver.TestListener.Reset();
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        Assert.Throws<InvalidOperationException>(() =>
        {
            // ReSharper disable UnusedVariable
            var temp = sample.NonNullProperty;

            // ReSharper restore UnusedVariable
        });
        Assert.AreEqual("Fail: [NullGuard] Return value of property 'System.String SimpleClass::NonNullProperty()' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void GenericPropertyGetterRequiresNonNullReturnValue()
    {
        AssemblyWeaver.TestListener.Reset();
        var sample = (dynamic)Activator.CreateInstance(genericClassType);
        Assert.Throws<InvalidOperationException>(() =>
        {
            // ReSharper disable UnusedVariable
            var temp = sample.NonNullProperty;

            // ReSharper restore UnusedVariable
        });
        Assert.AreEqual("Fail: [NullGuard] Return value of property 'T GenericClass`1::NonNullProperty()' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void PropertyAllowsNullGetButNotSet()
    {
        AssemblyWeaver.TestListener.Reset();
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        Assert.Null(sample.PropertyAllowsNullGetButDoesNotAllowNullSet);
        Assert.Throws<ArgumentNullException>(() => { sample.NonNullProperty = null; });
        Assert.AreEqual("Fail: [NullGuard] Cannot set the value of property 'System.String SimpleClass::NonNullProperty()' to null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void PropertyAllowsNullSetButNotGet()
    {
        AssemblyWeaver.TestListener.Reset();
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.PropertyAllowsNullSetButDoesNotAllowNullGet = null;
        Assert.Throws<InvalidOperationException>(() =>
        {
            // ReSharper disable UnusedVariable
            var temp = sample.PropertyAllowsNullSetButDoesNotAllowNullGet;

            // ReSharper restore UnusedVariable
        });
        Assert.AreEqual("Fail: [NullGuard] Return value of property 'System.String SimpleClass::PropertyAllowsNullSetButDoesNotAllowNullGet()' is null.", AssemblyWeaver.TestListener.Message);
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

    [Test]
    public void AllowsNullWhenClassMatchExcludeRegex()
    {
        var classToExclude = (dynamic) Activator.CreateInstance(classToExcludeType, "");
        classToExclude.Property = null;
        string result = classToExclude.Property;
    }
}