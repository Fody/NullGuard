using System;
using NUnit.Framework;

[TestFixture]
public class RewritingProperties
{
    Type sampleClassType;
    Type genericClassType;
    Type classWithPrivateMethodType;
    Type classToExcludeType;

    [SetUp]
    public void SetUp()
    {
        sampleClassType = AssemblyWeaver.Assemblies[0].GetType("SimpleClass");
        genericClassType = AssemblyWeaver.Assemblies[0].GetType("GenericClass`1").MakeGenericType(new[] { typeof(string) });
        classWithPrivateMethodType = AssemblyWeaver.Assemblies[0].GetType("ClassWithPrivateMethod");
        classToExcludeType = AssemblyWeaver.Assemblies[1].GetType("ClassToExclude");

        AssemblyWeaver.TestListener.Reset();
    }

    [Test]
    public void PropertySetterRequiresNonNullArgument()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        var exception = Assert.Throws<ArgumentNullException>(() => { sample.NonNullProperty = null; });
        Assert.AreEqual("value", exception.ParamName);
        Assert.AreEqual("[NullGuard] Cannot set the value of property 'System.String SimpleClass::NonNullProperty()' to null.\r\nParameter name: value", exception.Message);
        Assert.AreEqual("Fail: [NullGuard] Cannot set the value of property 'System.String SimpleClass::NonNullProperty()' to null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void PropertyGetterRequiresNonNullReturnValue()
    {
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
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        Assert.Null(sample.PropertyAllowsNullGetButDoesNotAllowNullSet);
        Assert.Throws<ArgumentNullException>(() => { sample.NonNullProperty = null; });
        Assert.AreEqual("Fail: [NullGuard] Cannot set the value of property 'System.String SimpleClass::NonNullProperty()' to null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void PropertyAllowsNullSetButNotGet()
    {
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