﻿using System;
using System.Reflection;
#if (NET46)
using ApprovalTests;
#endif
using Xunit;

public class RewritingConstructors
{
    [Fact]
    public void RequiresNonNullArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var exception = Assert.Throws<TargetInvocationException>(() => Activator.CreateInstance(type, null, ""));
#if (NET46)
        Approvals.Verify(exception.InnerException.Message);
#endif
    }

    [Fact]
    public void RequiresNonNullOutArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var args = new object[1];
        var exception = Assert.Throws<TargetInvocationException>(() => Activator.CreateInstance(type, args));
#if (NET46)
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
        Activator.CreateInstance(type, new object[]{ null });
    }

    [Fact]
    public void AllowsNullWhenBaseClassMatchExcludeRegex()
    {
        try
        {
            var type = AssemblyWeaver.Assembly.GetType("ClassToExclude");
            Activator.CreateInstance(type, new object[] { null });
        }
        catch (Exception ex)
        {
            throw new Exception("BaseClass", ex);
        }

        try
        {
            var typeDerived = AssemblyWeaver.Assembly.GetType("ClassToExclude_Derived");
            Activator.CreateInstance(typeDerived, new object[] { null });
        }
        catch (Exception ex)
        {
            throw new Exception("DerivedClass", ex);
        }
    }
}