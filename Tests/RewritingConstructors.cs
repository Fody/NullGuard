using System;
using System.Reflection;
using ApprovalTests;
using Xunit;
using Xunit.Abstractions;

public class RewritingConstructors :
    XunitApprovalBase
{
    [Fact]
    public void RequiresNonNullArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var exception = Assert.Throws<TargetInvocationException>(() => Activator.CreateInstance(type, null, ""));
        Approvals.Verify(exception.InnerException.Message);
    }

    [Fact]
    public void RequiresNonNullOutArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var args = new object[1];
        var exception = Assert.Throws<TargetInvocationException>(() => Activator.CreateInstance(type, args));
        Approvals.Verify(exception.InnerException.Message);
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
        Activator.CreateInstance(type, new object[] {null});
    }

    public RewritingConstructors(ITestOutputHelper output) :
        base(output)
    {
    }
}