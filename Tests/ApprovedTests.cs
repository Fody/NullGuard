#if NETFRAMEWORK

using VerifyTUnit;
using System.Linq;
using System.Threading.Tasks;
using VerifyTests.ICSharpCode.Decompiler;

public class ApprovedTests
{
    [Test]
    public async Task ClassWithBadAttributes()
    {
        await Verifier.Verify(GetType("ClassWithBadAttributes"));
    }

    [Test]
    public async Task ClassWithPrivateMethod()
    {
        await Verifier.Verify(GetType("ClassWithPrivateMethod"));
    }

    [Test]
    public async Task ClassWithPrivateMethodNoAssert()
    {
        await Verifier.Verify(GetType("ClassWithPrivateMethod"));
    }

#if DEBUG
    [Test]
    public async Task GenericClass()
    {
        await Verifier.Verify(GetType("GenericClass`1"));
    }
#endif

    [Test]
    public async Task GenericClassWithValueTypeConstraint()
    {
        await Verifier.Verify(GetType("GenericClassWithValueTypeConstraints`1"));
    }

    [Test]
    public async Task GenericClassWithReferenceTypeConstraints()
    {
        await Verifier.Verify(GetType("GenericClassWithReferenceTypeConstraints`1"));
    }

    [Test]
    public async Task Indexers()
    {
        await Verifier.Verify(GetType("Indexers"));
    }

    [Test]
    public async Task InterfaceBadAttributes()
    {
        await Verifier.Verify(GetType("InterfaceBadAttributes"));
    }

    [Test]
    public async Task SimpleClass()
    {
        await Verifier.Verify(GetType("SimpleClass"));
    }

    [Test]
    public async Task SimpleClassNoAssert()
    {
        await Verifier.Verify(GetType("SimpleClass"));
    }

    [Test]
    public async Task SkipIXamlMetadataProvider()
    {
        await Verifier.Verify(GetType("XamlMetadataProvider"));
    }

#if (DEBUG)
    [Test]
    public async Task SpecialClass_debug()
    {
        await Verifier.Verify(GetType("SpecialClass"));
    }
#else
    [Test]
    public async Task SpecialClass_release()
    {
        await Verifier.Verify(GetType("SpecialClass"));
    }
#endif

    [Test]
    public async Task PublicNestedInsideNonPublic()
    {
        await Verifier.Verify(GetType("NonPublicWithNested"));
    }

    [Test]
    public async Task UnsafeClass()
    {
        await Verifier.Verify(GetType("UnsafeClass"));
    }

    [Test]
    public async Task ClassWithImplicitInterface()
    {
        await Verifier.Verify(GetType("ClassWithImplicitInterface"));
    }

    [Test]
    public async Task ClassWithExplicitInterface()
    {
        await Verifier.Verify(GetType("ClassWithExplicitInterface"));
    }

    [Test]
    public async Task InfosList()
    {
        await Verifier.Verify(AssemblyWeaver.TestResult.Messages.Select(x=>x.Text));
    }

    [Test]
    public async Task WarnsList()
    {
        await Verifier.Verify(AssemblyWeaver.TestResult.Warnings.Select(x=>x.Text));
    }

    [Test]
    public async Task ErrorsList()
    {
        await Verifier.Verify(AssemblyWeaver.TestResult.Errors.Select(x=>x.Text));
    }

    private static TypeToDisassemble GetType(string typeName)
    {
        return new(AssemblyWeaver.PeFile, typeName);
    }
}

#endif