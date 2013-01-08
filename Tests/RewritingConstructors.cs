using System;
using NUnit.Framework;

[TestFixture]
public class RewritingConstructors
{
    Type sampleClassType;
    Type classWithPrivateMethodType;

    public RewritingConstructors()
    {
        sampleClassType = AssemblyWeaver.Assembly.GetType("SampleClass");
        classWithPrivateMethodType = AssemblyWeaver.Assembly.GetType("ClassWithPrivateMethod");
    }

    [Test]
    public void RequiresNonNullArgument()
    {
        Assert.That(new TestDelegate(() => Activator.CreateInstance(sampleClassType, null, "")),
            Throws.TargetInvocationException
                .With.InnerException.TypeOf<ArgumentNullException>()
                .And.InnerException.Property("ParamName").EqualTo("nonNullArg"));
    }

    [Test]
    public void RequiresNonNullOutArgument()
    {
        var args = new object[1];
        Assert.That(new TestDelegate(() => Activator.CreateInstance(sampleClassType, args)),
            Throws.TargetInvocationException
                .With.InnerException.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void AllowsNullWhenAttributeApplied()
    {
        Activator.CreateInstance(sampleClassType, "", null);
    }
}