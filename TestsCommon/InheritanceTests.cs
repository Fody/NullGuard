using System;
using System.Linq;
using Fody;
using Mono.Cecil;
using Xunit;
#pragma warning disable 618

public class InheritanceTests
{
    ModuleDefinition module;

    public InheritanceTests()
    {
        module = ModuleDefinition.ReadModule(
            fileName: typeof(InheritanceTests).Assembly.Location,
            parameters: new ReaderParameters
            {
                AssemblyResolver = new MockAssemblyResolver()
            });
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsExplicitImplementedInterfaceMethods()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(ClassWithExplicitInterfaceImplementation));
        var methods = type.Methods.Where(m => m.Name.EndsWith(nameof(IComparable.CompareTo))).ToArray();
        Assert.Equal(2, methods.Length);

        var result = methods.SelectMany(method => method.EnumerateOverridesAndImplementations());
        var expected = "System.Int32 System.IComparable`1::CompareTo(T)|System.Int32 System.IComparable`1::CompareTo(T)";
        var actual = string.Join("|", result);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsImplicitImplementedInterfaceMethods()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(ClassWithImplicitInterfaceImplementation));
        var methods = type.Methods.Where(m => m.Name.EndsWith(nameof(IComparable.CompareTo))).ToArray();
        Assert.Equal(2, methods.Length);

        var result = methods.SelectMany(method => method.EnumerateOverridesAndImplementations());
        var expected = "System.Int32 System.IComparable`1::CompareTo(T)|System.Int32 System.IComparable`1::CompareTo(T)";
        var actual = string.Join("|", result);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsCorrectImplementedInterfaceMethodsWhenClassHasBothExplicitAndImplicitImplementations()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(ClassWithExplicitAndImplicitInterfaceImplementation));
        var methods = type.Methods.Where(m => m.Name.EndsWith(nameof(IComparable.CompareTo))).ToList();
        Assert.Equal(3, methods.Count);

        var interfaceMethods = methods.SelectMany(method => method.EnumerateOverridesAndImplementations()).ToList();
        Assert.Equal(2, interfaceMethods.Count);

        var result = methods.SelectMany(method => method.EnumerateOverridesAndImplementations());
        var expected = "System.Int32 System.IComparable`1::CompareTo(T)|System.Int32 System.IComparable`1::CompareTo(T)";
        var actual = string.Join("|", result);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsCorrectMethodOnClassWithMixedGenericInterfaces()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(ClassWithMixedGenericInterfaces));
        var method = type.Methods.Single(m => m.Name.Equals(nameof(ClassWithMixedGenericInterfaces.Method)) && m.ReturnType == module.TypeSystem.Boolean);

        var result = method.EnumerateOverridesAndImplementations();

        var actual = string.Join("|", result);
        var expected = "U3 IGenericDerivedInterface`3::Method(U2)";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsCorrectMethodOnDerivedClassWithMixedGenericInterfacesWhereOriginalImplementationIsOnBaseClass()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(DerivedClassClassWithMixedGenericInterfaces));
        var methods = type.Methods.Where(m => !m.IsSpecialName);

        var result = methods.SelectMany(m => m.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "System.Int32 BaseClassWithMixedGenericInterfaces::Method(System.Boolean)|T2 IGenericBaseInterface`2::Method(T1)";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsCorrectPropertyOnDerivedClassWithMixedGenericInterfacesWhereOriginalImplementationIsOnBaseClass()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(DerivedClassClassWithMixedGenericInterfaces));
        var methods = type.Properties;

        var result = methods.SelectMany(m => m.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "System.Boolean BaseClassWithMixedGenericInterfaces::Property()";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsCorrectMethodFromBaseInterfaceOnClassWithMixedGenericInterfaces()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(ClassWithMixedGenericInterfaces));
        var method = type.Methods.Single(m => m.Name.Equals(nameof(ClassWithMixedGenericInterfaces.Method)) && m.ReturnType == module.TypeSystem.Int32);

        var result = method.EnumerateOverridesAndImplementations();

        var actual = string.Join("|", result);
        var expected = "T2 IGenericBaseInterface`2::Method(T1)";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsCorrectMethodOnClassWithMixedGenericInterfaces2()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(ClassWithMixedGenericInterfaces2));
        var method = type.Methods.Single(m => m.Name.Equals(nameof(ClassWithMixedGenericInterfaces2.Method)) && m.Parameters[0].ParameterType == module.TypeSystem.Int32);

        var result = method.EnumerateOverridesAndImplementations();

        var actual = string.Join("|", result);
        var expected = "U3 IGenericDerivedInterface2`3::Method(U2)";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsCorrectMethodFromBaseInterfaceOnClassWithMixedGenericInterfaces2()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(ClassWithMixedGenericInterfaces2));
        var method = type.Methods.Single(m => m.Name.Equals(nameof(ClassWithMixedGenericInterfaces2.Method)) && m.Parameters[0].ParameterType == module.TypeSystem.String);

        var result = method.EnumerateOverridesAndImplementations();

        var actual = string.Join("|", result);
        var expected = "T2 IGenericBaseInterface`2::Method(T1)";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsNoMethodFromBaseInterfaceOnClassWithMixedGenericInterfacesWhenExplictImplementationExists()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(ClassWithMixedGenericInterfaces3));
        var method = type.Methods.Single(m => m.Name.Equals(nameof(ClassWithMixedGenericInterfaces3.Method)) && m.Parameters[0].ParameterType == module.TypeSystem.String);

        var result = method.EnumerateOverridesAndImplementations();
        Assert.Empty(result);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsNoPropertyFromBaseInterfaceOnClassWithMixedGenericInterfacesWhenExplictImplementationExists()
    {
        var type = module.GetTypes().Single(t => t.Name == nameof(ClassWithMixedGenericInterfaces3));
        var property = type.Properties.Single(m => m.Name.Equals(nameof(ClassWithMixedGenericInterfaces3.Property)));

        var result = property.EnumerateOverridesAndImplementations();
        Assert.Empty(result);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsBaseMembersForMethodOverrides1()
    {
        var type = module.GetTypes().Single(t => t.Name.Contains(nameof(DerivedGenericClass1<string>)));
        var values = type.Methods.Where(m => !m.IsSpecialName);

        var result = values.SelectMany(item => item.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "T2 GenericBaseClass`2::Method(T1)";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsBaseMembersForMethodOverrides2()
    {
        var type = module.GetTypes().Single(t => t.Name.Contains(nameof(DerivedGenericClass2<string>)));
        var values = type.Methods.Where(m => !m.IsSpecialName);

        var result = values.SelectMany(item => item.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "T1 GenericBaseClass`2::Method(T2)";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsBaseMembersForMethodOverridesInDerivedDerived()
    {
        var type = module.GetTypes().Single(t => t.Name.Contains(nameof(DerivedDerivedClass)));
        var values = type.Methods.Where(m => !m.IsSpecialName);

        var result = values.SelectMany(item => item.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "T1 GenericBaseClass`2::Method(T2)";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsBaseMembersForPropertyOverrides()
    {
        var type = module.GetTypes().Single(t => t.Name.Contains(nameof(DerivedGenericClass2<string>)));
        var values = type.Properties;

        var result = values.SelectMany(item => item.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "T1 GenericBaseClass`2::Property()";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsBaseMembersForPropertyOverridesInDerivedDerived()
    {
        var type = module.GetTypes().Single(t => t.Name.Contains(nameof(DerivedDerivedClass)));
        var values = type.Properties;

        var result = values.SelectMany(item => item.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "T1 GenericBaseClass`2::Property()";
        Assert.Equal(expected, actual);
    }
}