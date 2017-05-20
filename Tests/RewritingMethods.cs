using System;
using System.Collections.Generic;
#if (DEBUG)
using System.Threading.Tasks;
#endif

using NUnit.Framework;

[TestFixture]
public class RewritingMethods
{
    [SetUp]
    public void SetUp()
    {
        AssemblyWeaver.TestListener.Reset();
    }

    private static IEnumerable<TestCaseData> ClassWithExplicitInterfaceTypes => TestCaseHelper.GetWovenTypes("ClassWithExplicitInterface");

    private static IEnumerable<TestCaseData> SampleClassTypes => TestCaseHelper.GetWovenTypes("SimpleClass");

    private static IEnumerable<TestCaseData> SpecialClassTypes => TestCaseHelper.GetWovenTypes("SpecialClass");

    private static IEnumerable<TestCaseData> ClassWithPrivateMethodTypes => TestCaseHelper.GetWovenTypes("ClassWithPrivateMethod");

    private static IEnumerable<TestCaseData> ClassToExcludeTypes => TestCaseHelper.GetTypesWovenWithConfig("ClassToExclude");

    [TestCaseSource(nameof(ClassWithExplicitInterfaceTypes))]
    public void RequiresNonNullArgumentForExplicitInterface(Type classWithExplicitInterfaceType)
    {
        AssemblyWeaver.TestListener.Reset();
        var sample = (IComparable<string>)Activator.CreateInstance(classWithExplicitInterfaceType);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.CompareTo(null));
        Assert.AreEqual("other", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] other is null.", AssemblyWeaver.TestListener.Message);
    }

    [TestCaseSource(nameof(SampleClassTypes))]
    public void RequiresNonNullArgument(Type sampleClassType)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.SomeMethod(null, ""));
        Assert.AreEqual("nonNullArg", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullArg is null.", AssemblyWeaver.TestListener.Message);
    }

    [TestCaseSource(nameof(SampleClassTypes))]
    public void AllowsNullWhenAttributeApplied(Type sampleClassType)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.SomeMethod("", null);
    }

    [TestCaseSource(nameof(SampleClassTypes))]
    public void RequiresNonNullMethodReturnValue(Type sampleClassType)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithReturnValue(true));
        Assert.AreEqual("Fail: [NullGuard] Return value of method 'System.String SimpleClass::MethodWithReturnValue(System.Boolean)' is null.", AssemblyWeaver.TestListener.Message);
    }

    [TestCaseSource(nameof(SampleClassTypes))]
    public void RequiresNonNullGenericMethodReturnValue(Type sampleClassType)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithGenericReturn<object>(true));
        Assert.AreEqual("Fail: [NullGuard] Return value of method 'T SimpleClass::MethodWithGenericReturn(System.Boolean)' is null.", AssemblyWeaver.TestListener.Message);
    }

    [TestCaseSource(nameof(SampleClassTypes))]
    public void AllowsNullReturnValueWhenAttributeApplied(Type sampleClassType)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.MethodAllowsNullReturnValue();
    }

    [TestCaseSource(nameof(SampleClassTypes))]
    public void RequiresNonNullOutValue(Type sampleClassType)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        string value;
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithOutValue(out value));
        Assert.AreEqual("Fail: [NullGuard] Out parameter 'nonNullOutArg' is null.", AssemblyWeaver.TestListener.Message);
    }

    [TestCaseSource(nameof(SampleClassTypes))]
    public void AllowsNullOutValue(Type sampleClassType)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        string value;
        sample.MethodWithAllowedNullOutValue(out value);
    }

    [TestCaseSource(nameof(SampleClassTypes))]
    public void DoesNotRequireNonNullForNonPublicMethod(Type sampleClassType)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.PublicWrapperOfPrivateMethod();
    }

    [TestCaseSource(nameof(SampleClassTypes))]
    public void DoesNotRequireNonNullForOptionalParameter(Type sampleClassType)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.MethodWithOptionalParameter(optional: null);
    }

    [TestCaseSource(nameof(SampleClassTypes))]
    public void RequiresNonNullForOptionalParameterWithNonNullDefaultValue(Type sampleClassType)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.MethodWithOptionalParameterWithNonNullDefaultValue(optional: null));
        Assert.AreEqual("optional", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] optional is null.", AssemblyWeaver.TestListener.Message);
    }

    [TestCaseSource(nameof(SampleClassTypes))]
    public void DoesNotRequireNonNullForOptionalParameterWithNonNullDefaultValueButAllowNullAttribute(Type sampleClassType)
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.MethodWithOptionalParameterWithNonNullDefaultValueButAllowNullAttribute(optional: null);
    }

    [TestCaseSource(nameof(ClassWithPrivateMethodTypes)), Explicit("Fails on AppVeyor - TODO")]
    public void RequiresNonNullForNonPublicMethodWhenAttributeSpecifiesNonPublic(Type classWithPrivateMethodType)
    {
        var sample = (dynamic)Activator.CreateInstance(classWithPrivateMethodType);
        Assert.Throws<ArgumentNullException>(() => sample.PublicWrapperOfPrivateMethod());
        Assert.AreEqual("Fail: [NullGuard] x is null.", AssemblyWeaver.TestListener.Message);
    }

    [TestCaseSource(nameof(SpecialClassTypes))]
    public void ReturnGuardDoesNotInterfereWithIteratorMethod(Type specialClassType)
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType);
        Assert.That(new[] { 0, 1, 2, 3, 4 }, Is.EquivalentTo(sample.CountTo(5)));
    }

