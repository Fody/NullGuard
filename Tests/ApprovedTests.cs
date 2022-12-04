#if NETFRAMEWORK

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
        return Verifier.Verify(Decompile("ClassWithBadAttributes"));
    }

    [Fact]
    public Task ClassWithPrivateMethod()
    {
        return Verifier.Verify(Decompile("ClassWithPrivateMethod"));
    }

    [Fact]
    public Task ClassWithPrivateMethodNoAssert()
    {
        return Verifier.Verify(Decompile("ClassWithPrivateMethod"));
    }

#if DEBUG
    [Fact]
    public Task GenericClass()
    {
        return Verifier.Verify(Decompile("GenericClass`1"));
    }
#endif

    [Fact]
    public Task GenericClassWithValueTypeConstraint()
    {
        return Verifier.Verify(Decompile("GenericClassWithValueTypeConstraints`1"));
    }

    [Fact]
    public Task GenericClassWithReferenceTypeConstraints()
    {
        return Verifier.Verify(Decompile("GenericClassWithReferenceTypeConstraints`1"));
    }

    [Fact]
    public Task Indexers()
    {
        return Verifier.Verify(Decompile("Indexers"));
    }

    [Fact]
    public Task InterfaceBadAttributes()
    {
        return Verifier.Verify(Decompile("InterfaceBadAttributes"));
    }

    [Fact]
    public Task SimpleClass()
    {
        return Verifier.Verify(Decompile("SimpleClass"));
    }

    [Fact]
    public Task SimpleClassNoAssert()
    {
        return Verifier.Verify(Decompile("SimpleClass"));
    }

    [Fact]
    public Task SkipIXamlMetadataProvider()
    {
        return Verifier.Verify(Decompile("XamlMetadataProvider"));
    }

#if (DEBUG)
    [Fact]
    public Task SpecialClass_debug()
    {
        return Verifier.Verify(Decompile("SpecialClass"));
    }
#else
    [Fact]
    public Task SpecialClass_release()
    {
        return Verifier.Verify(Decompile("SpecialClass"));
    }
#endif

    [Fact]
    public Task PublicNestedInsideNonPublic()
    {
        return Verifier.Verify(Decompile("NonPublicWithNested"));
    }

    [Fact]
    public Task UnsafeClass()
    {
        return Verifier.Verify(Decompile("UnsafeClass"));
    }

    [Fact]
    public Task ClassWithImplicitInterface()
    {
        return Verifier.Verify(Decompile("ClassWithImplicitInterface"));
    }

    [Fact]
    public Task ClassWithExplicitInterface()
    {
        return Verifier.Verify(Decompile("ClassWithExplicitInterface"));
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

    private string Decompile(string item)
    {
        return Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, item);
    }
}

#endif