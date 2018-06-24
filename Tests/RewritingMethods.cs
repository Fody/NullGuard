using System;
#if (NET46)
using ApprovalTests;
#endif
using Xunit;
#if (DEBUG)
using System.Threading.Tasks;
#endif

public class RewritingMethods
{
    [Fact]
    public void RequiresNonNullArgumentForExplicitInterface()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassWithExplicitInterface");
        var sample = (IComparable<string>)Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.CompareTo(null));
#if (NET46)
        Approvals.Verify(exception.Message);
#endif
    }

    [Fact]
    public void RequiresNonNullArgumentForInternalClassWithExplicitPublicInterface()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassWithExplicitInterface");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.CallInternalClassWithPublicInterface(null));
#if (NET46)
        Approvals.Verify(exception.Message);
#endif
    }

    [Fact]
    public void AllowsNullForInternalClassWithExplicitPrivateInterface()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassWithExplicitInterface");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.CallInternalClassWithPrivateInterface(null);
    }

    [Fact]
    public void RequiresNonNullArgumentForImplicitInterface()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassWithImplicitInterface");
        var sample = (IComparable<string>)Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.CompareTo(null));
#if (NET46)
        Approvals.Verify(exception.Message);
#endif
    }

    [Fact]
    public void RequiresNonNullArgumentForInternalClassWithImplicitPublicInterface()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassWithImplicitInterface");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.CallInternalClassWithPublicInterface(null));
#if (NET46)
        Approvals.Verify(exception.Message);
#endif
    }

    [Fact]
    public void AllowsNullForInternalClassWithImplicitPrivateInterface()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassWithImplicitInterface");
        var sample = (dynamic) Activator.CreateInstance(type);
        sample.CallInternalClassWithPrivateInterface(null);
    }

    [Fact]
    public void RequiresNonNullArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() =>
        {
            sample.SomeMethod(null, "");
        });
        Assert.Equal("nonNullArg", exception.ParamName);
#if (NET46)
        Approvals.Verify(exception.Message);
#endif
    }

    [Fact]
    public void AllowsNullWhenAttributeApplied()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.SomeMethod("", null);
    }

    [Fact]
    public void RequiresNonNullMethodReturnValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<InvalidOperationException>(() => sample.MethodWithReturnValue(true));
#if (NET46)
        Approvals.Verify(exception.Message);
#endif
    }

    [Fact]
    public void RequiresNonNullGenericMethodReturnValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<InvalidOperationException>(() => sample.MethodWithGenericReturn<object>(true));
#if (NET46)
        Approvals.Verify(exception.Message);
#endif
    }

    [Fact]
    public void AllowsNullReturnValueWhenAttributeApplied()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.MethodAllowsNullReturnValue();
    }

    [Fact]
    public void RequiresNonNullOutValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<InvalidOperationException>(() =>
        {
            sample.MethodWithOutValue(out string value);
        });
#if (NET46)
        Approvals.Verify(exception.Message);
#endif
    }

    [Fact]
    public void AllowsNullOutValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        string value;
        sample.MethodWithAllowedNullOutValue(out value);
    }

    [Fact]
    public void DoesNotRequireNonNullForNonPublicMethod()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.PublicWrapperOfPrivateMethod();
    }

    [Fact]
    public void DoesNotRequireNonNullForOptionalParameter()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.MethodWithOptionalParameter(optional: null);
    }

    [Fact]
    public void RequiresNonNullForOptionalParameterWithNonNullDefaultValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() =>
        {
            sample.MethodWithOptionalParameterWithNonNullDefaultValue(optional: null);
        });
#if (NET46)
        Approvals.Verify(exception.Message);
#endif
    }

    [Fact]
    public void DoesNotRequireNonNullForOptionalParameterWithNonNullDefaultValueButAllowNullAttribute()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.MethodWithOptionalParameterWithNonNullDefaultValueButAllowNullAttribute(optional: null);
    }

    [Fact]
    public void RequiresNonNullForNonPublicMethodWhenAttributeSpecifiesNonPublic()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassWithPrivateMethod");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() =>
        {
            sample.PublicWrapperOfPrivateMethod();
        });
#if (NET46)
        Approvals.Verify(exception.Message);
#endif
    }

    [Fact]
    public void ReturnGuardDoesNotInterfereWithIteratorMethod()
    {
        var type = AssemblyWeaver.Assembly.GetType("SpecialClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        Assert.Equal(new[] { 0, 1, 2, 3, 4 }, sample.CountTo(5));
    }

#if (DEBUG)

    [Fact]
    public void RequiresNonNullArgumentAsync()
    {
        var type = AssemblyWeaver.Assembly.GetType("SpecialClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.SomeMethodAsync(null, ""));
#if (NET46)
        Approvals.Verify(exception.Message);
#endif
    }

    [Fact]
    public void AllowsNullWhenAttributeAppliedAsync()
    {
        var type = AssemblyWeaver.Assembly.GetType("SpecialClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.SomeMethodAsync("", null);
    }

    [Fact]
    public void RequiresNonNullMethodReturnValueAsync()
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
#if (NET46)
        Approvals.Verify(exception.Message);
#endif
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
                    ex = t.Exception.Flatten().InnerException;
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
        var ClassToExclude = (dynamic)Activator.CreateInstance(type, "");
        ClassToExclude.Test(null);
    }

    [Fact]
    public void ReturnValueChecksWithBranchToRetInstruction()
    {
        // This is a regression test for the "Branch to RET" issue described in https://github.com/Fody/NullGuard/issues/61.
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<InvalidOperationException>(() => sample.ReturnValueChecksWithBranchToRetInstruction());
        Assert.Equal("[NullGuard] Return value of method 'System.String SimpleClass::ReturnValueChecksWithBranchToRetInstruction()' is null.", exception.Message);
    }

    [Fact]
    public void OutValueChecksWithRetInstructionAsSwitchCase()
    {
        // This is a regression test for the "Branch to RET" issue described in https://github.com/Fody/NullGuard/issues/61.
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<InvalidOperationException>(() =>
        {
            sample.OutValueChecksWithRetInstructionAsSwitchCase(0, out string value);
        });
        Assert.Equal("[NullGuard] Out parameter 'outParam' is null.", exception.Message);
    }
}