using System;
using System.Reflection;
using ApprovalTests;
using NUnit.Framework;

[TestFixture]
public class RewritingConstructors
{
    [Test]
    public void RequiresNonNullArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var exception = Assert.Throws<TargetInvocationException>(() => Activator.CreateInstance(type, null, ""));
        Approvals.Verify(exception.InnerException.Message);
    }

    [Test]
    public void RequiresNonNullOutArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var args = new object[1];
        var exception = Assert.Throws<TargetInvocationException>(() => Activator.CreateInstance(type, args));
        Approvals.Verify(exception.InnerException.Message);
      //  Assert.AreEqual("Fail: [NullGuard] Out parameter 'nonNullOutArg' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void AllowsNullWhenAttributeApplied()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        Activator.CreateInstance(type, "", null);
    }

    [Test]
    public void AllowsNullWhenClassMatchExcludeRegex()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassToExclude");
        Activator.CreateInstance(type, new object[]{ null });
    }
}