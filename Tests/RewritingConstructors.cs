using System;
using System.IO;
using NUnit.Framework;

[TestFixture]
public class RewritingConstructors
{
    private AssemblyWeaver assemblyWeaver;
    private Type sampleClassType;
    private Type classWithPrivateMethodType;

    public RewritingConstructors()
    {
        var assemblyPath = Path.GetFullPath(@"..\..\..\AssemblyToProcess\bin\Debug\AssemblyToProcess.dll");
        assemblyWeaver = new AssemblyWeaver(assemblyPath);
        sampleClassType = assemblyWeaver.Assembly.GetType("SampleClass");
        classWithPrivateMethodType = assemblyWeaver.Assembly.GetType("ClassWithPrivateMethod");
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