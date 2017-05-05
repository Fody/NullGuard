using System;

#if (DEBUG)
using System.Threading.Tasks;
#endif

using NUnit.Framework;

[TestFixture]
public class RewritingMethods
{
    Type sampleClassType;
    Type classWithPrivateMethodType;
    Type specialClassType;
    Type classToExcludeType;
    Type classWithExplicitInterfaceType;

    [SetUp]
    public void SetUp()
    {
        sampleClassType = AssemblyWeaver.Assemblies[0].GetType("SimpleClass");
        classWithPrivateMethodType = AssemblyWeaver.Assemblies[0].GetType("ClassWithPrivateMethod");
        specialClassType = AssemblyWeaver.Assemblies[0].GetType("SpecialClass");
        classToExcludeType = AssemblyWeaver.Assemblies[1].GetType("ClassToExclude");
        classWithExplicitInterfaceType = AssemblyWeaver.Assemblies[0].GetType("ClassWithExplicitInterface");

        AssemblyWeaver.TestListener.Reset();
    }

    [Test]
    public void RequiresNonNullArgumentForExplicitInterface()
    {
        AssemblyWeaver.TestListener.Reset();
        var sample = (IComparable<string>)Activator.CreateInstance(classWithExplicitInterfaceType);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.CompareTo(null));
        Assert.AreEqual("other", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] other is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void RequiresNonNullArgument()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.SomeMethod(null, ""));
        Assert.AreEqual("nonNullArg", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullArg is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void AllowsNullWhenAttributeApplied()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.SomeMethod("", null);
    }

    [Test]
    public void RequiresNonNullMethodReturnValue()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithReturnValue(true));
        Assert.AreEqual("Fail: [NullGuard] Return value of method 'System.String SimpleClass::MethodWithReturnValue(System.Boolean)' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void RequiresNonNullGenericMethodReturnValue()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithGenericReturn<object>(true));
        Assert.AreEqual("Fail: [NullGuard] Return value of method 'T SimpleClass::MethodWithGenericReturn(System.Boolean)' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void AllowsNullReturnValueWhenAttributeApplied()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.MethodAllowsNullReturnValue();
    }

    [Test]
    public void RequiresNonNullOutValue()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        string value;
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithOutValue(out value));
        Assert.AreEqual("Fail: [NullGuard] Out parameter 'nonNullOutArg' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void AllowsNullOutValue()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        string value;
        sample.MethodWithAllowedNullOutValue(out value);
    }

    [Test]
    public void DoesNotRequireNonNullForNonPublicMethod()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.PublicWrapperOfPrivateMethod();
    }

    [Test]
    public void DoesNotRequireNonNullForOptionalParameter()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.MethodWithOptionalParameter(optional: null);
    }

    [Test]
    public void RequiresNonNullForOptionalParameterWithNonNullDefaultValue()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.MethodWithOptionalParameterWithNonNullDefaultValue(optional: null));
        Assert.AreEqual("optional", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] optional is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void DoesNotRequireNonNullForOptionalParameterWithNonNullDefaultValueButAllowNullAttribute()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.MethodWithOptionalParameterWithNonNullDefaultValueButAllowNullAttribute(optional: null);
    }

    [Test, Explicit("Fails on AppVeyor - TODO")]
    public void RequiresNonNullForNonPublicMethodWhenAttributeSpecifiesNonPublic()
    {
        var sample = (dynamic)Activator.CreateInstance(classWithPrivateMethodType);
        Assert.Throws<ArgumentNullException>(() => sample.PublicWrapperOfPrivateMethod());
        Assert.AreEqual("Fail: [NullGuard] x is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void ReturnGuardDoesNotInterfereWithIteratorMethod()
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType);
        Assert.That(new[] { 0, 1, 2, 3, 4 }, Is.EquivalentTo(sample.CountTo(5)));
    }

#if (DEBUG)

    [Test]
    public void RequiresNonNullArgumentAsync()
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.SomeMethodAsync(null, ""));
        Assert.AreEqual("nonNullArg", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullArg is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void AllowsNullWhenAttributeAppliedAsync()
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType);
        sample.SomeMethodAsync("", null);
    }

    [Test]
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
        Assert.IsInstanceOf<InvalidOperationException>(ex);
        Assert.AreEqual("[NullGuard] Return value of method 'System.Threading.Tasks.Task`1<System.String> SpecialClass::MethodWithReturnValueAsync(System.Boolean)' is null.", ex.Message);
        Assert.AreEqual("Fail: [NullGuard] Return value of method 'System.Threading.Tasks.Task`1<System.String> SpecialClass::MethodWithReturnValueAsync(System.Boolean)' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
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

    [Test]
    public void NoAwaitWillCompile()
    {
        var instance = (dynamic)Activator.CreateInstance(specialClassType);
        Assert.AreEqual(42, instance.NoAwaitCode().Result);
    }

#endif

    [Test]
    public void AllowsNullWhenClassMatchExcludeRegex()
    {
        var ClassToExclude = (dynamic)Activator.CreateInstance(classToExcludeType, "");
        ClassToExclude.Test(null);
    }

    [Test]
    public void ReturnValueChecksWithBranchToRetInstruction()
    {
        // This is a regression test for the "Branch to RET" issue described in https://github.com/Fody/NullGuard/issues/61.

        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        var exception = Assert.Throws<InvalidOperationException>(() => sample.ReturnValueChecksWithBranchToRetInstruction());
        Assert.AreEqual("[NullGuard] Return value of method 'System.String SimpleClass::ReturnValueChecksWithBranchToRetInstruction()' is null.", exception.Message);
    }

    [Test]
    public void OutValueChecksWithRetInstructionAsSwitchCase()
    {
        // This is a regression test for the "Branch to RET" issue described in https://github.com/Fody/NullGuard/issues/61.

        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        string value;
        var exception = Assert.Throws<InvalidOperationException>(() => sample.OutValueChecksWithRetInstructionAsSwitchCase(0, out value));
        Assert.AreEqual("[NullGuard] Out parameter 'outParam' is null.", exception.Message);
    }
}