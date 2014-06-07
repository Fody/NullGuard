using System;
using System.Reflection;
using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class RewritingMethods
{
    private Type sampleClassType;
    private Type classWithPrivateMethodType;
    private Type specialClassType;
    private Type nestedClassesType;
    private Type interfaceImplementationsType;
    private Type classToExcludeType;

    [SetUp]
    public void SetUp()
    {
        sampleClassType = AssemblyWeaver.Assemblies[0].GetType("SimpleClass");
        classWithPrivateMethodType = AssemblyWeaver.Assemblies[0].GetType("ClassWithPrivateMethod");
        specialClassType = AssemblyWeaver.Assemblies[0].GetType("SpecialClass");
        nestedClassesType = AssemblyWeaver.Assemblies[0].GetType("NestedClasses");
        interfaceImplementationsType = AssemblyWeaver.Assemblies[0].GetType("InterfaceImplementations");
        classToExcludeType = AssemblyWeaver.Assemblies[1].GetType("ClassToExclude");

        AssemblyWeaver.TestListener.Reset();
    }

    [Test]
    public void RequiresNonNullArgument()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.SomeMethod(null, ""));
        Assert.AreEqual("nonNullArg", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullArg is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void AllowsNullWhenAttributeApplied()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.SomeMethod("", null);
    }

    [Test]
    public void RequiresNonNullMethodReturnValue()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithReturnValue(true));
        Assert.AreEqual("Fail: [NullGuard] Return value of method 'System.String SimpleClass::MethodWithReturnValue(System.Boolean)' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void RequiresNonNullGenericMethodReturnValue()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithGenericReturn<object>(true));
        Assert.AreEqual("Fail: [NullGuard] Return value of method 'T SimpleClass::MethodWithGenericReturn(System.Boolean)' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void AllowsNullReturnValueWhenAttributeApplied()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.MethodAllowsNullReturnValue();
    }

    [Test]
    public void RequiresNonNullOutValue()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        string value;
        Assert.Throws<InvalidOperationException>(() => sample.MethodWithOutValue(out value));
        Assert.AreEqual("Fail: [NullGuard] Out parameter 'nonNullOutArg' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void DoesNotRequireNonNullForNonPublicMethod()
    {
        var sample = Activator.CreateInstance(sampleClassType);
        sampleClassType.InvokeNonPublicMethod(sample, "SomePrivateMethod", new object[] { null });
    }

    [Test]
    public void DoesNotRequireNonNullForOptionalParameter()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.MethodWithOptionalParameter(optional: null);
    }

    [Test]
    public void RequiresNonNullForOptionalParameterWithNonNullDefaultValue()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.MethodWithOptionalParameterWithNonNullDefaultValue(optional: null));
        Assert.AreEqual("optional", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] optional is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void DoesNotRequireNonNullForOptionalParameterWithNonNullDefaultValueButAllowNullAttribute()
    {
        var sample = (dynamic)Activator.CreateInstance(sampleClassType);
        sample.MethodWithOptionalParameterWithNonNullDefaultValueButAllowNullAttribute(optional: null);
    }

    [Test]
    public void RequiresNonNullForNonPublicMethodWhenAttributeSpecifiesNonPublic()
    {
        var sample = Activator.CreateInstance(classWithPrivateMethodType);
        var exception = Assert.Throws<ArgumentNullException>(
            () => classWithPrivateMethodType.InvokeNonPublicMethod(sample, "SomePrivateMethod", new object[] { null }));
        Assert.AreEqual("x", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] x is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void ReturnGuardDoesNotInterfereWithIteratorMethod()
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType);
        Assert.That(new[] { 0, 1, 2, 3, 4 }, Is.EquivalentTo(sample.CountTo(5)));
    }

