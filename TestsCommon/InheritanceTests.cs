using System;
using System.Linq;
using Fody;
using Mono.Cecil;

public class InheritanceTests :
    IDisposable
{
    ModuleDefinition module;

    public InheritanceTests()
    {
        var readerParameters = new ReaderParameters
        {
            AssemblyResolver = new TestAssemblyResolver()
        };
        module = ModuleDefinition.ReadModule(typeof(InheritanceTests).Assembly.Location, readerParameters);
    }

    [Test]
    public async Task EnumerateOverridesAndImplementationsFindsExplicitImplementedInterfaceMethods()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(ClassWithExplicitInterfaceImplementation));
        var methods = type.Methods.Where(_ => _.Name.EndsWith(nameof(IComparable.CompareTo))).ToArray();
        await Assert.That(methods.Length).IsEqualTo(2);

        var result = methods.SelectMany(method => method.EnumerateOverridesAndImplementations());
        var expected = "System.Int32 System.IComparable`1::CompareTo(T)|System.Int32 System.IComparable`1::CompareTo(T)";
        var actual = string.Join("|", result);

        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task EnumerateOverridesAndImplementationsFindsImplicitImplementedInterfaceMethods()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(ClassWithImplicitInterfaceImplementation));
        var methods = type.Methods.Where(_ => _.Name.EndsWith(nameof(IComparable.CompareTo))).ToArray();
        await Assert.That(methods.Length).IsEqualTo(2);

        var result = methods.SelectMany(method => method.EnumerateOverridesAndImplementations());
        var expected = "System.Int32 System.IComparable`1::CompareTo(T)|System.Int32 System.IComparable`1::CompareTo(T)";
        var actual = string.Join("|", result);

        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task EnumerateOverridesAndImplementationsFindsCorrectImplementedInterfaceMethodsWhenClassHasBothExplicitAndImplicitImplementations()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(ClassWithExplicitAndImplicitInterfaceImplementation));
        var methods = type.Methods.Where(_ => _.Name.EndsWith(nameof(IComparable.CompareTo))).ToList();
        await Assert.That(methods.Count).IsEqualTo(3);

        var interfaceMethods = methods.SelectMany(method => method.EnumerateOverridesAndImplementations()).ToList();
        await Assert.That(interfaceMethods.Count).IsEqualTo(2);

        var result = methods.SelectMany(method => method.EnumerateOverridesAndImplementations());
        var expected = "System.Int32 System.IComparable`1::CompareTo(T)|System.Int32 System.IComparable`1::CompareTo(T)";
        var actual = string.Join("|", result);

        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task EnumerateOverridesAndImplementationsFindsCorrectMethodOnClassWithMixedGenericInterfaces()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(ClassWithMixedGenericInterfaces));
        var method = type.Methods.Single(_ => _.Name.Equals(nameof(ClassWithMixedGenericInterfaces.Method)) &&
                                              _.ReturnType == module.TypeSystem.Boolean);

        var result = method.EnumerateOverridesAndImplementations();

        var actual = string.Join("|", result);
        var expected = "U3 IGenericDerivedInterface`3::Method(U2)";
        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task EnumerateOverridesAndImplementationsFindsCorrectMethodOnDerivedClassWithMixedGenericInterfacesWhereOriginalImplementationIsOnBaseClass()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(DerivedClassClassWithMixedGenericInterfaces));
        var methods = type.Methods.Where(m => !m.IsSpecialName);

        var result = methods.SelectMany(_ => _.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "System.Int32 BaseClassWithMixedGenericInterfaces::Method(System.Boolean)|T2 IGenericBaseInterface`2::Method(T1)";
        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task EnumerateOverridesAndImplementationsFindsCorrectPropertyOnDerivedClassWithMixedGenericInterfacesWhereOriginalImplementationIsOnBaseClass()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(DerivedClassClassWithMixedGenericInterfaces));
        var methods = type.Properties;

        var result = methods.SelectMany(_ => _.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "System.Boolean BaseClassWithMixedGenericInterfaces::Property()";
        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task EnumerateOverridesAndImplementationsFindsCorrectMethodFromBaseInterfaceOnClassWithMixedGenericInterfaces()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(ClassWithMixedGenericInterfaces));
        var method = type.Methods.Single(_ => _.Name.Equals(nameof(ClassWithMixedGenericInterfaces.Method)) &&
                                              _.ReturnType == module.TypeSystem.Int32);

        var result = method.EnumerateOverridesAndImplementations();

        var actual = string.Join("|", result);
        var expected = "T2 IGenericBaseInterface`2::Method(T1)";
        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task EnumerateOverridesAndImplementationsFindsCorrectMethodOnClassWithMixedGenericInterfaces2()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(ClassWithMixedGenericInterfaces2));
        var method = type.Methods.Single(_ => _.Name.Equals(nameof(ClassWithMixedGenericInterfaces2.Method)) &&
                                              _.Parameters[0].ParameterType == module.TypeSystem.Int32);

        var result = method.EnumerateOverridesAndImplementations();

        var actual = string.Join("|", result);
        var expected = "U3 IGenericDerivedInterface2`3::Method(U2)";
        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task EnumerateOverridesAndImplementationsFindsCorrectMethodFromBaseInterfaceOnClassWithMixedGenericInterfaces2()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(ClassWithMixedGenericInterfaces2));
        var method = type.Methods.Single(_ => _.Name.Equals(nameof(ClassWithMixedGenericInterfaces2.Method)) &&
                                              _.Parameters[0].ParameterType == module.TypeSystem.String);

        var result = method.EnumerateOverridesAndImplementations();

        var actual = string.Join("|", result);
        var expected = "T2 IGenericBaseInterface`2::Method(T1)";
        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task EnumerateOverridesAndImplementationsFindsNoMethodFromBaseInterfaceOnClassWithMixedGenericInterfacesWhenExplicitImplementationExists()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(ClassWithMixedGenericInterfaces3));
        var method = type.Methods.Single(_ => _.Name.Equals(nameof(ClassWithMixedGenericInterfaces3.Method)) &&
                                              _.Parameters[0].ParameterType == module.TypeSystem.String);

        var result = method.EnumerateOverridesAndImplementations();
        await Assert.That(result).IsEmpty();
    }

    [Test]
    public async Task EnumerateOverridesAndImplementationsFindsNoPropertyFromBaseInterfaceOnClassWithMixedGenericInterfacesWhenExplicitImplementationExists()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(ClassWithMixedGenericInterfaces3));
        var property = type.Properties.Single(_ => _.Name.Equals(nameof(ClassWithMixedGenericInterfaces3.Property)));

        var result = property.EnumerateOverridesAndImplementations();
        await Assert.That(result).IsEmpty();
    }

    [Test]
    public async Task EnumerateOverridesAndImplementationsFindsBaseMembersForMethodOverrides1()
    {
        var type = module.GetTypes().Single(t => t.Name.Contains(nameof(DerivedGenericClass1<string>)));
        var values = type.Methods.Where(m => !m.IsSpecialName);

        var result = values.SelectMany(item => item.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "T2 GenericBaseClass`2::Method(T1)";
        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task EnumerateOverridesAndImplementationsFindsBaseMembersForMethodOverrides2()
    {
        var type = module.GetTypes().Single(t => t.Name.Contains(nameof(DerivedGenericClass2<string>)));
        var values = type.Methods.Where(m => !m.IsSpecialName);

        var result = values.SelectMany(item => item.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "T1 GenericBaseClass`2::Method(T2)";
        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task EnumerateOverridesAndImplementationsFindsBaseMembersForMethodOverridesInDerivedDerived()
    {
        var type = module.GetTypes().Single(t => t.Name.Contains(nameof(DerivedDerivedClass)));
        var values = type.Methods.Where(m => !m.IsSpecialName);

        var result = values.SelectMany(item => item.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "T1 GenericBaseClass`2::Method(T2)";
        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task EnumerateOverridesAndImplementationsFindsBaseMembersForPropertyOverrides()
    {
        var type = module.GetTypes().Single(t => t.Name.Contains(nameof(DerivedGenericClass2<string>)));
        var values = type.Properties;

        var result = values.SelectMany(item => item.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "T1 GenericBaseClass`2::Property()";
        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task EnumerateOverridesAndImplementationsFindsBaseMembersForPropertyOverridesInDerivedDerived()
    {
        var type = module.GetTypes().Single(t => t.Name.Contains(nameof(DerivedDerivedClass)));
        var values = type.Properties;

        var result = values.SelectMany(item => item.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "T1 GenericBaseClass`2::Property()";
        await Assert.That(actual).IsEqualTo(expected);
    }

    public void Dispose()
    {
        module.Dispose();
    }
}