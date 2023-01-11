using System;
using System.Threading.Tasks;
using TestsCommon;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class RewritingProperties
{
    [Fact]
    public Task PropertySetterRequiresNonNullArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() => { sample.NonNullProperty = null; });
        return Verifier.Verify(exception.NormalizedArgumentExceptionMessage());
    }

    [Fact]
    public Task PropertyGetterRequiresNonNullReturnValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<InvalidOperationException>(() =>
        {
            // ReSharper disable UnusedVariable
            var temp = sample.NonNullProperty;

            // ReSharper restore UnusedVariable
        });
        return Verifier.Verify(exception.Message);
    }

    [Fact]
    public Task GenericPropertyGetterRequiresNonNullReturnValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("GenericClass`1");
        var sample = (dynamic)Activator.CreateInstance(type.MakeGenericType(typeof(string)));
        var exception = Assert.Throws<InvalidOperationException>(() =>
        {
            // ReSharper disable UnusedVariable
            var temp = sample.NonNullProperty;

            // ReSharper restore UnusedVariable
        });
        return Verifier.Verify(exception.Message);
    }

    [Fact]
    public Task PropertyAllowsNullGetButNotSet()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        Assert.Null(sample.PropertyAllowsNullGetButDoesNotAllowNullSet);
        var exception = Assert.Throws<ArgumentNullException>(() => { sample.NonNullProperty = null; });
        return Verifier.Verify(exception.NormalizedArgumentExceptionMessage());
    }

    [Fact]
    public Task PropertyAllowsNullSetButNotGet()
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
        return Verifier.Verify(exception.Message);
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
}