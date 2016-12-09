using System;
using NUnit.Framework;

[TestFixture]
public class RewritingProperties
{
    Func<int, Type> sampleClassType;
    Func<int, Type> genericClassType;
    Func<int, Type> classWithPrivateMethodType;
    Type classToExcludeType;

    [SetUp]
    public void SetUp()
    {
        sampleClassType = index => AssemblyWeaver.RewritingTestAssemblies[index].GetType("SimpleClass");
        genericClassType = index => AssemblyWeaver.RewritingTestAssemblies[index].GetType("GenericClass`1").MakeGenericType(new[] { typeof(string) });
        classWithPrivateMethodType = index => AssemblyWeaver.RewritingTestAssemblies[index].GetType("ClassWithPrivateMethod");
        classToExcludeType = AssemblyWeaver.Assemblies[1].GetType("ClassToExclude");

        AssemblyWeaver.TestListener.Reset();
    }

    [Test]
    public void PropertySetterRequiresNonNullArgument([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType(index));
        var exception = Assert.Throws<ArgumentNullException>(() => { sample.NonNullProperty = null; });
        Assert.AreEqual("value", exception.ParamName);
        Assert.AreEqual("[NullGuard] Cannot set the value of property 'System.String SimpleClass::NonNullProperty()' to null.\r\nParameter name: value", exception.Message);
        Assert.AreEqual("Fail: [NullGuard] Cannot set the value of property 'System.String SimpleClass::NonNullProperty()' to null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void PropertyGetterRequiresNonNullReturnValue([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType(index));
        Assert.Throws<InvalidOperationException>(() =>
        {
            // ReSharper disable UnusedVariable
            var temp = sample.NonNullProperty;

            // ReSharper restore UnusedVariable
        });
        Assert.AreEqual("Fail: [NullGuard] Return value of property 'System.String SimpleClass::NonNullProperty()' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void GenericPropertyGetterRequiresNonNullReturnValue([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(genericClassType(index));
        Assert.Throws<InvalidOperationException>(() =>
        {
            // ReSharper disable UnusedVariable
            var temp = sample.NonNullProperty;

            // ReSharper restore UnusedVariable
        });
        Assert.AreEqual("Fail: [NullGuard] Return value of property 'T GenericClass`1::NonNullProperty()' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void PropertyAllowsNullGetButNotSet([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType(index));
        Assert.Null(sample.PropertyAllowsNullGetButDoesNotAllowNullSet);
        Assert.Throws<ArgumentNullException>(() => { sample.NonNullProperty = null; });
        Assert.AreEqual("Fail: [NullGuard] Cannot set the value of property 'System.String SimpleClass::NonNullProperty()' to null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void PropertyAllowsNullSetButNotGet([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType(index));
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
    public void PropertySetterRequiresAllowsNullArgumentForNullableType([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType(index));
        sample.NonNullNullableProperty = null;
    }

    [Test]
    public void DoesNotRequireNullSetterWhenPropertiesNotSpecifiedByAttribute([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(classWithPrivateMethodType(index));
        sample.SomeProperty = null;
    }

    [Test]
    public void AllowsNullWhenClassMatchExcludeRegex()
    {
        var classToExclude = (dynamic)Activator.CreateInstance(classToExcludeType, "");
        classToExclude.Property = null;
        string result = classToExclude.Property;
    }
}