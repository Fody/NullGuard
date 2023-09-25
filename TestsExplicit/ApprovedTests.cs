using System.Linq;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;
using VerifyTests;
using VerifyTests.ICSharpCode.Decompiler;

[UsesVerify]
public class ApprovedTests
{
    [Fact]
    public Task ClassWithBadAttributes()
    {
        return Verifier.Verify(GetType("ClassWithBadAttributes"), settings);
    }

    [Fact]
    public Task ClassWithPrivateMethod()
    {
        return Verifier.Verify(GetType("ClassWithPrivateMethod"), settings);
    }

    [Fact]
    public Task ClassWithPrivateMethodNoAssert()
    {
        return Verifier.Verify(GetType("ClassWithPrivateMethod"), settings);
    }

    [Fact]
    public Task GenericClass()
    {
        return Verifier.Verify(GetType("GenericClass`1"), settings);
    }

    [Fact]
    public Task Indexers()
    {
        return Verifier.Verify(GetType("Indexers"), settings);
    }

    [Fact]
    public Task InterfaceBadAttributes()
    {
        return Verifier.Verify(GetType("InterfaceBadAttributes"), settings);
    }

    [Fact]
    public Task SimpleClass()
    {
        return Verifier.Verify(GetType("SimpleClass"), settings);
    }

    [Fact]
    public Task SimpleClassNoAssert()
    {
        return Verifier.Verify(GetType("SimpleClass"), settings);
    }

    [Fact]
    public Task SkipIXamlMetadataProvider()
    {
        return Verifier.Verify(GetType("XamlMetadataProvider"), settings);
    }
#if DEBUG
    [Fact]
    public Task SpecialClass()
    {
        return Verifier.Verify(GetType("SpecialClass"), settings);
    }
#endif

    [Fact]
    public Task PublicNestedInsideNonPublic()
    {
        return Verifier.Verify(GetType("NonPublicWithNested"), settings);
    }

    [Fact]
    public Task UnsafeClass()
    {
        return Verifier.Verify(GetType("UnsafeClass"), settings);
    }

    [Fact]
    public Task DerivedClass()
    {
        return Verifier.Verify(GetType("InternalBase.DerivedClass"), settings);
    }

    [Fact]
    public Task ImplementsInterface()
    {
        return Verifier.Verify(GetType("InternalBase.ImplementsInterface"), settings);
    }

    [Fact]
    public Task ImplementsInheritedInterface()
    {
        return Verifier.Verify(GetType("InternalBase.ImplementsInheritedInterface"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceExplicit()
    {
        return Verifier.Verify(GetType("InternalBase.ImplementsInterfaceExplicit"), settings);
    }

    [Fact]
    public Task DerivedClassAssemblyBase()
    {
        return Verifier.Verify(GetType("AssemblyBase.DerivedClass"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceAssemblyBase()
    {
        return Verifier.Verify(GetType("AssemblyBase.ImplementsInterface"), settings);
    }

    [Fact]
    public Task ImplementsInheritedInterfaceAssemblyBase()
    {
        return Verifier.Verify(GetType("AssemblyBase.ImplementsInheritedInterface"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceExplicitAssemblyBase()
    {
        return Verifier.Verify(GetType("AssemblyBase.ImplementsInterfaceExplicit"), settings);
    }

    [Fact]
    public Task DerivedClassExternalBase()
    {
        return Verifier.Verify(GetType("ExternalBase.DerivedClass"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceExternalBase()
    {
        return Verifier.Verify(GetType("ExternalBase.ImplementsInterface"), settings);
    }

    [Fact]
    public Task ImplementsInheritedInterfaceExternalBase()
    {
        return Verifier.Verify(GetType("ExternalBase.ImplementsInheritedInterface"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceExplicitExternalBase()
    {
        return Verifier.Verify(GetType("ExternalBase.ImplementsInterfaceExplicit"), settings);
    }

    [Fact]
    public Task InfosList()
    {
        return Verifier.Verify(AssemblyWeaver.TestResult.Messages.Select(_ => _.Text), settings);
    }

    [Fact]
    public Task WarnsList()
    {
        return Verifier.Verify(AssemblyWeaver.TestResult.Warnings.Select(_ => _.Text), settings);
    }

    [Fact]
    public Task ErrorsList()
    {
        return Verifier.Verify(AssemblyWeaver.TestResult.Errors.Select(_ => _.Text), settings);
    }

    VerifySettings settings;

    public ApprovedTests()
    {
        settings = new();
        settings.UniqueForRuntime();
        settings.UniqueForAssemblyConfiguration();
        settings.AddScrubber(v => v.Replace("InternalBase.", string.Empty));
        settings.AddScrubber(v => v.Replace("AssemblyBase.", string.Empty));
        settings.AddScrubber(v => v.Replace("ExternalBase.", string.Empty));
        settings.AddScrubber(v => v.Replace("[System.Private.CoreLib]", "[netstandard]"));
    }

    private static TypeToDisassemble GetType(string typeName)
    {
        return new(AssemblyWeaver.PeFile, typeName);
    }
}