using System.Collections.Generic;
using TUnit.Assertions.Enums;
using System;
using System.Threading.Tasks;
using TestsCommon;
using VerifyTUnit;

public class RewritingMethods
{
    [Test]
    public async Task RequiresNonNullArgumentForExplicitInterface()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassWithExplicitInterface");
        var sample = (IComparable<string>)Activator.CreateInstance(type);
        var exception = Shared.Throws<ArgumentNullException>(() => sample.CompareTo(null));
        await Verifier.Verify(exception.NormalizedArgumentExceptionMessage());
    }

    [Test]
    public async Task RequiresNonNullArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Shared.Throws<ArgumentNullException>(() =>
        {
            sample.SomeMethod(null, "");
        });
        await Assert.That(exception.ParamName).IsEqualTo("nonNullArg");
        await Verifier.Verify(exception.NormalizedArgumentExceptionMessage());
    }

    [Test]
    public void AllowsNullWhenAttributeApplied()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.SomeMethod("", null);
    }

    [Test]
    public async Task RequiresNonNullMethodReturnValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Shared.Throws<InvalidOperationException>(() => sample.MethodWithReturnValue(true));
        await Verifier.Verify(exception.Message);
    }

    [Test]
    public async Task RequiresNonNullGenericMethodReturnValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Shared.Throws<InvalidOperationException>(() => sample.MethodWithGenericReturn<object>(true));
        await Verifier.Verify(exception.Message);
    }

    [Test]
    public void AllowsNullReturnValueWhenAttributeApplied()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.MethodAllowsNullReturnValue();
    }

    [Test]
    public async Task RequiresNonNullOutValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        string value;
        var exception = Shared.Throws<InvalidOperationException>(() =>
        {
            sample.MethodWithOutValue(out value);
        });
        await Verifier.Verify(exception.Message);
    }

    [Test]
    public void AllowsNullOutValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        string value;
        sample.MethodWithAllowedNullOutValue(out value);
    }

    [Test]
    public void DoesNotRequireNonNullForNonPublicMethod()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.PublicWrapperOfPrivateMethod();
    }

    [Test]
    public void DoesNotRequireNonNullForOptionalParameter()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.MethodWithOptionalParameter(optional: null);
    }

    [Test]
    public async Task RequiresNonNullForOptionalParameterWithNonNullDefaultValue()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Shared.Throws<ArgumentNullException>(() =>
        {
            sample.MethodWithOptionalParameterWithNonNullDefaultValue(optional: null);
        });
        await Verifier.Verify(exception.NormalizedArgumentExceptionMessage());
    }

    [Test]
    public void DoesNotRequireNonNullForOptionalParameterWithNonNullDefaultValueButAllowNullAttribute()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.MethodWithOptionalParameterWithNonNullDefaultValueButAllowNullAttribute(optional: null);
    }

    [Test]
    public async Task RequiresNonNullForNonPublicMethodWhenAttributeSpecifiesNonPublic()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassWithPrivateMethod");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Shared.Throws<ArgumentNullException>(() =>
        {
            sample.PublicWrapperOfPrivateMethod();
        });
        await Verifier.Verify(exception.NormalizedArgumentExceptionMessage());
    }

    [Test]
    public async Task ReturnGuardDoesNotInterfereWithIteratorMethod()
    {
        var type = AssemblyWeaver.Assembly.GetType("SpecialClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        await Assert.That((IEnumerable<int>)sample.CountTo(5)).IsEquivalentTo(new[] {0, 1, 2, 3, 4}, CollectionOrdering.Matching);
    }

#if (DEBUG)

    [Test]
    public async Task RequiresNonNullArgumentAsync()
    {
        var type = AssemblyWeaver.Assembly.GetType("SpecialClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Shared.Throws<ArgumentNullException>(() => sample.SomeMethodAsync(null, ""));
        await Verifier.Verify(exception.NormalizedArgumentExceptionMessage());
    }

    [Test]
    public void AllowsNullWhenAttributeAppliedAsync()
    {
        var type = AssemblyWeaver.Assembly.GetType("SpecialClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.SomeMethodAsync("", null);
    }

    [Test]
    public async Task RequiresNonNullMethodReturnValueAsync()
    {
        var type = AssemblyWeaver.Assembly.GetType("SpecialClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        await Verifier.ThrowsTask(() => sample.MethodWithReturnValueAsync(true))
            .IgnoreStackTrace();
    }

    [Test]
    public async Task AllowsNullReturnValueWhenAttributeAppliedAsync()
    {
        var type = AssemblyWeaver.Assembly.GetType("SpecialClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        return sample.MethodAllowsNullReturnValueAsync();
    }

    [Test]
    public async Task NoAwaitWillCompile()
    {
        var type = AssemblyWeaver.Assembly.GetType("SpecialClass");
        var instance = (dynamic)Activator.CreateInstance(type);
        await Assert.That(instance.NoAwaitCode().Result).IsEqualTo(42);
    }

#endif

    [Test]
    public void AllowsNullWhenClassMatchExcludeRegex()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassToExclude");
        var instance = (dynamic)Activator.CreateInstance(type, "");
        instance.Test(null);
    }

    [Test]
    public async Task ReturnValueChecksWithBranchToRetInstruction()
    {
        // This is a regression test for the "Branch to RET" issue described in https://github.com/Fody/NullGuard/issues/61.
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Shared.Throws<InvalidOperationException>(() => sample.ReturnValueChecksWithBranchToRetInstruction());
        await Assert.That(exception.Message).IsEqualTo("[NullGuard] Return value of method 'System.String SimpleClass::ReturnValueChecksWithBranchToRetInstruction()' is null.");
    }

    [Test]
    public async Task OutValueChecksWithRetInstructionAsSwitchCase()
    {
        // This is a regression test for the "Branch to RET" issue described in https://github.com/Fody/NullGuard/issues/61.
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var sample = (dynamic)Activator.CreateInstance(type);
        string value;
        var exception = Shared.Throws<InvalidOperationException>(() =>
        {
            sample.OutValueChecksWithRetInstructionAsSwitchCase(0, out value);
        });
        await Assert.That(exception.Message).IsEqualTo("[NullGuard] Out parameter 'outParam' is null.");
    }
}