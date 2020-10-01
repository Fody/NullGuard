#if (NET472)
using VerifyXunit;
using Fody;
using Xunit;
using System.Linq;
using System.Threading.Tasks;

[UsesVerify]
public class ApprovedTests
{
    [Fact]
    public Task ClassWithBadAttributes()
    {
        return Verifier.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithBadAttributes"));
    }

    [Fact]
    public Task ClassWithPrivateMethod()
    {
        return Verifier.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithPrivateMethod"));
    }

    [Fact]
    public Task ClassWithPrivateMethodNoAssert()
    {
        return Verifier.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithPrivateMethod"));
    }

#if DEBUG
    [Fact]
    public Task GenericClass()
    {
        return Verifier.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "GenericClass`1"));
    }
#endif

    [Fact]
    public Task GenericClassWithValueTypeConstraint()
    {
        return Verifier.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "GenericClassWithValueTypeConstraints`1"));
    }

    [Fact]
    public Task GenericClassWithReferenceTypeConstraints()
    {
        return Verifier.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "GenericClassWithReferenceTypeConstraints`1"));
    }

    [Fact]
    public Task Indexers()
    {
        return Verifier.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "Indexers"));
    }

    [Fact]
    public Task InterfaceBadAttributes()
    {
        return Verifier.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "InterfaceBadAttributes"));
    }

    [Fact]
    public Task SimpleClass()
    {
        return Verifier.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "SimpleClass"));
    }

    [Fact]
    public Task SimpleClassNoAssert()
    {
        return Verifier.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "SimpleClass"));
    }

    [Fact]
    public Task SkipIXamlMetadataProvider()
    {
        return Verifier.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "XamlMetadataProvider"));
    }

#if (DEBUG)
    [Fact]
    public Task SpecialClass_debug()
    {
        return Verifier.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "SpecialClass"));
    }
#else
    [Fact]
    public Task SpecialClass_release()
    {
        return Verifier.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "SpecialClass"));
    }
#endif

    [Fact]
    public Task PublicNestedInsideNonPublic()
    {
        return Verifier.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "NonPublicWithNested"));
    }

    [Fact]
    public Task UnsafeClass()
    {
        return Verifier.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "UnsafeClass"));
    }

    [Fact]
    public Task ClassWithImplicitInterface()
    {
        return Verifier.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithImplicitInterface"));
    }

    [Fact]
    public Task ClassWithExplicitInterface()
    {
        return Verifier.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithExplicitInterface"));
    }

    [Fact]
    public Task InfosList()
    {
        return Verifier.Verify(AssemblyWeaver.TestResult.Messages.Select(x=>x.Text));
    }

    [Fact]
    public Task WarnsList()
    {
        return Verifier.Verify(AssemblyWeaver.TestResult.Warnings.Select(x=>x.Text));
    }

    [Fact]
    public Task ErrorsList()
    {
        return Verifier.Verify(AssemblyWeaver.TestResult.Errors.Select(x=>x.Text));
    }
}

#endif