#if (DEBUG)

    [Test]
    public void RequiresNonNullArgumentAsync()
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType);
        var exception = Assert.Throws<ArgumentNullException>(() => sample.SomeMethodAsync(null, ""));
        Assert.AreEqual("nonNullArg", exception.ParamName);
        Assert.AreEqual("Fail: [NullGuard] nonNullArg is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void AllowsNullWhenAttributeAppliedAsync()
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType);
        sample.SomeMethodAsync("", null);
    }

    [Test]
    public void RequiresNonNullMethodReturnValueAsync()
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType);

        Exception ex = null;

        ((Task<string>)sample.MethodWithReturnValueAsync(true))
            .ContinueWith(t =>
            {
                if (t.IsFaulted)
                    ex = t.Exception.Flatten().InnerException;
            })
            .Wait();

        Assert.NotNull(ex);
        Assert.IsInstanceOf<InvalidOperationException>(ex);
        Assert.AreEqual("[NullGuard] Return value of method 'System.Threading.Tasks.Task`1<System.String> SpecialClass::MethodWithReturnValueAsync(System.Boolean)' is null.", ex.Message);
        Assert.AreEqual("Fail: [NullGuard] Return value of method 'System.Threading.Tasks.Task`1<System.String> SpecialClass::MethodWithReturnValueAsync(System.Boolean)' is null.", AssemblyWeaver.TestListener.Message);
    }

    [Test]
    public void AllowsNullReturnValueWhenAttributeAppliedAsync()
    {
        var sample = (dynamic)Activator.CreateInstance(specialClassType);

        Exception ex = null;

        ((Task<string>)sample.MethodAllowsNullReturnValueAsync())
            .ContinueWith(t =>
            {
                if (t.IsFaulted)
                    ex = t.Exception.Flatten().InnerException;
            })
            .Wait();

        Assert.Null(ex);
    }

    [Test]
    public void NoAwaitWillCompile()
    {
        var instance = (dynamic)Activator.CreateInstance(specialClassType);
        Assert.AreEqual(42, instance.NoAwaitCode().Result);
    }

#endif

    [Test]
    public void NotNullMethodInNestedClass()
    {
        var nestedType = nestedClassesType.GetNestedType("OuterNestedClass").GetNestedType("InnerNestedClass");
        var instance = (dynamic)Activator.CreateInstance(nestedType);
        var exception = Assert.Throws<ArgumentNullException>(() => instance.SomeMethod(null));
        Assert.AreEqual("notNull", exception.ParamName);
    }

    [Test]
    public void AllowNullMethodInNestedClass()
    {
        var nestedType = nestedClassesType.GetNestedType("OuterNestedClass").GetNestedType("InnerNestedClass");
        var instance = (dynamic)Activator.CreateInstance(nestedType);
        instance.AllowNullArgMethod(null);
    }

    [Test]
    public void InternalMethodInNestedClass()
    {
        var nestedType = nestedClassesType.GetNestedType("OuterNestedClass").GetNestedType("InnerNestedClass");
        var instance = Activator.CreateInstance(nestedType);
        nestedType.InvokeNonPublicMethod(instance, "SomeInternalMethod", new object[] { null });
    }

    [Test]
    public void NotNullMethodInExplicitInterfaceImplementation()
    {
        var nestedType = interfaceImplementationsType.GetNestedType("ExplicitImplementation", BindingFlags.NonPublic);
        var instance = Activator.CreateInstance(nestedType);
        var exception = Assert.Throws<ArgumentNullException>(
            () => nestedType.GetInterface("ISomeInterface").InvokePublicMethod(instance, "SomeInterfaceMethod", new object[] { null }));
        Assert.AreEqual("notNull", exception.ParamName);
    }

    [Test]
    public void NotNullMethodInImplicitInterfaceImplementation()
    {
        var nestedType = interfaceImplementationsType.GetNestedType("ImplicitImplementation", BindingFlags.NonPublic);
        var instance = Activator.CreateInstance(nestedType);
        var exception = Assert.Throws<ArgumentNullException>(
            () => nestedType.GetInterface("ISomeInterface").InvokePublicMethod(instance, "SomeInterfaceMethod", new object[] { null }));
        Assert.AreEqual("notNull", exception.ParamName);
    }

    [Test]
    public void AllowsNullWhenClassMatchExcludeRegex()
    {
        var ClassToExclude = (dynamic)Activator.CreateInstance(classToExcludeType, "");
        ClassToExclude.Test(null);
    }
}