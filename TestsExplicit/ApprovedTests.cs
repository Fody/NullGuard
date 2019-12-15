using Xunit.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;
using Fody;
using Verify;

public class ApprovedTests :
    VerifyBase
{
    [Fact]
    public Task ClassWithBadAttributes()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithBadAttributes"), settings);
    }

    [Fact]
    public Task ClassWithPrivateMethod()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithPrivateMethod"), settings);
    }

    [Fact]
    public Task ClassWithPrivateMethodNoAssert()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithPrivateMethod"), settings);
    }

    [Fact]
    public Task GenericClass()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "GenericClass`1"), settings);
    }

    [Fact]
    public Task Indexers()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "Indexers"), settings);
    }

    [Fact]
    public Task InterfaceBadAttributes()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "InterfaceBadAttributes"), settings);
    }

    [Fact]
    public Task SimpleClass()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "SimpleClass"), settings);
    }

    [Fact]
    public Task SimpleClassNoAssert()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "SimpleClass"), settings);
    }

    [Fact]
    public Task SkipIXamlMetadataProvider()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "XamlMetadataProvider"), settings);
    }

    [Fact]
    public Task SpecialClass()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "SpecialClass"), settings);
    }

    [Fact]
    public Task PublicNestedInsideNonPublic()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "NonPublicWithNested"), settings);
    }

    [Fact]
    public Task UnsafeClass()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "UnsafeClass"), settings);
    }

    [Fact]
    public Task DerivedClass()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "InternalBase.DerivedClass"), settings);
    }

    [Fact]
    public Task ImplementsInterface()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "InternalBase.ImplementsInterface"), settings);
    }

    [Fact]
    public Task ImplementsInheritedInterface()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "InternalBase.ImplementsInheritedInterface"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceExplicit()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "InternalBase.ImplementsInterfaceExplicit"), settings);
    }

    [Fact]
    public Task DerivedClassAssemblyBase()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "AssemblyBase.DerivedClass"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceAssemblyBase()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "AssemblyBase.ImplementsInterface"), settings);
    }

    [Fact]
    public Task ImplementsInheritedInterfaceAssemblyBase()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "AssemblyBase.ImplementsInheritedInterface"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceExplicitAssemblyBase()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "AssemblyBase.ImplementsInterfaceExplicit"), settings);
    }

    [Fact]
    public Task DerivedClassExternalBase()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ExternalBase.DerivedClass"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceExternalBase()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ExternalBase.ImplementsInterface"), settings);
    }

    [Fact]
    public Task ImplementsInheritedInterfaceExternalBase()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ExternalBase.ImplementsInheritedInterface"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceExplicitExternalBase()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ExternalBase.ImplementsInterfaceExplicit"), settings);
    }

    [Fact]
    public Task InfosList()
    {
        return Verify(AssemblyWeaver.TestResult.Messages.Select(x => x.Text), settings);
    }

    [Fact]
    public Task WarnsList()
    {
        return Verify(AssemblyWeaver.TestResult.Warnings.Select(x => x.Text), settings);
    }

    [Fact]
    public Task ErrorsList()
    {
        return Verify(AssemblyWeaver.TestResult.Errors.Select(x => x.Text), settings);
    }

    VerifySettings settings;

    public ApprovedTests(ITestOutputHelper output) :
        base(output)
    {
        settings = new VerifySettings();
        settings.UniqueForRuntime();
        settings.UniqueForAssemblyConfiguration();
        settings.AddScrubber(v => v.Replace("InternalBase.", string.Empty));
        settings.AddScrubber(v => v.Replace("AssemblyBase.", string.Empty));
        settings.AddScrubber(v => v.Replace("ExternalBase.", string.Empty));
    }
}