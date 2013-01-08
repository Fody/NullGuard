﻿using System;
using System.Reflection;
using NUnit.Framework;

[TestFixture]
public class IntegrationTests
{
    [Test]
    public void ErrorsForAbstract()
    {
        Assert.Contains("Method 'System.Void ClassWithBadAttributes::MethodWithNoNullCheckOnParam(System.String)' is abstract but has a [AllowNullAttribute]. Remove this attribute.", AssemblyWeaver.Errors);
        Assert.Contains("Method 'System.Void ClassWithBadAttributes::set_PropertyWithNoNullCheckOnSet(System.String)' is abstract but has a [AllowNullAttribute]. Remove this attribute.", AssemblyWeaver.Errors);
        Assert.Contains("Method 'System.Void InterfaceBadAttributes::MethodWithNoNullCheckOnParam(System.String)' is abstract but has a [AllowNullAttribute]. Remove this attribute.", AssemblyWeaver.Errors);
        Assert.Contains("Method 'System.Void InterfaceBadAttributes::set_PropertyWithNoNullCheckOnSet(System.String)' is abstract but has a [AllowNullAttribute]. Remove this attribute.", AssemblyWeaver.Errors);
    }

    [Test]
    public void MethodWithNullCheckOnParam()
    {
        var type = AssemblyWeaver.Assembly.GetType("TargetClass");
        var instance = (dynamic)Activator.CreateInstance(type);
        instance.MethodWithNullCheckOnParam("notNull");
        Assert.Throws<ArgumentNullException>(() => instance.MethodWithNullCheckOnParam(null));
    }

    [Test]
    public void Constructor()
    {
        var type = AssemblyWeaver.Assembly.GetType("TargetClass");
        Activator.CreateInstance(type, "notNull");

        var targetInvocationException = Assert.Throws<TargetInvocationException>(() => Activator.CreateInstance(type, new object[] { null }));
        var argumentNullException = (ArgumentNullException)targetInvocationException.InnerException;
        Assert.AreEqual("arg", argumentNullException.ParamName);
    }

    [Test]
    public void PropertyWithNullCheckOnSet()
    {
        var type = AssemblyWeaver.Assembly.GetType("TargetClass");
        var instance = (dynamic)Activator.CreateInstance(type);
        instance.PropertyWithNullCheckOnSet = "notNull";
        Assert.Throws<ArgumentNullException>(() => instance.PropertyWithNullCheckOnSet = null);
    }

    [Test]
    public void MethodWithNoNullCheckOnParam()
    {
        var type = AssemblyWeaver.Assembly.GetType("TargetClass");
        var instance = (dynamic)Activator.CreateInstance(type);
        instance.MethodWithNoNullCheckOnParam("notNull");
        instance.MethodWithNoNullCheckOnParam(null);
    }

    [Test]
    public void PropertyWithNoNullCheckOnSet()
    {
        var type = AssemblyWeaver.Assembly.GetType("TargetClass");
        var instance = (dynamic)Activator.CreateInstance(type);
        instance.PropertyWithNoNullCheckOnSet = "notNull";
        instance.PropertyWithNoNullCheckOnSet = null;
    }

#if(DEBUG)

    [Test]
    public void PeVerify()
    {
        Verifier.Verify(AssemblyWeaver.Assembly.CodeBase.Remove(0, 8));
    }

#endif
}