using System;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;

#if (DEBUG)
using System.Threading.Tasks;
#endif

public class RewritingMethods :
    VerifyBase
{
    [Fact]
    public Task RequiresNonNullArgumentForExplicitInterface()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassWithExplicitInterface");
        var sample = (IComparable<string>) Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.CompareTo(null));
        return Verify(exception.Message);
    }

    [Fact]
    public Task RequiresNonNullArgumentForInternalClassWithExplicitPublicInterface()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassWithExplicitInterface");
        var sample = (dynamic) Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.CallInternalClassWithPublicInterface(null));
        return Verify(exception.Message);
    }

    [Fact]
    public void AllowsNullForInternalClassWithExplicitPrivateInterface()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassWithExplicitInterface");
        var sample = (dynamic) Activator.CreateInstance(type);
        sample.CallInternalClassWithPrivateInterface(null);
    }

    [Fact]
    public Task RequiresNonNullArgumentForImplicitInterface()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassWithImplicitInterface");
        var sample = (IComparable<string>) Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.CompareTo(null));
        return Verify(exception.Message);
    }

    [Fact]
    public Task RequiresNonNullArgumentForInternalClassWithImplicitPublicInterface()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassWithImplicitInterface");
        var sample = (dynamic) Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.CallInternalClassWithPublicInterface(null));
        return Verify(exception.Message);
    }

    [Fact]
    public void AllowsNullForInternalClassWithImplicitPrivateInterface()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassWithImplicitInterface");
        var sample = (dynamic) Activator.CreateInstance(type);
        sample.CallInternalClassWithPrivateInterface(null);
    }

    [Fact]
    public Task RequiresNonNullArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic) Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() => { sample.SomeMethod(null, ""); });
        Assert.Equal("nonNullArg", exception.ParamName);
        return Verify(exception.Message);
    }

    [Fact]
    public void AllowsNullWhenAttributeApplied()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic) Activator.CreateInstance(type);
        sample.SomeMethod("", null);
    }

    [Fact]
    public Task RequiresNonNullMethodReturnValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic) Activator.CreateInstance(type);
        var exception = Assert.Throws<InvalidOperationException>(() => sample.MethodWithReturnValue(true));
        return Verify(exception.Message);
    }

    [Fact]
    public Task RequiresNonNullGenericMethodReturnValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic) Activator.CreateInstance(type);
        var exception = Assert.Throws<InvalidOperationException>(() => sample.MethodWithGenericReturn<object>(true));
        return Verify(exception.Message);
    }

    [Fact]
    public void AllowsNullReturnValueWhenAttributeApplied()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic) Activator.CreateInstance(type);
        sample.MethodAllowsNullReturnValue();
    }

    [Fact]
    public Task RequiresNonNullOutValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic) Activator.CreateInstance(type);
        var exception = Assert.Throws<InvalidOperationException>(() => { sample.MethodWithOutValue(out string value); });
        return Verify(exception.Message);
    }

    [Fact]
    public void AllowsNullOutValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic) Activator.CreateInstance(type);
        string value;
        sample.MethodWithAllowedNullOutValue(out value);
    }

    [Fact]
    public void DoesNotRequireNonNullForNonPublicMethod()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic) Activator.CreateInstance(type);
        sample.PublicWrapperOfPrivateMethod();
    }

    [Fact]
    public void DoesNotRequireNonNullForOptionalParameter()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic) Activator.CreateInstance(type);
        sample.MethodWithOptionalParameter(optional: null);
    }

    [Fact]
    public Task RequiresNonNullForOptionalParameterWithNonNullDefaultValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic) Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() => { sample.MethodWithOptionalParameterWithNonNullDefaultValue(optional: null); });
        return Verify(exception.Message);
    }

    [Fact]
    public void DoesNotRequireNonNullForOptionalParameterWithNonNullDefaultValueButAllowNullAttribute()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic) Activator.CreateInstance(type);
        sample.MethodWithOptionalParameterWithNonNullDefaultValueButAllowNullAttribute(optional: null);
    }

    [Fact]
    public Task RequiresNonNullForNonPublicMethodWhenAttributeSpecifiesNonPublic()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassWithPrivateMethod");
        var sample = (dynamic) Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() => { sample.PublicWrapperOfPrivateMethod(); });
        return Verify(exception.Message);
    }

    [Fact]
    public void ReturnGuardDoesNotInterfereWithIteratorMethod()
    {
        var type = AssemblyWeaver.Assembly.GetType("SpecialClass");
        var sample = (dynamic) Activator.CreateInstance(type);
        Assert.Equal(new[] {0, 1, 2, 3, 4}, sample.CountTo(5));
    }

