using System;
using NUnit.Framework;

[TestFixture]
public class RewritingConstructors
{
    Type sampleClassType;

    public RewritingConstructors()
    {
        sampleClassType = AssemblyWeaver.Assembly.GetType("SimpleClass");
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
        AssemblyWeaver.TestListener.Reset();
        var args = new object[1];
        Assert.That(new TestDelegate(() => Activator.CreateInstance(sampleClassType, args)),
            Throws.TargetInvocationException
                .With.InnerException.TypeOf<InvalidOperationException>());
        Assert.AreEqual("Fail: [NullGuard] Out parameter 'nonNullOutArg' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void AllowsNullWhenAttributeApplied()
    {
        Activator.CreateInstance(sampleClassType, "", null);
    }
}