using System.Linq;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;
using Fody;
using VerifyTests;

[UsesVerify]
public class ApprovedTests
{
    [Fact]
    public Task ClassWithBadAttributes()
    {
        return Verifier.Verify(Decompile("ClassWithBadAttributes"), settings);
    }

    [Fact]
    public Task ClassWithPrivateMethod()
    {
        return Verifier.Verify(Decompile("ClassWithPrivateMethod"), settings);
    }

    [Fact]
    public Task ClassWithPrivateMethodNoAssert()
    {
        return Verifier.Verify(Decompile("ClassWithPrivateMethod"), settings);
    }

    [Fact]
    public Task GenericClass()
    {
        return Verifier.Verify(Decompile("GenericClass`1"), settings);
    }

    [Fact]
    public Task Indexers()
    {
        return Verifier.Verify(Decompile("Indexers"), settings);
    }

    [Fact]
    public Task InterfaceBadAttributes()
    {
        return Verifier.Verify(Decompile("InterfaceBadAttributes"), settings);
    }

    [Fact]
    public Task SimpleClass()
    {
        return Verifier.Verify(Decompile("SimpleClass"), settings);
    }

    [Fact]
    public Task SimpleClassNoAssert()
    {
        return Verifier.Verify(Decompile("SimpleClass"), settings);
    }

    [Fact]
    public Task SkipIXamlMetadataProvider()
    {
        return Verifier.Verify(Decompile("XamlMetadataProvider"), settings);
    }

    [Fact]
    public Task SpecialClass()
    {
        return Verifier.Verify(Decompile("SpecialClass"), settings);
    }

    [Fact]
    public Task PublicNestedInsideNonPublic()
    {
        return Verifier.Verify(Decompile("NonPublicWithNested"), settings);
    }

    [Fact]
    public Task UnsafeClass()
    {
        return Verifier.Verify(Decompile("UnsafeClass"), settings);
    }

    [Fact]
    public Task DerivedClass()
    {
        return Verifier.Verify(Decompile("InternalBase.DerivedClass"), settings);
    }

    [Fact]
    public Task ImplementsInterface()
    {
        return Verifier.Verify(Decompile("InternalBase.ImplementsInterface"), settings);
    }

    [Fact]
    public Task ImplementsInheritedInterface()
    {
        return Verifier.Verify(Decompile("InternalBase.ImplementsInheritedInterface"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceExplicit()
    {
        return Verifier.Verify(Decompile("InternalBase.ImplementsInterfaceExplicit"), settings);
    }

    [Fact]
    public Task DerivedClassAssemblyBase()
    {
        return Verifier.Verify(Decompile("AssemblyBase.DerivedClass"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceAssemblyBase()
    {
        return Verifier.Verify(Decompile("AssemblyBase.ImplementsInterface"), settings);
    }

    [Fact]
    public Task ImplementsInheritedInterfaceAssemblyBase()
    {
        return Verifier.Verify(Decompile("AssemblyBase.ImplementsInheritedInterface"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceExplicitAssemblyBase()
    {
        return Verifier.Verify(Decompile("AssemblyBase.ImplementsInterfaceExplicit"), settings);
    }

    [Fact]
    public Task DerivedClassExternalBase()
    {
        return Verifier.Verify(Decompile("ExternalBase.DerivedClass"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceExternalBase()
    {
        return Verifier.Verify(Decompile("ExternalBase.ImplementsInterface"), settings);
    }

    [Fact]
    public Task ImplementsInheritedInterfaceExternalBase()
    {
        return Verifier.Verify(Decompile("ExternalBase.ImplementsInheritedInterface"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceExplicitExternalBase()
    {
        return Verifier.Verify(Decompile("ExternalBase.ImplementsInterfaceExplicit"), settings);
    }

    [Fact]
    public Task InfosList()
    {
        return Verifier.Verify(AssemblyWeaver.TestResult.Messages.Select(x => x.Text), settings);
    }

    [Fact]
    public Task WarnsList()
    {
        return Verifier.Verify(AssemblyWeaver.TestResult.Warnings.Select(x => x.Text), settings);
    }

    [Fact]
    public Task ErrorsList()
    {
        return Verifier.Verify(AssemblyWeaver.TestResult.Errors.Select(x => x.Text), settings);
    }

    VerifySettings settings;

    public ApprovedTests()
    {
        settings = new VerifySettings();
        settings.UniqueForRuntime();
        settings.UniqueForAssemblyConfiguration();
        settings.AddScrubber(v => v.Replace("InternalBase.", string.Empty));
        settings.AddScrubber(v => v.Replace("AssemblyBase.", string.Empty));
        settings.AddScrubber(v => v.Replace("ExternalBase.", string.Empty));
    }

    private string Decompile(string item)
    {
        return Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, item);
    }
}