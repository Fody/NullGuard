using System.Threading.Tasks;
using DiffEngine;
using ICSharpCode.Decompiler.Metadata;
using VerifyTests;
using VerifyTests.ICSharpCode.Decompiler;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class ApprovedTests
{
    [Fact]
    public Task ClassWithNullableContext1()
    {
        return Verifier.Verify(Decompile<ClassWithNullableContext1>(), uniqueForRuntime);
    }

    [Fact]
    public Task ClassWithNullableContext2()
    {
        return Verifier.Verify(Decompile<ClassWithNullableContext2>(), uniqueForRuntime);
    }

    [Fact]
    public Task ClassWithNullableReferenceMethod()
    {
        return Verifier.Verify(Decompile<ClassWithNullableReferenceMethod>());
    }

    [Fact]
    public Task ClassWithGenericNestedClass()
    {
        return Verifier.Verify(Decompile<ClassWithGenericNestedClass>(), uniqueForRuntime);
    }

    [Fact]
    public Task ClassWithAsyncMethods()
    {
        return Verifier.Verify(Decompile<ClassWithAsyncMethods>());
    }

    [Fact]
    public Task ClassWithRefReturns()
    {
        return Verifier.Verify(Decompile<ClassWithRefReturns>(), uniqueForRuntime);
    }

    static VerifySettings settings;
    static VerifySettings uniqueForRuntime;

    static ApprovedTests()
    {
        VerifyICSharpCodeDecompiler.Enable();
        DiffRunner.MaxInstancesToLaunch(100);

        settings = new VerifySettings();
        settings.AddScrubber(v => v.Replace("[netstandard]", "[mscorlib]"));

        uniqueForRuntime = new VerifySettings(settings);
        uniqueForRuntime.UniqueForRuntime();
    }

    private TypeToDisassemble Decompile<T>()
    {
        return new TypeToDisassemble(new PEFile(typeof(T).Assembly.Location), typeof(T).Name);
    }
}