using System;
#if (DEBUG)
using System.Threading.Tasks;
#endif
using NUnit.Framework;

[TestFixture]
public class RewritingMethods
{
    Func<int, Type> sampleClassType;
    Func<int, Type> classWithPrivateMethodType;
    Func<int, Type> specialClassType;
    Type classToExcludeType;
    Func<int, Type> classWithExplicitInterfaceType;

    [SetUp]
    public void SetUp()
    {
        sampleClassType = index => AssemblyWeaver.RewritingTestAssemblies[index].GetType("SimpleClass");
        classWithPrivateMethodType = index => AssemblyWeaver.RewritingTestAssemblies[index].GetType("ClassWithPrivateMethod");
        specialClassType = index => AssemblyWeaver.RewritingTestAssemblies[index].GetType("SpecialClass");
        classToExcludeType = AssemblyWeaver.Assemblies[1].GetType("ClassToExclude");
        classWithExplicitInterfaceType = index => AssemblyWeaver.RewritingTestAssemblies[index].GetType("ClassWithExplicitInterface");

        AssemblyWeaver.TestListener.Reset();
    }

    [Test]
    public void RequiresNonNullArgumentForExplicitInterface([Values(0, 1)] int index)
    {
        AssemblyWeaver.TestListener.Reset();
        var sample = (IComparable<string>)Activator.CreateInstance(classWithExplicitInterfaceType(index));
        var exception = Assert.Throws<ArgumentNullException>(() => sample.CompareTo(null));
        Assert.AreEqual("other", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] other is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void RequiresNonNullArgument([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType(index));
        var exception = Assert.Throws<ArgumentNullException>(() => sample.SomeMethod(null, ""));
        Assert.AreEqual("nonNullArg", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullArg is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void AllowsNullWhenAttributeApplied([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType(index));
        sample.SomeMethod("", null);
    }

    [Test]
    public void RequiresNonNullMethodReturnValue([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType(index));
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithReturnValue(true));
        Assert.AreEqual("Fail: [NullGuard] Return value of method 'System.String SimpleClass::MethodWithReturnValue(System.Boolean)' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void RequiresNonNullGenericMethodReturnValue([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType(index));
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithGenericReturn<object>(true));
        Assert.AreEqual("Fail: [NullGuard] Return value of method 'T SimpleClass::MethodWithGenericReturn(System.Boolean)' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void AllowsNullReturnValueWhenAttributeApplied([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType(index));
        sample.MethodAllowsNullReturnValue();
    }

    [Test]
    public void RequiresNonNullOutValue([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType(index));
        string value;
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithOutValue(out value));
        Assert.AreEqual("Fail: [NullGuard] Out parameter 'nonNullOutArg' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void AllowsNullOutValue([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType(index));
        string value;
        sample.MethodWithAllowedNullOutValue(out value);
    }

    [Test]
    public void DoesNotRequireNonNullForNonPublicMethod([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType(index));
        sample.PublicWrapperOfPrivateMethod();
    }

    [Test]
    public void DoesNotRequireNonNullForOptionalParameter([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType(index));
        sample.MethodWithOptionalParameter(optional: null);
    }

    [Test]
    public void RequiresNonNullForOptionalParameterWithNonNullDefaultValue([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType(index));
        var exception = Assert.Throws<ArgumentNullException>(() => sample.MethodWithOptionalParameterWithNonNullDefaultValue(optional: null));
        Assert.AreEqual("optional", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] optional is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void DoesNotRequireNonNullForOptionalParameterWithNonNullDefaultValueButAllowNullAttribute([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType(index));
        sample.MethodWithOptionalParameterWithNonNullDefaultValueButAllowNullAttribute(optional: null);
    }

    [Test]
    public void RequiresNonNullForNonPublicMethodWhenAttributeSpecifiesNonPublic([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(classWithPrivateMethodType(index));
        Assert.Throws<ArgumentNullException>(() => sample.PublicWrapperOfPrivateMethod());
        Assert.AreEqual("Fail: [NullGuard] x is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void ReturnGuardDoesNotInterfereWithIteratorMethod([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType(index));
        Assert.That(new[] { 0, 1, 2, 3, 4 }, Is.EquivalentTo(sample.CountTo(5)));
    }

#if (DEBUG)

    [Test]
    public void RequiresNonNullArgumentAsync([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType(index));
        var exception = Assert.Throws<ArgumentNullException>(() => sample.SomeMethodAsync(null, ""));
        Assert.AreEqual("nonNullArg", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullArg is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void AllowsNullWhenAttributeAppliedAsync([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType(index));
        sample.SomeMethodAsync("", null);
    }

    [Test]
    public void RequiresNonNullMethodReturnValueAsync([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType(index));

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
    public void AllowsNullReturnValueWhenAttributeAppliedAsync([Values(0, 1)] int index)
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType(index));

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
    public void NoAwaitWillCompile([Values(0, 1)] int index)
    {
        var instance = (dynamic)Activator.CreateInstance(specialClassType(index));
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
    public void ReturnValueChecksWithBranchToRetInstruction([Values(0, 1)] int index)
    {
        // This is a regression test for the "Branch to RET" issue described in https://github.com/Fody/NullGuard/issues/61.

        var sample = (dynamic)Activator.CreateInstance(sampleClassType(index));
        var exception = Assert.Throws<InvalidOperationException>(() => sample.ReturnValueChecksWithBranchToRetInstruction());
        Assert.AreEqual("[NullGuard] Return value of method 'System.String SimpleClass::ReturnValueChecksWithBranchToRetInstruction()' is null.", exception.Message);
    }

    [Test]
    public void OutValueChecksWithRetInstructionAsSwitchCase([Values(0, 1)] int index)
    {
        // This is a regression test for the "Branch to RET" issue described in https://github.com/Fody/NullGuard/issues/61.

        var sample = (dynamic)Activator.CreateInstance(sampleClassType(index));
        string value;
        var exception = Assert.Throws<InvalidOperationException>(() => sample.OutValueChecksWithRetInstructionAsSwitchCase(0, out value));
        Assert.AreEqual("[NullGuard] Out parameter 'outParam' is null.", exception.Message);
    }
}