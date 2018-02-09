using System.Linq;
using ApprovalTests;
using Fody;
using ObjectApproval;
using Xunit;

public class ApprovedTests
{
    [Fact]
    public void ClassWithBadAttributes()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithBadAttributes"));
    }

    [Fact]
    public void ClassWithPrivateMethod()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithPrivateMethod"));
    }

    [Fact]
    public void ClassWithPrivateMethodNoAssert()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithPrivateMethod"));
    }

    [Fact]
    public void GenericClass()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "GenericClass`1"));
    }

    [Fact]
    public void Indexers()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "Indexers"));
    }

    [Fact]
    public void InterfaceBadAttributes()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "InterfaceBadAttributes"));
    }

    [Fact]
    public void SimpleClass()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "SimpleClass"));
    }

    [Fact]
    public void SimpleClassNoAssert()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "SimpleClass"));
    }

    [Fact]
    public void SkipIXamlMetadataProvider()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "XamlMetadataProvider"));
    }

    [Fact]
    public void SpecialClass()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "SpecialClass"));
    }

    [Fact]
    public void PublicNestedInsideNonPublic()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "NonPublicWithNested"));
    }

    [Fact]
    public void UnsafeClass()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "UnsafeClass"));
    }

    [Fact]
    public void ClassWithImplicitInterface()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithImplicitInterface"));
    }

    [Fact]
    public void ClassWithExplicitInterface()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithExplicitInterface"));
    }

    [Fact]
    public void InfosList()
    {
        ObjectApprover.VerifyWithJson(AssemblyWeaver.TestResult.Messages.Select(x=>x.Text));
    }

    [Fact]
    public void WarnsList()
    {
        ObjectApprover.VerifyWithJson(AssemblyWeaver.TestResult.Warnings.Select(x=>x.Text));
    }

    [Fact]
    public void ErrorsList()
    {
        ObjectApprover.VerifyWithJson(AssemblyWeaver.TestResult.Errors.Select(x=>x.Text));
    }
}