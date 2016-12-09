using System;
using NUnit.Framework;

[TestFixture]
public class RewritingConstructors
{
    private Func<int, Type> sampleClassType;
    Type classToExcludeType;

    [SetUp]
    public void SetUp()
    {
        sampleClassType = index => AssemblyWeaver.RewritingTestAssemblies[index].GetType("SimpleClass");
        classToExcludeType = AssemblyWeaver.Assemblies[1].GetType("ClassToExclude");

        AssemblyWeaver.TestListener.Reset();
    }

    [Test]
    public void RequiresNonNullArgument([Values(0, 1)] int index)
    {
        Assert.That(new TestDelegate(() => Activator.CreateInstance(sampleClassType(index), null, "")),
            Throws.TargetInvocationException
                .With.InnerException.TypeOf<ArgumentNullException>()
                .And.InnerException.Property("ParamName").EqualTo("nonNullArg"));
    }

    [Test]
    public void RequiresNonNullOutArgument([Values(0, 1)] int index)
    {
        var args = new object[1];
        Assert.That(new TestDelegate(() => Activator.CreateInstance(sampleClassType(index), args)),
            Throws.TargetInvocationException
                .With.InnerException.TypeOf<InvalidOperationException>());
        Assert.AreEqual("Fail: [NullGuard] Out parameter 'nonNullOutArg' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void AllowsNullWhenAttributeApplied([Values(0, 1)] int index)
    {
        Activator.CreateInstance(sampleClassType(index), "", null);
    }

    [Test]
    public void AllowsNullWhenClassMatchExcludeRegex()
    {
        Activator.CreateInstance(classToExcludeType, new object[] { null });
    }
}