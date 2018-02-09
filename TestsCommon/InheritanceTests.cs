using System;
using System.Linq;
using Mono.Cecil;
using Xunit;

public class InheritanceTests
{
    ModuleDefinition _module = ModuleDefinition.ReadModule(typeof(InheritanceTests).Assembly.Location);

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsExplicitImplementedInterfaceMethods()
    {
        var type = _module.GetTypes().Single(t => t.Name == nameof(ClassWithExplicitInterfaceImplentation));
        var methods = type.Methods.Where(m => m.Name.EndsWith(nameof(IComparable.CompareTo))).ToArray();
        Assert.Equal(2, methods.Length);

        var result = methods.SelectMany(method => method.EnumerateOverridesAndImplementations());
        var expected = "System.Int32 System.IComparable`1::CompareTo(T)|System.Int32 System.IComparable`1::CompareTo(T)";
        var actual = string.Join("|", result);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsImplicitImplementedInterfaceMethodss()
    {
        var type = _module.GetTypes().Single(t => t.Name == nameof(ClassWithImplicitInterfaceImplementation));
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
        var type = _module.GetTypes().Single(t => t.Name == nameof(ClassWithExplicitAndImplicitInterfaceImplementation));
        var methods = type.Methods.Where(m => m.Name.EndsWith(nameof(IComparable.CompareTo))).ToArray();
        Assert.Equal(3, methods.Length);

        var interfaceMethods = methods.SelectMany(method => method.EnumerateOverridesAndImplementations()).ToArray();
        Assert.Equal(2, interfaceMethods.Length);

        var result = methods.SelectMany(method => method.EnumerateOverridesAndImplementations());
        var expected = "System.Int32 System.IComparable`1::CompareTo(T)|System.Int32 System.IComparable`1::CompareTo(T)";
        var actual = string.Join("|", result);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsCorrectMethodOnClassWithMixedGenericInterfaces()
    {
        var type = _module.GetTypes().Single(t => t.Name == nameof(ClassWithMixedGenericInterfaces));
        var method = type.Methods.Single(m => m.Name.Equals(nameof(ClassWithMixedGenericInterfaces.Method)) && m.ReturnType == _module.TypeSystem.Boolean);

        var result = method.EnumerateOverridesAndImplementations();

        var actual = string.Join("|", result);
        var expected = "U3 IGenericDerivedInterface`3::Method(U2)";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsCorrectMethodOnDerivedClassWithMixedGenericInterfacesWhereOriginalImplementationIsOnBaseClass()
    {
        var type = _module.GetTypes().Single(t => t.Name == nameof(DerivedClassClassWithMixedGenericInterfaces));
        var methods = type.Methods.Where(m => !m.IsSpecialName);

        var result = methods.SelectMany(m => m.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "System.Int32 BaseClassWithMixedGenericInterfaces::Method(System.Boolean)|T2 IGenericBaseInterface`2::Method(T1)";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsCorrectPropertyOnDerivedClassWithMixedGenericInterfacesWhereOriginalImplementationIsOnBaseClass()
    {
        var type = _module.GetTypes().Single(t => t.Name == nameof(DerivedClassClassWithMixedGenericInterfaces));
        var methods = type.Properties;

        var result = methods.SelectMany(m => m.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "System.Boolean BaseClassWithMixedGenericInterfaces::Property()";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsCorrectMethodFromBaseInterfaceOnClassWithMixedGenericInterfaces()
    {
        var type = _module.GetTypes().Single(t => t.Name == nameof(ClassWithMixedGenericInterfaces));
        var method = type.Methods.Single(m => m.Name.Equals(nameof(ClassWithMixedGenericInterfaces.Method)) && m.ReturnType == _module.TypeSystem.Int32);

        var result = method.EnumerateOverridesAndImplementations();

        var actual = string.Join("|", result);
        var expected = "T2 IGenericBaseInterface`2::Method(T1)";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsCorrectMethodOnClassWithMixedGenericInterfaces2()
    {
        var type = _module.GetTypes().Single(t => t.Name == nameof(ClassWithMixedGenericInterfaces2));
        var method = type.Methods.Single(m => m.Name.Equals(nameof(ClassWithMixedGenericInterfaces2.Method)) && m.Parameters[0].ParameterType == _module.TypeSystem.Int32);

        var result = method.EnumerateOverridesAndImplementations();

        var actual = string.Join("|", result);
        var expected = "U3 IGenericDerivedInterface2`3::Method(U2)";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsCorrectMethodFromBaseInterfaceOnClassWithMixedGenericInterfaces2()
    {
        var type = _module.GetTypes().Single(t => t.Name == nameof(ClassWithMixedGenericInterfaces2));
        var method = type.Methods.Single(m => m.Name.Equals(nameof(ClassWithMixedGenericInterfaces2.Method)) && m.Parameters[0].ParameterType == _module.TypeSystem.String);

        var result = method.EnumerateOverridesAndImplementations();

        var actual = string.Join("|", result);
        var expected = "T2 IGenericBaseInterface`2::Method(T1)";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsNoMethodFromBaseInterfaceOnClassWithMixedGenericInterfacesWhenExplictImplementationExists()
    {
        var type = _module.GetTypes().Single(t => t.Name == nameof(ClassWithMixedGenericInterfaces3));
        var method = type.Methods.Single(m => m.Name.Equals(nameof(ClassWithMixedGenericInterfaces3.Method)) && m.Parameters[0].ParameterType == _module.TypeSystem.String);

        var result = method.EnumerateOverridesAndImplementations();
        Assert.Empty(result);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsNoPropertyFromBaseInterfaceOnClassWithMixedGenericInterfacesWhenExplictImplementationExists()
    {
        var type = _module.GetTypes().Single(t => t.Name == nameof(ClassWithMixedGenericInterfaces3));
        var property = type.Properties.Single(m => m.Name.Equals(nameof(ClassWithMixedGenericInterfaces3.Property)));

        var result = property.EnumerateOverridesAndImplementations();
        Assert.Empty(result);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsBaseMembersForMethodOverrides1()
    {
        var type = _module.GetTypes().Single(t => t.Name.Contains(nameof(DerivedGenericClass1<string>)));
        var values = type.Methods.Where(m => !m.IsSpecialName);

        var result = values.SelectMany(item => item.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "T2 GenericBaseClass`2::Method(T1)";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsBaseMembersForMethodOverrides2()
    {
        var type = _module.GetTypes().Single(t => t.Name.Contains(nameof(DerivedGenericClass2<string>)));
        var values = type.Methods.Where(m => !m.IsSpecialName);

        var result = values.SelectMany(item => item.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "T1 GenericBaseClass`2::Method(T2)";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsBaseMembersForMethodOverridesInDerivedDerived()
    {
        var type = _module.GetTypes().Single(t => t.Name.Contains(nameof(DerivedDerivedClass)));
        var values = type.Methods.Where(m => !m.IsSpecialName);

        var result = values.SelectMany(item => item.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "T1 GenericBaseClass`2::Method(T2)";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsBaseMembersForPropertyOverrides()
    {
        var type = _module.GetTypes().Single(t => t.Name.Contains(nameof(DerivedGenericClass2<string>)));
        var values = type.Properties;

        var result = values.SelectMany(item => item.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "T1 GenericBaseClass`2::Property()";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateOverridesAndImplementationsFindsBaseMembersForPropertyOverridesInDerivedDerived()
    {
        var type = _module.GetTypes().Single(t => t.Name.Contains(nameof(DerivedDerivedClass)));
        var values = type.Properties;

        var result = values.SelectMany(item => item.EnumerateOverridesAndImplementations());

        var actual = string.Join("|", result);
        var expected = "T1 GenericBaseClass`2::Property()";
        Assert.Equal(expected, actual);
    }
}

public class ClassWithExplicitInterfaceImplentation : IComparable<string>, IComparable<int>
{
    int IComparable<string>.CompareTo(string other)
    {
        return 0;
    }

    int IComparable<int>.CompareTo(int other)
    {
        return 0;
    }
}

public class ClassWithImplicitInterfaceImplementation : IComparable<string>, IComparable<int>
{
    public int CompareTo(string other)
    {
        return 0;
    }

    public int CompareTo(int other)
    {
        return 0;
    }
}

public class ClassWithExplicitAndImplicitInterfaceImplementation : IComparable<string>, IComparable<int>
{
    int IComparable<string>.CompareTo(string other)
    {
        return 0;
    }

    public int CompareTo(string other)
    {
        return 0;
    }

    public int CompareTo(int other)
    {
        return 0;
    }
}

public interface IGenericBaseInterface<T1, T2>
{
    T2 Method(T1 param);

    T1 Property { get; set; }
}

public interface IGenericDerivedInterface<U1, U2, U3> : IGenericBaseInterface<U3, U2>
{
    U1 DerivedMethod(U3 derivedParam);

    U3 Method(U2 param);
}

public interface IGenericDerivedInterface2<U1, U2, U3> : IGenericBaseInterface<U1, U3>
{
    U1 DerivedMethod(U3 derivedParam);

    U3 Method(U2 param);
}

public class ClassWithMixedGenericInterfaces : IGenericDerivedInterface<string, int, bool>
{
    public int Method(bool param)
    {
        return 0;
    }

    bool IGenericBaseInterface<bool, int>.Property
    {
        get;
        set;
    }

    string IGenericDerivedInterface<string, int, bool>.DerivedMethod(bool derivedParam)
    {
        return default(string);
    }

    public bool Method(int param)
    {
        return false;
    }
}

public class ClassWithMixedGenericInterfaces2 : IGenericDerivedInterface2<string, int, bool>
{
    public bool Method(string param)
    {
        return false;
    }

    string IGenericBaseInterface<string, bool>.Property { get; set; }

    string IGenericDerivedInterface2<string, int, bool>.DerivedMethod(bool derivedParam)
    {
        return default(string);
    }

    public bool Method(int param)
    {
        return false;
    }


}

public class ClassWithMixedGenericInterfaces3 : IGenericDerivedInterface2<string, int, bool>
{
    public bool Method(string param)
    {
        return false;
    }

    bool IGenericBaseInterface<string, bool>.Method(string param)
    {
        return false;
    }

    string IGenericBaseInterface<string, bool>.Property { get; set; }

    public string Property { get; set; }

    string IGenericDerivedInterface2<string, int, bool>.DerivedMethod(bool derivedParam)
    {
        return default(string);
    }

    public bool Method(int param)
    {
        return false;
    }
}

public class BaseClassWithMixedGenericInterfaces : IGenericDerivedInterface<string, int, bool>
{
    public virtual int Method(bool param)
    {
        return 0;
    }

    bool IGenericBaseInterface<bool, int>.Property
    {
        get;
        set;
    }

    string IGenericDerivedInterface<string, int, bool>.DerivedMethod(bool derivedParam)
    {
        return default(string);
    }

    public virtual bool Property { get; set; }

    public bool Method(int param)
    {
        return false;
    }
}

public class DerivedClassClassWithMixedGenericInterfaces : BaseClassWithMixedGenericInterfaces
{
    public override int Method(bool param)
    {
        return 1;
    }

    public string Method(string param)
    {
        return default;
    }

    public override bool Property { get; set; }
}

public class GenericBaseClass<T1, T2>
{
    public virtual T2 Method(T1 param)
    {
        return default;
    }

    public virtual T1 Method(T2 param)
    {
        return default;
    }

    public virtual T1 Property { get; set; }
}

internal class DerivedGenericClass1<T> : GenericBaseClass<string, T>
{
    public override T Method(string param)
    {
        return base.Method(param + "1");
    }

    public override string Property
    {
        get
        {
            return base.Property + "1";
        }
        set
        {
            base.Property = value;
        }
    }
}

internal class DerivedGenericClass2<T> : GenericBaseClass<string, T>
{
    public override string Method(T param)
    {
        return base.Method(default(T));
    }
    public override string Property
    {
        get
        {
            return base.Property + "1";
        }
        set
        {
            base.Property = value;
        }
    }
}

internal class EmptyDerivedClass<Q> : GenericBaseClass<int, Q>
{
}

internal class DerivedDerivedClass : EmptyDerivedClass<string>
{
    public override int Method(string param)
    {
        return base.Method(param + "1");
    }

    public override int Property
    {
        get { return base.Property + 1; }
        set { base.Property = value; }
    }
}