#if (DEBUG)

    [Fact]
    public Task RequiresNonNullArgumentAsync()
    {
        var type = AssemblyWeaver.Assembly.GetType("SpecialClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.SomeMethodAsync(null, ""));
        return Verify(exception.Message);
    }

    [Fact]
    public void AllowsNullWhenAttributeAppliedAsync()
    {
        var type = AssemblyWeaver.Assembly.GetType("SpecialClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.SomeMethodAsync("", null);
    }

    [Fact]
    public Task RequiresNonNullMethodReturnValueAsync()
    {
        var type = AssemblyWeaver.Assembly.GetType("SpecialClass");
        var sample = (dynamic)Activator.CreateInstance(type);

        Exception exception = null;

        ((Task<string>)sample.MethodWithReturnValueAsync(true))
            .ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    exception = t.Exception.Flatten().InnerException;
                }
            })
            .Wait();

        Assert.NotNull(exception);
        Assert.IsType<InvalidOperationException>(exception);
        return Verify(exception.Message);
    }

    [Fact]
    public void AllowsNullReturnValueWhenAttributeAppliedAsync()
    {
        var type = AssemblyWeaver.Assembly.GetType("SpecialClass");
        var sample = (dynamic)Activator.CreateInstance(type);

        Exception ex = null;

        ((Task<string>)sample.MethodAllowsNullReturnValueAsync())
            .ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    ex = t.Exception.Flatten().InnerException;
                }
            })
            .Wait();

        Assert.Null(ex);
    }

    [Fact]
    public void NoAwaitWillCompile()
    {
        var type = AssemblyWeaver.Assembly.GetType("SpecialClass");
        var instance = (dynamic)Activator.CreateInstance(type);
        Assert.Equal(42, instance.NoAwaitCode().Result);
    }

#endif

    [Fact]
    public void AllowsNullWhenClassMatchExcludeRegex()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassToExclude");
        var ClassToExclude = (dynamic) Activator.CreateInstance(type, "");
        ClassToExclude.Test(null);
    }

    [Fact]
    public void ReturnValueChecksWithBranchToRetInstruction()
    {
        // This is a regression test for the "Branch to RET" issue described in https://github.com/Fody/NullGuard/issues/61.
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic) Activator.CreateInstance(type);
        var exception = Assert.Throws<InvalidOperationException>(() => sample.ReturnValueChecksWithBranchToRetInstruction());
        Assert.Equal("[NullGuard] Return value of method 'System.String SimpleClass::ReturnValueChecksWithBranchToRetInstruction()' is null.", exception.Message);
    }

    [Fact]
    public void OutValueChecksWithRetInstructionAsSwitchCase()
    {
        // This is a regression test for the "Branch to RET" issue described in https://github.com/Fody/NullGuard/issues/61.
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic) Activator.CreateInstance(type);
        var exception = Assert.Throws<InvalidOperationException>(() => { sample.OutValueChecksWithRetInstructionAsSwitchCase(0, out string value); });
        Assert.Equal("[NullGuard] Out parameter 'outParam' is null.", exception.Message);
    }

    public RewritingMethods(ITestOutputHelper output) :
        base(output)
    {
    }
}