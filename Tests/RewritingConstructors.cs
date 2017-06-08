using System;
using System.Reflection;
using Xunit;

public class RewritingConstructors
{
    Type sampleClassType;
    Type classToExcludeType;

    public RewritingConstructors()
    {
        sampleClassType = AssemblyWeaver.Assemblies[0].GetType("SimpleClass");
        classToExcludeType = AssemblyWeaver.Assemblies[1].GetType("ClassToExclude");

        AssemblyWeaver.TestListener.Reset();
    }

    [Fact]
    public void RequiresNonNullArgument()
    {
        var targetInvocationException = Assert.Throws<TargetInvocationException>(() => Activator.CreateInstance(sampleClassType, null, ""));

        var argumentNullException = Assert.IsType<ArgumentNullException>(targetInvocationException.InnerException);
        Assert.Equal("nonNullArg", argumentNullException.ParamName);
    }

    [Fact]
    public void RequiresNonNullOutArgument()
    {
        var args = new object[1];
        var targetInvocationException = Assert.Throws<TargetInvocationException>(() => Activator.CreateInstance(sampleClassType, args));
        var invalidOperationException = Assert.IsType<InvalidOperationException>(targetInvocationException.InnerException);
        Assert.Equal("Fail: [NullGuard] Out parameter 'nonNullOutArg' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Fact]
    public void AllowsNullWhenAttributeApplied()
    {
        Activator.CreateInstance(sampleClassType, "", null);
    }

    [Fact]
    public void AllowsNullWhenClassMatchExcludeRegex()
    {
        Activator.CreateInstance(classToExcludeType, new object[]{ null });
    }
}