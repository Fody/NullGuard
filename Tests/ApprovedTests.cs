#if(DEBUG)

using System.Linq;
using System.Runtime.CompilerServices;
using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Namers.StackTraceParsers;
using ApprovalTests.Reporters;
using Tests.Helpers;
using Xunit;

[UseReporter(typeof(DiffReporter), typeof(ClipboardReporter))]
[UseApprovalSubdirectory("approvals")]
public class ApprovedTests
{
    static ApprovedTests()
    {
        StackTraceParser.AddParser(new SkippableNamer());
    }

    [SkippableFact]
    public void ClassWithBadAttributes()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "ClassWithBadAttributes"));
    }

    [SkippableFact]
    public void ClassWithPrivateMethod()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "ClassWithPrivateMethod"));
    }

    [SkippableFact]
    public void ClassWithPrivateMethodNoAssert()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPaths[1], "ClassWithPrivateMethod"));
    }

    [SkippableFact]
    public void GenericClass()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "GenericClass`1"));
    }

    [SkippableFact]
    public void GenericClassNoAssert()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPaths[1], "GenericClass`1"));
    }

    [SkippableFact]
    public void Indexers()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "Indexers"));
    }

    [SkippableFact]
    public void InterfaceBadAttributes()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "InterfaceBadAttributes"));
    }

    [SkippableFact]
    public void SimpleClass()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "SimpleClass"));
    }

    [SkippableFact]
    public void SimpleClassNoAssert()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPaths[1], "SimpleClass"));
    }

    [SkippableFact]
    public void SkipIXamlMetadataProvider()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "XamlMetadataProvider"));
    }

    [SkippableFact]
    public void SpecialClass()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "SpecialClass"));
    }

    [SkippableFact]
    public void SpecialClassNoAssert()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPaths[1], "SpecialClass"));
    }

    [SkippableFact]
    public void PublicNestedInsideNonPublic()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "NonPublicWithNested"));
    }

    [SkippableFact]
    public void UnsafeClass()
    {
        Approvals.Verify(Decompiler.Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "UnsafeClass"));
    }

    [Fact]
    public void InfosList()
    {
        Approvals.VerifyAll(AssemblyWeaver.Infos.OrderBy(e => e), "Infos: ");
    }

    [Fact]
    public void WarnsList()
    {
        Approvals.VerifyAll(AssemblyWeaver.Warns.OrderBy(e => e), "Warns: ");
    }

    [Fact]
    public void ErrorsList()
    {
        Approvals.VerifyAll(AssemblyWeaver.Errors.OrderBy(e => e), "Errors: ");
    }
}

#endif