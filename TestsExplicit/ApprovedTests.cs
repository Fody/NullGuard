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
        return Verify(Decompile("ClassWithBadAttributes"), settings);
    }

    [Fact]
    public Task ClassWithPrivateMethod()
    {
        return Verify(Decompile("ClassWithPrivateMethod"), settings);
    }

    [Fact]
    public Task ClassWithPrivateMethodNoAssert()
    {
        return Verify(Decompile("ClassWithPrivateMethod"), settings);
    }

    [Fact]
    public Task GenericClass()
    {
        return Verify(Decompile("GenericClass`1"), settings);
    }

    [Fact]
    public Task Indexers()
    {
        return Verify(Decompile("Indexers"), settings);
    }

    [Fact]
    public Task InterfaceBadAttributes()
    {
        return Verify(Decompile("InterfaceBadAttributes"), settings);
    }

    [Fact]
    public Task SimpleClass()
    {
        return Verify(Decompile("SimpleClass"), settings);
    }

    [Fact]
    public Task SimpleClassNoAssert()
    {
        return Verify(Decompile("SimpleClass"), settings);
    }

    [Fact]
    public Task SkipIXamlMetadataProvider()
    {
        return Verify(Decompile("XamlMetadataProvider"), settings);
    }

    [Fact]
    public Task SpecialClass()
    {
        return Verify(Decompile("SpecialClass"), settings);
    }

    [Fact]
    public Task PublicNestedInsideNonPublic()
    {
        return Verify(Decompile("NonPublicWithNested"), settings);
    }

    [Fact]
    public Task UnsafeClass()
    {
        return Verify(Decompile("UnsafeClass"), settings);
    }

    [Fact]
    public Task DerivedClass()
    {
        return Verify(Decompile("InternalBase.DerivedClass"), settings);
    }

    [Fact]
    public Task ImplementsInterface()
    {
        return Verify(Decompile("InternalBase.ImplementsInterface"), settings);
    }

    [Fact]
    public Task ImplementsInheritedInterface()
    {
        return Verify(Decompile("InternalBase.ImplementsInheritedInterface"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceExplicit()
    {
        return Verify(Decompile("InternalBase.ImplementsInterfaceExplicit"), settings);
    }

    [Fact]
    public Task DerivedClassAssemblyBase()
    {
        return Verify(Decompile("AssemblyBase.DerivedClass"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceAssemblyBase()
    {
        return Verify(Decompile("AssemblyBase.ImplementsInterface"), settings);
    }

    [Fact]
    public Task ImplementsInheritedInterfaceAssemblyBase()
    {
        return Verify(Decompile("AssemblyBase.ImplementsInheritedInterface"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceExplicitAssemblyBase()
    {
        return Verify(Decompile("AssemblyBase.ImplementsInterfaceExplicit"), settings);
    }

    [Fact]
    public Task DerivedClassExternalBase()
    {
        return Verify(Decompile("ExternalBase.DerivedClass"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceExternalBase()
    {
        return Verify(Decompile("ExternalBase.ImplementsInterface"), settings);
    }

    [Fact]
    public Task ImplementsInheritedInterfaceExternalBase()
    {
        return Verify(Decompile("ExternalBase.ImplementsInheritedInterface"), settings);
    }

    [Fact]
    public Task ImplementsInterfaceExplicitExternalBase()
    {
        return Verify(Decompile("ExternalBase.ImplementsInterfaceExplicit"), settings);
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

    private string Decompile(string item)
    {
        return Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, item);
    }
}