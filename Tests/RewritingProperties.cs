using System;
using NUnit.Framework;

[TestFixture]
public class RewritingProperties
{
    [SetUp]
    public void SetUp()
    {
        AssemblyWeaver.TestListener.Reset();
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.SampleClassTypes))]
    public void PropertySetterRequiresNonNullArgument(Type sampleClassType)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        var exception = Assert.Throws<ArgumentNullException>(() => { sample.NonNullProperty = null; });
        Assert.AreEqual("value", exception.ParamName);
        Assert.AreEqual("[NullGuard] Cannot set the value of property 'System.String SimpleClass::NonNullProperty()' to null.\r\nParameter name: value", exception.Message);
        Assert.AreEqual("Fail: [NullGuard] Cannot set the value of property 'System.String SimpleClass::NonNullProperty()' to null.", AssemblyWeaver.TestListener.Message);
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.SampleClassTypes))]
    public void PropertyGetterRequiresNonNullReturnValue(Type sampleClassType)
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

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.GenericClassTypes))]
    public void GenericPropertyGetterRequiresNonNullReturnValue(Type genericClassType)
    {
        var sample = (dynamic)Activator.CreateInstance(genericClassType.MakeGenericType(new [] {typeof(string)}));
        Assert.Throws<InvalidOperationException>(() =>
        {
            // ReSharper disable UnusedVariable
            var temp = sample.NonNullProperty;

            // ReSharper restore UnusedVariable
        });
        Assert.AreEqual("Fail: [NullGuard] Return value of property 'T GenericClass`1::NonNullProperty()' is null.", AssemblyWeaver.TestListener.Message);
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.SampleClassTypes))]
    public void PropertyAllowsNullGetButNotSet(Type sampleClassType)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        Assert.Null(sample.PropertyAllowsNullGetButDoesNotAllowNullSet);
        Assert.Throws<ArgumentNullException>(() => { sample.NonNullProperty = null; });
        Assert.AreEqual("Fail: [NullGuard] Cannot set the value of property 'System.String SimpleClass::NonNullProperty()' to null.", AssemblyWeaver.TestListener.Message);
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.SampleClassTypes))]
    public void PropertyAllowsNullSetButNotGet(Type sampleClassType)
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

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.SampleClassTypes))]
    public void PropertySetterRequiresAllowsNullArgumentForNullableType(Type sampleClassType)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.NonNullNullableProperty = null;
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.ClassWithPrivateMethodTypes))]
    public void DoesNotRequireNullSetterWhenPropertiesNotSpecifiedByAttribute(Type classWithPrivateMethodType)
    {
        var sample = (dynamic)Activator.CreateInstance(classWithPrivateMethodType);
        sample.SomeProperty = null;
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.ClassToExcludeTypes))]
    public void AllowsNullWhenClassMatchExcludeRegex(Type classToExcludeType)
    {
        var classToExclude = (dynamic) Activator.CreateInstance(classToExcludeType, "");
        classToExclude.Property = null;
        string result = classToExclude.Property;
    }
}