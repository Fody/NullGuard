#pragma warning disable xUnit1026 // Theory methods should use all of their parameters
using Xunit;
using System;
using System.Collections.Generic;
using Xunit.Abstractions;

public class ExplicitSpecificTests :
    XunitApprovalBase
{
    public static IEnumerable<object[]> GetFixtureArgs
    {
        get
        {
            yield return new object[] {"InternalBase.DerivedClass", string.Empty};
            yield return new object[] {"InternalBase.ImplementsInterface", string.Empty};
            yield return new object[] {"InternalBase.ImplementsInheritedInterface", string.Empty};
            yield return new object[] {"InternalBase.ImplementsInterfaceExplicit", "InterfaceWithAttributes."};
            yield return new object[] {"AssemblyBase.DerivedClass", string.Empty};
            yield return new object[] {"AssemblyBase.ImplementsInterface", string.Empty};
            yield return new object[] {"AssemblyBase.ImplementsInheritedInterface", string.Empty};
            yield return new object[] {"AssemblyBase.ImplementsInterfaceExplicit", "AssemblyWithAnnotations.InterfaceWithAttributes."};
            yield return new object[] {"ExternalBase.DerivedClass", string.Empty};
            yield return new object[] {"ExternalBase.ImplementsInterface", string.Empty};
            yield return new object[] {"ExternalBase.ImplementsInheritedInterface", string.Empty};
            yield return new object[] {"ExternalBase.ImplementsInterfaceExplicit", "AssemblyWithExternalAnnotations.InterfaceWithAttributes."};
        }
    }

    [Theory]
    [MemberData(nameof(GetFixtureArgs))]
    public void InheritsNullabilityForMethodParameterAndThrowsOnNull(string className, string interfaceName)
    {
        var type = AssemblyWeaver.Assembly.GetType(className);
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() =>
        {
            sample.MethodWithNotNullParameter((string) null, (string) null);
        });
        Assert.Equal("[NullGuard] arg is null.\r\nParameter name: arg", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetFixtureArgs))]
    public void InheritsNullabilityForMethodParameterAndDoesNotThrowOnNotNull(string className, string interfaceName)
    {
        var type = AssemblyWeaver.Assembly.GetType(className);
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.MethodWithNotNullParameter((string)null, "Test");
    }

    [Theory]
    [MemberData(nameof(GetFixtureArgs))]
    public void InheritsNullabilityForMethodReturnAndThrowsOnNull(string className, string interfaceName)
    {
        var type = AssemblyWeaver.Assembly.GetType(className);
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<InvalidOperationException>(() => sample.MethodWithNotNullReturnValue((string)null));
        Assert.Equal($"[NullGuard] Return value of method 'System.String {className}::{interfaceName}MethodWithNotNullReturnValue(System.String)' is null.", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetFixtureArgs))]
    public void InheritsNullabilityForMethodReturnAndDoesNotThrowOnNotNull(string className, string interfaceName)
    {
        var type = AssemblyWeaver.Assembly.GetType(className);
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.MethodWithNotNullReturnValue("Test");
    }

    [Theory]
    [MemberData(nameof(GetFixtureArgs))]
    public void InheritsNullabilityForPropertyAndThrowsOnNullSet(string className, string interfaceName)
    {
        var type = AssemblyWeaver.Assembly.GetType(className);
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.NotNullProperty = (string)null);
        Assert.Equal($"[NullGuard] Cannot set the value of property 'System.String {className}::{interfaceName}NotNullProperty()' to null.\r\nParameter name: value", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetFixtureArgs))]
    public void InheritsNullabilityForPropertyAndDoesNotThrowOnNotNullSet(string className, string interfaceName)
    {
        var type = AssemblyWeaver.Assembly.GetType(className);
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.NotNullProperty = "Test";
    }

    [Theory]
    [MemberData(nameof(GetFixtureArgs))]
    public void InheritsNullabilityForPropertyAndThrowsOnNullGet(string className, string interfaceName)
    {
        var type = AssemblyWeaver.Assembly.GetType(className);
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Assert.Throws<InvalidOperationException>(() => sample.NotNullProperty);
        Assert.Equal($"[NullGuard] Return value of property 'System.String {className}::{interfaceName}NotNullProperty()' is null.", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetFixtureArgs))]
    public void InheritsNullabilityForPropertyAndDoesNotThrowOnNotNullGet(string className, string interfaceName)
    {
        var type = AssemblyWeaver.Assembly.GetType(className);
        var sample = (dynamic) Activator.CreateInstance(type);
        sample.NotNullProperty = "Test";
        string value = sample.NotNullProperty;
    }

    public ExplicitSpecificTests(ITestOutputHelper output) :
        base(output)
    {
    }
}
