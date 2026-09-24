using System;
using System.Collections.Generic;
using TestsCommon;

public class ExplicitSpecificTests
{
    public static IEnumerable<(string, string)> GetFixtureArgs()
    {
        yield return ("InternalBase.DerivedClass", string.Empty);
        yield return ("InternalBase.ImplementsInterface", string.Empty);
        yield return ("InternalBase.ImplementsInheritedInterface", string.Empty);
        yield return ("InternalBase.ImplementsInterfaceExplicit", "InterfaceWithAttributes.");
        yield return ("AssemblyBase.DerivedClass", string.Empty);
        yield return ("AssemblyBase.ImplementsInterface", string.Empty);
        yield return ("AssemblyBase.ImplementsInheritedInterface", string.Empty);
        yield return ("AssemblyBase.ImplementsInterfaceExplicit", "AssemblyWithAnnotations.InterfaceWithAttributes.");
        yield return ("ExternalBase.DerivedClass", string.Empty);
        yield return ("ExternalBase.ImplementsInterface", string.Empty);
        yield return ("ExternalBase.ImplementsInheritedInterface", string.Empty);
        yield return ("ExternalBase.ImplementsInterfaceExplicit", "AssemblyWithExternalAnnotations.InterfaceWithAttributes.");
    }

    [Test]
    [MethodDataSource(nameof(GetFixtureArgs))]
    public async Task InheritsNullabilityForMethodParameterAndThrowsOnNull(string className, string interfaceName)
    {
        var type = AssemblyWeaver.Assembly.GetType(className);
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Shared.Throws<ArgumentNullException>(() =>
        {
            sample.MethodWithNotNullParameter((string) null, (string) null);
        });
        await Assert.That(exception.NormalizedArgumentExceptionMessage()).IsEqualTo("[NullGuard] arg is null.\r\nParameter name: arg");
    }

    [Test]
    [MethodDataSource(nameof(GetFixtureArgs))]
    public void InheritsNullabilityForMethodParameterAndDoesNotThrowOnNotNull(string className, string interfaceName)
    {
        var type = AssemblyWeaver.Assembly.GetType(className);
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.MethodWithNotNullParameter((string)null, "Test");
    }

    [Test]
    [MethodDataSource(nameof(GetFixtureArgs))]
    public async Task InheritsNullabilityForMethodReturnAndThrowsOnNull(string className, string interfaceName)
    {
        var type = AssemblyWeaver.Assembly.GetType(className);
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Shared.Throws<InvalidOperationException>(() => sample.MethodWithNotNullReturnValue((string)null));
        await Assert.That(exception.Message).IsEqualTo($"[NullGuard] Return value of method 'System.String {className}::{interfaceName}MethodWithNotNullReturnValue(System.String)' is null.");
    }

    [Test]
    [MethodDataSource(nameof(GetFixtureArgs))]
    public void InheritsNullabilityForMethodReturnAndDoesNotThrowOnNotNull(string className, string interfaceName)
    {
        var type = AssemblyWeaver.Assembly.GetType(className);
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.MethodWithNotNullReturnValue("Test");
    }

    [Test]
    [MethodDataSource(nameof(GetFixtureArgs))]
    public async Task InheritsNullabilityForPropertyAndThrowsOnNullSet(string className, string interfaceName)
    {
        var type = AssemblyWeaver.Assembly.GetType(className);
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Shared.Throws<ArgumentNullException>(() => sample.NotNullProperty = (string)null);
        await Assert.That(exception.NormalizedArgumentExceptionMessage()).IsEqualTo($"[NullGuard] Cannot set the value of property 'System.String {className}::{interfaceName}NotNullProperty()' to null.\r\nParameter name: value");
    }

    [Test]
    [MethodDataSource(nameof(GetFixtureArgs))]
    public void InheritsNullabilityForPropertyAndDoesNotThrowOnNotNullSet(string className, string interfaceName)
    {
        var type = AssemblyWeaver.Assembly.GetType(className);
        var sample = (dynamic)Activator.CreateInstance(type);
        sample.NotNullProperty = "Test";
    }

    [Test]
    [MethodDataSource(nameof(GetFixtureArgs))]
    public async Task InheritsNullabilityForPropertyAndThrowsOnNullGet(string className, string interfaceName)
    {
        var type = AssemblyWeaver.Assembly.GetType(className);
        var sample = (dynamic)Activator.CreateInstance(type);
        var exception = Shared.Throws<InvalidOperationException>(() => sample.NotNullProperty);
        await Assert.That(exception.Message).IsEqualTo($"[NullGuard] Return value of property 'System.String {className}::{interfaceName}NotNullProperty()' is null.");
    }

    [Test]
    [MethodDataSource(nameof(GetFixtureArgs))]
    public void InheritsNullabilityForPropertyAndDoesNotThrowOnNotNullGet(string className, string interfaceName)
    {
        var type = AssemblyWeaver.Assembly.GetType(className);
        var sample = (dynamic) Activator.CreateInstance(type);
        sample.NotNullProperty = "Test";
        string value = sample.NotNullProperty;
    }
}