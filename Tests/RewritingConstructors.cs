using System;
using NUnit.Framework;

[TestFixture]
public class RewritingConstructors
{
    private Type sampleClassType;
    Type classToExcludeType;

    [SetUp]
    public void SetUp()
    {
        sampleClassType = AssemblyWeaver.Assemblies[0].GetType("SimpleClass");
        classToExcludeType = AssemblyWeaver.Assemblies[1].GetType("ClassToExclude");

        AssemblyWeaver.TestListener.Reset();
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
        Assert.AreEqual("Fail: [NullGuard] Out parameter 'nonNullOutArg' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void AllowsNullWhenAttributeApplied()
    {
        Activator.CreateInstance(sampleClassType, "", null);
    }

    [Test]
    public void AllowsNullWhenClassMatchExcludeRegex()
    {
        Activator.CreateInstance(this.classToExcludeType, new object[]{ null });
    }
}