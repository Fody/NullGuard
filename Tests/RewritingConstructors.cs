using System;
using NUnit.Framework;

[TestFixture]
public class RewritingConstructors
{
    [SetUp]
    public void SetUp()
    {
        AssemblyWeaver.TestListener.Reset();
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.SampleClassTypes))]
    public void RequiresNonNullArgument(Type sampleClassType)
    {
        Assert.That(new TestDelegate(() => Activator.CreateInstance(sampleClassType, null, "")),
            Throws.TargetInvocationException
                .With.InnerException.TypeOf<ArgumentNullException>()
                .And.InnerException.Property("ParamName").EqualTo("nonNullArg"));
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.SampleClassTypes))]
    public void RequiresNonNullOutArgument(Type sampleClassType)
    {
        var args = new object[1];
        Assert.That(new TestDelegate(() => Activator.CreateInstance(sampleClassType, args)),
            Throws.TargetInvocationException
                .With.InnerException.TypeOf<InvalidOperationException>());
        Assert.AreEqual("Fail: [NullGuard] Out parameter 'nonNullOutArg' is null.", AssemblyWeaver.TestListener.Message);
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.SampleClassTypes))]
    public void AllowsNullWhenAttributeApplied(Type sampleClassType)
    {
        Activator.CreateInstance(sampleClassType, "", null);
    }

    [TestCaseSource(typeof(TestCaseHelper), nameof(TestCaseHelper.ClassToExcludeTypes))]
    public void AllowsNullWhenClassMatchExcludeRegex(Type classToExcludeType)
    {
        Activator.CreateInstance(classToExcludeType, new object[]{ null });
    }
}