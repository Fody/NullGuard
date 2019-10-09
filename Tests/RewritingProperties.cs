using System;
using ApprovalTests;
using Xunit;
using Xunit.Abstractions;

public class RewritingProperties :
    XunitApprovalBase
{
    [Fact]
    public void PropertySetterRequiresNonNullArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() => { sample.NonNullProperty = null; });
        Approvals.Verify(exception.Message);
    }

    [Fact]
    public void PropertyGetterRequiresNonNullReturnValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<InvalidOperationException>(() =>
        {
            // ReSharper disable UnusedVariable
            var temp = sample.NonNullProperty;

            // ReSharper restore UnusedVariable
        });
        Approvals.Verify(exception.Message);
    }

    [Fact]
    public void GenericPropertyGetterRequiresNonNullReturnValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("GenericClass`1");
        var sample = (dynamic)Activator.CreateInstance(type.MakeGenericType(typeof(string)));
        var exception = Assert.Throws<InvalidOperationException>(() =>
        {
            // ReSharper disable UnusedVariable
            var temp = sample.NonNullProperty;

            // ReSharper restore UnusedVariable
        });
        Approvals.Verify(exception.Message);
    }

    [Fact]
    public void PropertyAllowsNullGetButNotSet()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        Assert.Null(sample.PropertyAllowsNullGetButDoesNotAllowNullSet);
        var exception = Assert.Throws<ArgumentNullException>(() => { sample.NonNullProperty = null; });
        Approvals.Verify(exception.Message);
    }

    [Fact]
    public void PropertyAllowsNullSetButNotGet()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.PropertyAllowsNullSetButDoesNotAllowNullGet = null;
        var exception = Assert.Throws<InvalidOperationException>(() =>
        {
            // ReSharper disable UnusedVariable
            var temp = sample.PropertyAllowsNullSetButDoesNotAllowNullGet;

            // ReSharper restore UnusedVariable
        });
        Approvals.Verify(exception.Message);
    }

    [Fact]
    public void PropertySetterRequiresAllowsNullArgumentForNullableType()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.NonNullNullableProperty = null;
    }

    [Fact]
    public void DoesNotRequireNullSetterWhenPropertiesNotSpecifiedByAttribute()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassWithPrivateMethod");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.SomeProperty = null;
    }

    [Fact]
    public void AllowsNullWhenClassMatchExcludeRegex()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassToExclude");
        var classToExclude = (dynamic) Activator.CreateInstance(type, "");
        classToExclude.Property = null;
        string result = classToExclude.Property;
    }

    public RewritingProperties(ITestOutputHelper output) :
        base(output)
    {
    }
}