using System;
using Xunit;
[assembly: CollectionBehavior(DisableTestParallelization = true)]

public class RewritingProperties
{
    Type sampleClassType;
    Type genericClassType;
    Type classWithPrivateMethodType;
    Type classToExcludeType;

    public RewritingProperties()
    {
        sampleClassType = AssemblyWeaver.Assemblies[0].GetType("SimpleClass");
        genericClassType = AssemblyWeaver.Assemblies[0].GetType("GenericClass`1").MakeGenericType(new[] { typeof(string) });
        classWithPrivateMethodType = AssemblyWeaver.Assemblies[0].GetType("ClassWithPrivateMethod");
        classToExcludeType = AssemblyWeaver.Assemblies[1].GetType("ClassToExclude");

        AssemblyWeaver.TestListener.Reset();
    }

    [Fact]
    public void PropertySetterRequiresNonNullArgument()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        var exception = Assert.Throws<ArgumentNullException>(() => { sample.NonNullProperty = null; });
        Assert.Equal("value", exception.ParamName);
        Assert.Equal("[NullGuard] Cannot set the value of property 'System.String SimpleClass::NonNullProperty()' to null.\r\nParameter name: value", exception.Message);
        Assert.Equal("Fail: [NullGuard] Cannot set the value of property 'System.String SimpleClass::NonNullProperty()' to null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void PropertyGetterRequiresNonNullReturnValue()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        Assert.Throws<InvalidOperationException>(() =>
        {
            // ReSharper disable UnusedVariable
            var temp = sample.NonNullProperty;

            // ReSharper restore UnusedVariable
        });
        Assert.Equal("Fail: [NullGuard] Return value of property 'System.String SimpleClass::NonNullProperty()' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void GenericPropertyGetterRequiresNonNullReturnValue()
    {
        var sample = (dynamic)Activator.CreateInstance(genericClassType);
        Assert.Throws<InvalidOperationException>(() =>
        {
            // ReSharper disable UnusedVariable
            var temp = sample.NonNullProperty;

            // ReSharper restore UnusedVariable
        });
        Assert.Equal("Fail: [NullGuard] Return value of property 'T GenericClass`1::NonNullProperty()' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void PropertyAllowsNullGetButNotSet()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        Assert.Null(sample.PropertyAllowsNullGetButDoesNotAllowNullSet);
        Assert.Throws<ArgumentNullException>(() => { sample.NonNullProperty = null; });
        Assert.Equal("Fail: [NullGuard] Cannot set the value of property 'System.String SimpleClass::NonNullProperty()' to null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
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
        Assert.Equal("Fail: [NullGuard] Return value of property 'System.String SimpleClass::PropertyAllowsNullSetButDoesNotAllowNullGet()' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void PropertySetterRequiresAllowsNullArgumentForNullableType()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.NonNullNullableProperty = null;
    }

    [Fact]
    public void DoesNotRequireNullSetterWhenPropertiesNotSpecifiedByAttribute()
    {
        var sample = (dynamic)Activator.CreateInstance(classWithPrivateMethodType);
        sample.SomeProperty = null;
    }

    [Fact]
    public void AllowsNullWhenClassMatchExcludeRegex()
    {
        var classToExclude = (dynamic) Activator.CreateInstance(classToExcludeType, "");
        classToExclude.Property = null;
        string result = classToExclude.Property;
    }
}