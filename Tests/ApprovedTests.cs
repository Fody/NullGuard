#if (NET472)
using Xunit.Abstractions;
using VerifyXunit;
using Fody;
using Xunit;
using System.Linq;
using System.Threading.Tasks;

public class ApprovedTests:
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
    public Task GenericClassWithValueTypeConstraint()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "GenericClassWithValueTypeConstraints`1"));
    }

    [Fact]
    public Task GenericClassWithReferenceTypeConstraints()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "GenericClassWithReferenceTypeConstraints`1"));
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

#if (DEBUG)
    [Fact]
    public Task SpecialClass_debug()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "SpecialClass"));
    }
#else
    [Fact]
    public Task SpecialClass_release()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "SpecialClass"));
    }
#endif

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
    public Task ClassWithImplicitInterface()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithImplicitInterface"));
    }

    [Fact]
    public Task ClassWithExplicitInterface()
    {
        return Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithExplicitInterface"));
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
    }
}

#endif