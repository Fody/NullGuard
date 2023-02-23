using System.Reflection;
using System.Threading.Tasks;
using DiffEngine;
using ICSharpCode.Decompiler.Metadata;
using TestsCommon;
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
        return Verifier.Verify(GetType<ClassWithNullableContext1>(), uniqueForRuntime);
    }

    [Fact]
    public Task ClassWithNullableContext2()
    {
        return Verifier.Verify(GetType<ClassWithNullableContext2>(), uniqueForRuntime);
    }

    [Fact]
    public Task ClassWithNullableReferenceMethod()
    {
        return Verifier.Verify(GetType<ClassWithNullableReferenceMethod>());
    }

    [Fact]
    public Task ClassWithGenericNestedClass()
    {
        return Verifier.Verify(GetType<ClassWithGenericNestedClass>(), uniqueForRuntime);
    }

    [Fact]
    public Task ClassWithAsyncMethods()
    {
        return Verifier.Verify(GetType<ClassWithAsyncMethods>());
    }

    [Fact]
    public Task ClassWithRefReturns()
    {
        return Verifier.Verify(GetType<ClassWithRefReturns>(), uniqueForRuntime);
    }

    static VerifySettings uniqueForRuntime;

    static ApprovedTests()
    {
        VerifyICSharpCodeDecompiler.Initialize();
        DiffRunner.MaxInstancesToLaunch(100);

        var settings = new VerifySettings();
        settings.UniqueForRuntime();
        settings.AddScrubber(v => v.Replace("[netstandard]", "[mscorlib]"));

        uniqueForRuntime = settings;
    }

    private static TypeToDisassemble GetType<T>()
    {
        return new TypeToDisassemble(new PEFile(typeof(T).Assembly.Location), typeof(T).Name);
    }
}