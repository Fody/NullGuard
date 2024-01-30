#if NETFRAMEWORK

using VerifyXunit;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using VerifyTests.ICSharpCode.Decompiler;

public class ApprovedTests
{
    [Fact]
    public Task ClassWithBadAttributes()
    {
        return Verifier.Verify(GetType("ClassWithBadAttributes"));
    }

    [Fact]
    public Task ClassWithPrivateMethod()
    {
        return Verifier.Verify(GetType("ClassWithPrivateMethod"));
    }

    [Fact]
    public Task ClassWithPrivateMethodNoAssert()
    {
        return Verifier.Verify(GetType("ClassWithPrivateMethod"));
    }

#if DEBUG
    [Fact]
    public Task GenericClass()
    {
        return Verifier.Verify(GetType("GenericClass`1"));
    }
#endif

    [Fact]
    public Task GenericClassWithValueTypeConstraint()
    {
        return Verifier.Verify(GetType("GenericClassWithValueTypeConstraints`1"));
    }

    [Fact]
    public Task GenericClassWithReferenceTypeConstraints()
    {
        return Verifier.Verify(GetType("GenericClassWithReferenceTypeConstraints`1"));
    }

    [Fact]
    public Task Indexers()
    {
        return Verifier.Verify(GetType("Indexers"));
    }

    [Fact]
    public Task InterfaceBadAttributes()
    {
        return Verifier.Verify(GetType("InterfaceBadAttributes"));
    }

    [Fact]
    public Task SimpleClass()
    {
        return Verifier.Verify(GetType("SimpleClass"));
    }

    [Fact]
    public Task SimpleClassNoAssert()
    {
        return Verifier.Verify(GetType("SimpleClass"));
    }

    [Fact]
    public Task SkipIXamlMetadataProvider()
    {
        return Verifier.Verify(GetType("XamlMetadataProvider"));
    }

#if (DEBUG)
    [Fact]
    public Task SpecialClass_debug()
    {
        return Verifier.Verify(GetType("SpecialClass"));
    }
#else
    [Fact]
    public Task SpecialClass_release()
    {
        return Verifier.Verify(GetType("SpecialClass"));
    }
#endif

    [Fact]
    public Task PublicNestedInsideNonPublic()
    {
        return Verifier.Verify(GetType("NonPublicWithNested"));
    }

    [Fact]
    public Task UnsafeClass()
    {
        return Verifier.Verify(GetType("UnsafeClass"));
    }

    [Fact]
    public Task ClassWithImplicitInterface()
    {
        return Verifier.Verify(GetType("ClassWithImplicitInterface"));
    }

    [Fact]
    public Task ClassWithExplicitInterface()
    {
        return Verifier.Verify(GetType("ClassWithExplicitInterface"));
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

    private static TypeToDisassemble GetType(string typeName)
    {
        return new(AssemblyWeaver.PeFile, typeName);
    }
}

#endif