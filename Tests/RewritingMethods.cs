using System;
using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class RewritingMethods
{
    private Type sampleClassType;
    private Type classWithPrivateMethodType;
    private Type specialClassType;

    public RewritingMethods()
    {
        sampleClassType = AssemblyWeaver.Assembly.GetType("SimpleClass");
        classWithPrivateMethodType = AssemblyWeaver.Assembly.GetType("ClassWithPrivateMethod");
        specialClassType = AssemblyWeaver.Assembly.GetType("SpecialClass");
    }

    [Test]
    public void RequiresNonNullArgument()
    {
        AssemblyWeaver.TestListener.Reset();
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
        AssemblyWeaver.TestListener.Reset();
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithReturnValue(true));
        Assert.AreEqual("Fail: [NullGuard] Return value of method 'MethodWithReturnValue' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void RequiresNonNullGenericMethodReturnValue()
    {
        AssemblyWeaver.TestListener.Reset();
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithGenericReturn<object>(true));
        Assert.AreEqual("Fail: [NullGuard] Return value of method 'MethodWithGenericReturn' is null.", AssemblyWeaver.TestListener.Message);
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
        AssemblyWeaver.TestListener.Reset();
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        string value;
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithOutValue(out value));
        Assert.AreEqual("Fail: [NullGuard] Out parameter 'nonNullOutArg' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void DoesNotRequireNonNullForNonPublicMethod()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.PublicWrapperOfPrivateMethod();
    }

    [Test]
    public void RequiresNonNullForNonPublicMethodWhenAttributeSpecifiesNonPublic()
    {
        AssemblyWeaver.TestListener.Reset();
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
        AssemblyWeaver.TestListener.Reset();
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
        AssemblyWeaver.TestListener.Reset();
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
        Assert.AreEqual("[NullGuard] Return value of method 'MethodWithReturnValueAsync' is null.", ex.Message);
        Assert.AreEqual("Fail: [NullGuard] Return value of method 'MethodWithReturnValueAsync' is null.", AssemblyWeaver.TestListener.Message);
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

#endif
}