using System.Linq;
using System.Threading.Tasks;
using VerifyTUnit;
using VerifyTests;
using VerifyTests.ICSharpCode.Decompiler;

public class ApprovedTests
{
    [Test]
    public async Task ClassWithBadAttributes()
    {
        await Verifier.Verify(GetType("ClassWithBadAttributes"), settings);
    }

    [Test]
    public async Task ClassWithPrivateMethod()
    {
        await Verifier.Verify(GetType("ClassWithPrivateMethod"), settings);
    }

    [Test]
    public async Task ClassWithPrivateMethodNoAssert()
    {
        await Verifier.Verify(GetType("ClassWithPrivateMethod"), settings);
    }

    [Test]
    public async Task GenericClass()
    {
        await Verifier.Verify(GetType("GenericClass`1"), settings);
    }

    [Test]
    public async Task Indexers()
    {
        await Verifier.Verify(GetType("Indexers"), settings);
    }

    [Test]
    public async Task InterfaceBadAttributes()
    {
        await Verifier.Verify(GetType("InterfaceBadAttributes"), settings);
    }

    [Test]
    public async Task SimpleClass()
    {
        await Verifier.Verify(GetType("SimpleClass"), settings);
    }

    [Test]
    public async Task SimpleClassNoAssert()
    {
        await Verifier.Verify(GetType("SimpleClass"), settings);
    }

    [Test]
    public async Task SkipIXamlMetadataProvider()
    {
        await Verifier.Verify(GetType("XamlMetadataProvider"), settings);
    }
#if DEBUG
    [Test]
    public async Task SpecialClass()
    {
        await Verifier.Verify(GetType("SpecialClass"), settings);
    }
#endif

    [Test]
    public async Task PublicNestedInsideNonPublic()
    {
        await Verifier.Verify(GetType("NonPublicWithNested"), settings);
    }

    [Test]
    public async Task UnsafeClass()
    {
        await Verifier.Verify(GetType("UnsafeClass"), settings);
    }

    [Test]
    public async Task DerivedClass()
    {
        await Verifier.Verify(GetType("InternalBase.DerivedClass"), settings);
    }

    [Test]
    public async Task ImplementsInterface()
    {
        await Verifier.Verify(GetType("InternalBase.ImplementsInterface"), settings);
    }

    [Test]
    public async Task ImplementsInheritedInterface()
    {
        await Verifier.Verify(GetType("InternalBase.ImplementsInheritedInterface"), settings);
    }

    [Test]
    public async Task ImplementsInterfaceExplicit()
    {
        await Verifier.Verify(GetType("InternalBase.ImplementsInterfaceExplicit"), settings);
    }

    [Test]
    public async Task DerivedClassAssemblyBase()
    {
        await Verifier.Verify(GetType("AssemblyBase.DerivedClass"), settings);
    }

    [Test]
    public async Task ImplementsInterfaceAssemblyBase()
    {
        await Verifier.Verify(GetType("AssemblyBase.ImplementsInterface"), settings);
    }

    [Test]
    public async Task ImplementsInheritedInterfaceAssemblyBase()
    {
        await Verifier.Verify(GetType("AssemblyBase.ImplementsInheritedInterface"), settings);
    }

    [Test]
    public async Task ImplementsInterfaceExplicitAssemblyBase()
    {
        await Verifier.Verify(GetType("AssemblyBase.ImplementsInterfaceExplicit"), settings);
    }

    [Test]
    public async Task DerivedClassExternalBase()
    {
        await Verifier.Verify(GetType("ExternalBase.DerivedClass"), settings);
    }

    [Test]
    public async Task ImplementsInterfaceExternalBase()
    {
        await Verifier.Verify(GetType("ExternalBase.ImplementsInterface"), settings);
    }

    [Test]
    public async Task ImplementsInheritedInterfaceExternalBase()
    {
        await Verifier.Verify(GetType("ExternalBase.ImplementsInheritedInterface"), settings);
    }

    [Test]
    public async Task ImplementsInterfaceExplicitExternalBase()
    {
        await Verifier.Verify(GetType("ExternalBase.ImplementsInterfaceExplicit"), settings);
    }

    [Test]
    public async Task InfosList()
    {
        await Verifier.Verify(AssemblyWeaver.TestResult.Messages.Select(_ => _.Text), settings);
    }

    [Test]
    public async Task WarnsList()
    {
        await Verifier.Verify(AssemblyWeaver.TestResult.Warnings.Select(_ => _.Text), settings);
    }

    [Test]
    public async Task ErrorsList()
    {
        await Verifier.Verify(AssemblyWeaver.TestResult.Errors.Select(_ => _.Text), settings);
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