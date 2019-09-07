using System;
using System.Reflection;
#if (NET472)
using ApprovalTests;
#endif
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
#if (NET472)
        Approvals.Verify(exception.InnerException.Message);
#endif
    }

    [Fact]
    public void RequiresNonNullOutArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var args = new object[1];
        var exception = Assert.Throws<TargetInvocationException>(() => Activator.CreateInstance(type, args));
#if (NET472)
        Approvals.Verify(exception.InnerException.Message);
#endif
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