#if (DEBUG)
    
    [TestCaseSource(nameof(SpecialClassTypes))]
    public void RequiresNonNullArgumentAsync(Type specialClassType)
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.SomeMethodAsync(null, ""));
        Assert.AreEqual("nonNullArg", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullArg is null.", AssemblyWeaver.TestListener.Message);
    }

    [TestCaseSource(nameof(SpecialClassTypes))]
    public void AllowsNullWhenAttributeAppliedAsync(Type specialClassType)
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType);
        sample.SomeMethodAsync("", null);
    }

    [TestCaseSource(nameof(SpecialClassTypes))]
    public void RequiresNonNullMethodReturnValueAsync(Type specialClassType)
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

    [TestCaseSource(nameof(SpecialClassTypes))]
    public void AllowsNullReturnValueWhenAttributeAppliedAsync(Type specialClassType)
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

    [TestCaseSource(nameof(SpecialClassTypes))]
    public void NoAwaitWillCompile(Type specialClassType)
    {
        var instance = (dynamic)Activator.CreateInstance(specialClassType);
        Assert.AreEqual(42, instance.NoAwaitCode().Result);
    }

#endif

    [TestCaseSource(nameof(ClassToExcludeTypes))]
    public void AllowsNullWhenClassMatchExcludeRegex(Type classToExcludeType)
    {
        var ClassToExclude = (dynamic)Activator.CreateInstance(classToExcludeType, "");
        ClassToExclude.Test(null);
    }

    [TestCaseSource(nameof(SampleClassTypes))]
    public void ReturnValueChecksWithBranchToRetInstruction(Type sampleClassType)
    {
        // This is a regression test for the "Branch to RET" issue described in https://github.com/Fody/NullGuard/issues/61.

        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        var exception = Assert.Throws<InvalidOperationException>(() => sample.ReturnValueChecksWithBranchToRetInstruction());
        Assert.AreEqual("[NullGuard] Return value of method 'System.String SimpleClass::ReturnValueChecksWithBranchToRetInstruction()' is null.", exception.Message);
    }

    [TestCaseSource(nameof(SampleClassTypes))]
    public void OutValueChecksWithRetInstructionAsSwitchCase(Type sampleClassType)
    {
        // This is a regression test for the "Branch to RET" issue described in https://github.com/Fody/NullGuard/issues/61.

        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        string value;
        var exception = Assert.Throws<InvalidOperationException>(() => sample.OutValueChecksWithRetInstructionAsSwitchCase(0, out value));
        Assert.AreEqual("[NullGuard] Out parameter 'outParam' is null.", exception.Message);
    }
}