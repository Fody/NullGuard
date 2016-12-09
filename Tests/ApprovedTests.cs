#if(DEBUG)

using System.Linq;
using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using NUnit.Framework;

[TestFixture]
[UseReporter(typeof(DiffReporter), typeof(ClipboardReporter))]
[UseApprovalSubdirectory("approvals")]
public class ApprovedTests
{
    [Test]
    public void ClassWithBadAttributes()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.Assemblies[0].Location, "ClassWithBadAttributes"));
    }

    [Test]
    public void ClassWithPrivateMethod()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.Assemblies[0].Location, "ClassWithPrivateMethod"));
    }

    [Test]
    public void ClassWithPrivateMethodNoAssert()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.Assemblies[1].Location, "ClassWithPrivateMethod"));
    }

    [Test]
    public void GenericClass()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.Assemblies[0].Location, "GenericClass`1"));
    }

    [Test]
    public void GenericClassNoAssert()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.Assemblies[1].Location, "GenericClass`1"));
    }

    [Test]
    public void Indexers()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.Assemblies[0].Location, "Indexers"));
    }

    [Test]
    public void InterfaceBadAttributes()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.Assemblies[0].Location, "InterfaceBadAttributes"));
    }

    [Test]
    public void SimpleClass()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.Assemblies[0].Location, "SimpleClass"));
    }

    [Test]
    public void SimpleClassNoAssert()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.Assemblies[1].Location, "SimpleClass"));
    }

    [Test]
    public void SkipIXamlMetadataProvider()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.Assemblies[0].Location, "XamlMetadataProvider"));
    }

    [Test]
    public void SpecialClass()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.Assemblies[0].Location, "SpecialClass"));
    }

    [Test]
    public void SpecialClassNoAssert()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.Assemblies[1].Location, "SpecialClass"));
    }

    [Test]
    public void PublicNestedInsideNonPublic()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.Assemblies[0].Location, "NonPublicWithNested"));
    }

    [Test]
    public void UnsafeClass()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.Assemblies[0].Location, "UnsafeClass"));
    }

    [Test]
    public void InfosList()
    {
        Approvals.VerifyAll(AssemblyWeaver.Infos.OrderBy(e => e), "Infos: ");
    }

    [Test]
    public void WarnsList()
    {
        Approvals.VerifyAll(AssemblyWeaver.Warns.OrderBy(e => e), "Warns: ");
    }

    [Test]
    public void ErrorsList()
    {
        Approvals.VerifyAll(AssemblyWeaver.Errors.OrderBy(e => e), "Errors: ");
    }
}

#endif