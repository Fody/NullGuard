using Xunit.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;
using Fody;

public class ApprovedTests :
    VerifyBase
{
    [Fact]
    public Task ClassWithBadAttributes()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithBadAttributes"));
    }

    [Fact]
    public Task ClassWithPrivateMethod()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithPrivateMethod"));
    }

    [Fact]
    public Task ClassWithPrivateMethodNoAssert()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithPrivateMethod"));
    }

    [Fact]
    public Task GenericClass()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "GenericClass`1"));
    }

    [Fact]
    public Task Indexers()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "Indexers"));
    }

    [Fact]
    public Task InterfaceBadAttributes()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "InterfaceBadAttributes"));
    }

    [Fact]
    public Task SimpleClass()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "SimpleClass"));
    }

    [Fact]
    public Task SimpleClassNoAssert()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "SimpleClass"));
    }

    [Fact]
    public Task SkipIXamlMetadataProvider()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "XamlMetadataProvider"));
    }

    [Fact]
    public Task SpecialClass()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "SpecialClass"));
    }

    [Fact]
    public Task PublicNestedInsideNonPublic()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "NonPublicWithNested"));
    }

    [Fact]
    public Task UnsafeClass()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "UnsafeClass"));
    }

    [Fact]
    public Task DerivedClass()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "InternalBase.DerivedClass"));
    }

    [Fact]
    public Task ImplementsInterface()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "InternalBase.ImplementsInterface"));
    }

    [Fact]
    public Task ImplementsInheritedInterface()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "InternalBase.ImplementsInheritedInterface"));
    }

    [Fact]
    public Task ImplementsInterfaceExplicit()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "InternalBase.ImplementsInterfaceExplicit"));
    }

    [Fact]
    public Task DerivedClassAssemblyBase()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "AssemblyBase.DerivedClass"));
    }

    [Fact]
    public Task ImplementsInterfaceAssemblyBase()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "AssemblyBase.ImplementsInterface"));
    }

    [Fact]
    public Task ImplementsInheritedInterfaceAssemblyBase()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "AssemblyBase.ImplementsInheritedInterface"));
    }

    [Fact]
    public Task ImplementsInterfaceExplicitAssemblyBase()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "AssemblyBase.ImplementsInterfaceExplicit"));
    }

    [Fact]
    public Task DerivedClassExternalBase()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ExternalBase.DerivedClass"));
    }

    [Fact]
    public Task ImplementsInterfaceExternalBase()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ExternalBase.ImplementsInterface"));
    }

    [Fact]
    public Task ImplementsInheritedInterfaceExternalBase()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ExternalBase.ImplementsInheritedInterface"));
    }

    [Fact]
    public Task ImplementsInterfaceExplicitExternalBase()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ExternalBase.ImplementsInterfaceExplicit"));
    }

    [Fact]
    public Task InfosList()
    {
        return Verify(AssemblyWeaver.TestResult.Messages.Select(x=>x.Text));
    }

    [Fact]
    public Task WarnsList()
    {
        return Verify(AssemblyWeaver.TestResult.Warnings.Select(x=>x.Text));
    }

    [Fact]
    public Task ErrorsList()
    {
        return Verify(AssemblyWeaver.TestResult.Errors.Select(x=>x.Text));
    }

    public ApprovedTests(ITestOutputHelper output) :
        base(output)
    {
        UniqueForRuntime();
        UniqueForAssemblyConfiguration();
        AddScrubber(v => v.Replace("InternalBase.", string.Empty));
        AddScrubber(v => v.Replace("AssemblyBase.", string.Empty));
        AddScrubber(v => v.Replace("ExternalBase.", string.Empty));
    }
}