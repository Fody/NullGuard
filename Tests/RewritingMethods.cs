using System;
using NUnit.Framework;

[TestFixture]
public class RewritingMethods
{
    Type sampleClassType;
    Type classWithPrivateMethodType;
    Type specialClassType;

    public RewritingMethods()
    {
        sampleClassType = AssemblyWeaver.Assembly.GetType("SampleClass");
        classWithPrivateMethodType = AssemblyWeaver.Assembly.GetType("ClassWithPrivateMethod");
        specialClassType = AssemblyWeaver.Assembly.GetType("SpecialClass");
    }

    [Test]
    public void RequiresNonNullArgument()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.SomeMethod(null, ""));
        Assert.AreEqual("nonNullArg", exception.ParamName);
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
        var sample = (dynamic)Activator.CreateInstance(classWithPrivateMethodType);
        Assert.Throws<ArgumentNullException>(() => sample.PublicWrapperOfPrivateMethod());
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
    }

    [Test]
    public void AllowsNullWhenAttributeAppliedAsync()
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType);
        sample.SomeMethodAsync("", null);
    }
#endif
    [Test]
    [Ignore("Not sure how to guard for null in an async method.")]
    public void RequiresNonNullMethodReturnValueAsync()
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType);
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithReturnValueAsync(true));
    }
}