using System;
using NUnit.Framework;

[TestFixture]
public class RewritingMethods
{
    Type sampleClassType;
    Type classWithPrivateMethodType;

    public RewritingMethods()
    {
        sampleClassType = AssemblyWeaver.Assembly.GetType("SampleClass");
        classWithPrivateMethodType = AssemblyWeaver.Assembly.GetType("ClassWithPrivateMethod");
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
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithReturnValue(returnNull: true));
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
}