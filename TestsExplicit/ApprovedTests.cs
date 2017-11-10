#if(DEBUG)

using System;
using System.Linq;
using ApprovalTests;
using NUnit.Framework;

[TestFixture]
public class ApprovedTests
{
    [Test]
    public void ClassWithBadAttributes()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithBadAttributes"));
    }

    [Test]
    public void ClassWithPrivateMethod()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithPrivateMethod"));
    }

    [Test]
    public void ClassWithPrivateMethodNoAssert()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "ClassWithPrivateMethod"));
    }

    [Test]
    public void GenericClass()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "GenericClass`1"));
    }

    [Test]
    public void Indexers()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "Indexers"));
    }

    [Test]
    public void InterfaceBadAttributes()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "InterfaceBadAttributes"));
    }

    [Test]
    public void SimpleClass()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "SimpleClass"));
    }

    [Test]
    public void SimpleClassNoAssert()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "SimpleClass"));
    }

    [Test]
    public void SkipIXamlMetadataProvider()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "XamlMetadataProvider"));
    }

    [Test]
    public void SpecialClass()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "SpecialClass"));
    }

    [Test]
    public void PublicNestedInsideNonPublic()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "NonPublicWithNested"));
    }

    [Test]
    public void UnsafeClass()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "UnsafeClass"));
    }

    [Test]
    public void DerivedClass()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "InternalBase.DerivedClass"), v => v.Replace("InternalBase.", string.Empty));
    }

    [Test]
    public void ImplementsInterface()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "InternalBase.ImplementsInterface"), v => v.Replace("InternalBase.", string.Empty));
    }

    [Test]
    public void ImplementsInheritedInterface()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "InternalBase.ImplementsInheritedInterface"), v => v.Replace("InternalBase.", string.Empty));
    }

    [Test]
    public void ImplementsInterfaceExplicit()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "InternalBase.ImplementsInterfaceExplicit"), v => v.Replace("InternalBase.", string.Empty));
    }

    [Test]
    public void DerivedClassAssemblyBase()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "AssemblyBase.DerivedClass"), v => v.Replace("AssemblyBase.", string.Empty));
    }

    [Test]
    public void ImplementsInterfaceAssemblyBase()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "AssemblyBase.ImplementsInterface"), v => v.Replace("AssemblyBase.", string.Empty));
    }

    [Test]
    public void ImplementsInheritedInterfaceAssemblyBase()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "AssemblyBase.ImplementsInheritedInterface"), v => v.Replace("AssemblyBase.", string.Empty));
    }

    [Test]
    public void ImplementsInterfaceExplicitAssemblyBase()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "AssemblyBase.ImplementsInterfaceExplicit"), v => v.Replace("AssemblyBase.", string.Empty));
    }

    [Test]
    public void DerivedClassExternalBase()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "ExternalBase.DerivedClass"), v => v.Replace("ExternalBase.", string.Empty));
    }

    [Test]
    public void ImplementsInterfaceExternalBase()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "ExternalBase.ImplementsInterface"), v => v.Replace("ExternalBase.", string.Empty));
    }

    [Test]
    public void ImplementsInheritedInterfaceExternalBase()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "ExternalBase.ImplementsInheritedInterface"), v => v.Replace("ExternalBase.", string.Empty));
    }

    [Test]
    public void ImplementsInterfaceExplicitExternalBase()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPath, "ExternalBase.ImplementsInterfaceExplicit"), v => v.Replace("ExternalBase.", string.Empty));
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