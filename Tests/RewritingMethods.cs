using System;

#if (DEBUG)
using System.Threading.Tasks;
#endif

using Xunit;

public class RewritingMethods
{
    Type sampleClassType;
    Type classWithPrivateMethodType;
    Type specialClassType;
    Type classToExcludeType;
    Type classWithExplicitInterfaceType;

    public RewritingMethods()
    {
        sampleClassType = AssemblyWeaver.Assemblies[0].GetType("SimpleClass");
        classWithPrivateMethodType = AssemblyWeaver.Assemblies[0].GetType("ClassWithPrivateMethod");
        specialClassType = AssemblyWeaver.Assemblies[0].GetType("SpecialClass");
        classToExcludeType = AssemblyWeaver.Assemblies[1].GetType("ClassToExclude");
        classWithExplicitInterfaceType = AssemblyWeaver.Assemblies[0].GetType("ClassWithExplicitInterface");

        AssemblyWeaver.TestListener.Reset();
    }

    [Fact]
    public void RequiresNonNullArgumentForExplicitInterface()
    {
        AssemblyWeaver.TestListener.Reset();
        var sample = (IComparable<string>)Activator.CreateInstance(classWithExplicitInterfaceType);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.CompareTo(null));
        Assert.Equal("other", exception.ParamName);
        Assert.Equal("Fail: [NullGuard] other is null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void RequiresNonNullArgument()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        var exception = Assert.Throws<ArgumentNullException>(() =>
        {
            sample.SomeMethod(null, "");
        });
        Assert.Equal("nonNullArg", exception.ParamName);
        Assert.Equal("Fail: [NullGuard] nonNullArg is null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void AllowsNullWhenAttributeApplied()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.SomeMethod("", null);
    }

    [Fact]
    public void RequiresNonNullMethodReturnValue()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithReturnValue(true));
        Assert.Equal("Fail: [NullGuard] Return value of method 'System.String SimpleClass::MethodWithReturnValue(System.Boolean)' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void RequiresNonNullGenericMethodReturnValue()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithGenericReturn<object>(true));
        Assert.Equal("Fail: [NullGuard] Return value of method 'T SimpleClass::MethodWithGenericReturn(System.Boolean)' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void AllowsNullReturnValueWhenAttributeApplied()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.MethodAllowsNullReturnValue();
    }

    [Fact]
    public void RequiresNonNullOutValue()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        string value;
        Assert.Throws<InvalidOperationException>(() =>
        {
            sample.MethodWithOutValue(out value);
        });
        Assert.Equal("Fail: [NullGuard] Out parameter 'nonNullOutArg' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void AllowsNullOutValue()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        string value;
        sample.MethodWithAllowedNullOutValue(out value);
    }

    [Fact]
    public void DoesNotRequireNonNullForNonPublicMethod()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.PublicWrapperOfPrivateMethod();
    }

    [Fact]
    public void DoesNotRequireNonNullForOptionalParameter()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.MethodWithOptionalParameter(optional: null);
    }

    [Fact]
    public void RequiresNonNullForOptionalParameterWithNonNullDefaultValue()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        var exception = Assert.Throws<ArgumentNullException>(() =>
        {
            sample.MethodWithOptionalParameterWithNonNullDefaultValue(optional: null);
        });
        Assert.Equal("optional", exception.ParamName);
        Assert.Equal("Fail: [NullGuard] optional is null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void DoesNotRequireNonNullForOptionalParameterWithNonNullDefaultValueButAllowNullAttribute()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.MethodWithOptionalParameterWithNonNullDefaultValueButAllowNullAttribute(optional: null);
    }

    [Fact(Skip = "Fails on AppVeyor - TODO")]
    public void RequiresNonNullForNonPublicMethodWhenAttributeSpecifiesNonPublic()
    {
        var sample = (dynamic)Activator.CreateInstance(classWithPrivateMethodType);
        Assert.Throws<ArgumentNullException>(() => sample.PublicWrapperOfPrivateMethod());
        Assert.Equal("Fail: [NullGuard] x is null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void ReturnGuardDoesNotInterfereWithIteratorMethod()
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType);
        //Assert.That(new[] { 0, 1, 2, 3, 4 }, Is.EquivalentTo(sample.CountTo(5)));
    }

#if (DEBUG)

    [Fact]
    public void RequiresNonNullArgumentAsync()
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.SomeMethodAsync(null, ""));
        Assert.Equal("nonNullArg", exception.ParamName);
        Assert.Equal("Fail: [NullGuard] nonNullArg is null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void AllowsNullWhenAttributeAppliedAsync()
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType);
        sample.SomeMethodAsync("", null);
    }

    [Fact]
    public void RequiresNonNullMethodReturnValueAsync()
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType);

        Exception ex = null;

        ((Task<string>)sample.MethodWithReturnValueAsync(true))
            .ContinueWith(t =>
            {
                if (t.IsFaulted)
                    ex = t.Exception.Flatten().InnerException;
            })
            .Wait();

        Assert.NotNull(ex);
        Assert.IsType<InvalidOperationException>(ex);
        Assert.Equal("[NullGuard] Return value of method 'System.Threading.Tasks.Task`1<System.String> SpecialClass::MethodWithReturnValueAsync(System.Boolean)' is null.", ex.Message);
        Assert.Equal("Fail: [NullGuard] Return value of method 'System.Threading.Tasks.Task`1<System.String> SpecialClass::MethodWithReturnValueAsync(System.Boolean)' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void AllowsNullReturnValueWhenAttributeAppliedAsync()
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType);

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
        var instance = (dynamic)Activator.CreateInstance(specialClassType);
        Assert.Equal(42, instance.NoAwaitCode().Result);
    }

#endif

    [Fact]
    public void AllowsNullWhenClassMatchExcludeRegex()
    {
        var ClassToExclude = (dynamic)Activator.CreateInstance(classToExcludeType, "");
        ClassToExclude.Test(null);
    }

    [Fact]
    public void ReturnValueChecksWithBranchToRetInstruction()
    {
        // This is a regression test for the "Branch to RET" issue described in https://github.com/Fody/NullGuard/issues/61.

        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        var exception = Assert.Throws<InvalidOperationException>(() => sample.ReturnValueChecksWithBranchToRetInstruction());
        Assert.Equal("[NullGuard] Return value of method 'System.String SimpleClass::ReturnValueChecksWithBranchToRetInstruction()' is null.", exception.Message);
    }

    [Fact]
    public void OutValueChecksWithRetInstructionAsSwitchCase()
    {
        // This is a regression test for the "Branch to RET" issue described in https://github.com/Fody/NullGuard/issues/61.

        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        string value;
        var exception = Assert.Throws<InvalidOperationException>(() =>
        {
            sample.OutValueChecksWithRetInstructionAsSwitchCase(0, out value);
        });
        Assert.Equal("[NullGuard] Out parameter 'outParam' is null.", exception.Message);
    }
}