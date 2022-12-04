using System;
using System.Reflection;
using System.Threading.Tasks;
using TestsCommon;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class RewritingConstructors
{
    [Fact]
    public Task RequiresNonNullArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var exception = Assert.Throws<TargetInvocationException>(() => Activator.CreateInstance(type, null, ""));
        return Verifier.Verify(exception.InnerException.NormalizedArgumentExceptionMessage());
    }

    [Fact]
    public Task RequiresNonNullOutArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var args = new object[1];
        var exception = Assert.Throws<TargetInvocationException>(() => Activator.CreateInstance(type, args));
        return Verifier.Verify(exception.InnerException.Message);
    }

    [Fact]
    public void AllowsNullWhenAttributeApplied()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        Activator.CreateInstance(type, "", null);
    }

    [Fact]
    public void AllowsNullWhenClassMatchExcludeRegex()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassToExclude");
        Activator.CreateInstance(type, new object[]{ null });
    }
}