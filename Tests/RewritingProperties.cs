using System;
using System.Threading.Tasks;
using TestsCommon;
using VerifyTUnit;

public class RewritingProperties
{
    [Test]
    public async Task PropertySetterRequiresNonNullArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Shared.Throws<ArgumentNullException>(() => { sample.NonNullProperty = null; });
        await Verifier.Verify(exception.NormalizedArgumentExceptionMessage());
    }

    [Test]
    public async Task PropertyGetterRequiresNonNullReturnValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Shared.Throws<InvalidOperationException>(() =>
        {
            // ReSharper disable UnusedVariable
            var temp = sample.NonNullProperty;

            // ReSharper restore UnusedVariable
        });
        await Verifier.Verify(exception.Message);
    }

    [Test]
    public async Task GenericPropertyGetterRequiresNonNullReturnValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("GenericClass`1");
        var sample = (dynamic)Activator.CreateInstance(type.MakeGenericType(typeof(string)));
        var exception = Shared.Throws<InvalidOperationException>(() =>
        {
            // ReSharper disable UnusedVariable
            var temp = sample.NonNullProperty;

            // ReSharper restore UnusedVariable
        });
        await Verifier.Verify(exception.Message);
    }

    [Test]
    public async Task PropertyAllowsNullGetButNotSet()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        await Assert.That((object)sample.PropertyAllowsNullGetButDoesNotAllowNullSet).IsNull();
        var exception = Shared.Throws<ArgumentNullException>(() => { sample.NonNullProperty = null; });
        await Verifier.Verify(exception.NormalizedArgumentExceptionMessage());
    }

    [Test]
    public async Task PropertyAllowsNullSetButNotGet()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.PropertyAllowsNullSetButDoesNotAllowNullGet = null;
        var exception = Shared.Throws<InvalidOperationException>(() =>
        {
            // ReSharper disable UnusedVariable
            var temp = sample.PropertyAllowsNullSetButDoesNotAllowNullGet;

            // ReSharper restore UnusedVariable
        });
        await Verifier.Verify(exception.Message);
    }

    [Test]
    public void PropertySetterRequiresAllowsNullArgumentForNullableType()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.NonNullNullableProperty = null;
    }

    [Test]
    public void DoesNotRequireNullSetterWhenPropertiesNotSpecifiedByAttribute()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassWithPrivateMethod");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.SomeProperty = null;
    }

    [Test]
    public void AllowsNullWhenClassMatchExcludeRegex()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassToExclude");
        var classToExclude = (dynamic) Activator.CreateInstance(type, "");
        classToExclude.Property = null;
        string result = classToExclude.Property;
    }
}