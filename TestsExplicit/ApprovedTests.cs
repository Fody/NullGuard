using System.Linq;
using ApprovalTests;
using Xunit;
using Fody;
using ObjectApproval;

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
    public void DerivedClass()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "InternalBase.DerivedClass"), v => v.Replace("InternalBase.", string.Empty));
    }

    [Fact]
    public void ImplementsInterface()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "InternalBase.ImplementsInterface"), v => v.Replace("InternalBase.", string.Empty));
    }

    [Fact]
    public void ImplementsInheritedInterface()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "InternalBase.ImplementsInheritedInterface"), v => v.Replace("InternalBase.", string.Empty));
    }

    [Fact]
    public void ImplementsInterfaceExplicit()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "InternalBase.ImplementsInterfaceExplicit"), v => v.Replace("InternalBase.", string.Empty));
    }

    [Fact]
    public void DerivedClassAssemblyBase()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "AssemblyBase.DerivedClass"), v => v.Replace("AssemblyBase.", string.Empty));
    }

    [Fact]
    public void ImplementsInterfaceAssemblyBase()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "AssemblyBase.ImplementsInterface"), v => v.Replace("AssemblyBase.", string.Empty));
    }

    [Fact]
    public void ImplementsInheritedInterfaceAssemblyBase()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "AssemblyBase.ImplementsInheritedInterface"), v => v.Replace("AssemblyBase.", string.Empty));
    }

    [Fact]
    public void ImplementsInterfaceExplicitAssemblyBase()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "AssemblyBase.ImplementsInterfaceExplicit"), v => v.Replace("AssemblyBase.", string.Empty));
    }

    [Fact]
    public void DerivedClassExternalBase()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ExternalBase.DerivedClass"), v => v.Replace("ExternalBase.", string.Empty));
    }

    [Fact]
    public void ImplementsInterfaceExternalBase()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ExternalBase.ImplementsInterface"), v => v.Replace("ExternalBase.", string.Empty));
    }

    [Fact]
    public void ImplementsInheritedInterfaceExternalBase()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ExternalBase.ImplementsInheritedInterface"), v => v.Replace("ExternalBase.", string.Empty));
    }

    [Fact]
    public void ImplementsInterfaceExplicitExternalBase()
    {
        Approvals.Verify(Ildasm.Decompile(AssemblyWeaver.AfterAssemblyPath, "ExternalBase.ImplementsInterfaceExplicit"), v => v.Replace("ExternalBase.", string.Empty));
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