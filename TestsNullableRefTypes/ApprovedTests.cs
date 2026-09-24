using System.Threading.Tasks;
using DiffEngine;
using VerifyTests;
using VerifyTests.ICSharpCode.Decompiler;
using VerifyTUnit;

public class ApprovedTests
{
    [Test]
    public async Task ClassWithNullableContext1()
    {
        await Verifier.Verify(GetType<ClassWithNullableContext1>(), uniqueForRuntime);
    }

    [Test]
    public async Task ClassWithNullableContext2()
    {
        await Verifier.Verify(GetType<ClassWithNullableContext2>(), uniqueForRuntime);
    }

    [Test]
    public async Task ClassWithNullableReferenceMethod()
    {
        await Verifier.Verify(GetType<ClassWithNullableReferenceMethod>());
    }

    [Test]
    public async Task ClassWithGenericNestedClass()
    {
        await Verifier.Verify(GetType<ClassWithGenericNestedClass>(), uniqueForRuntime);
    }

    [Test]
    public async Task ClassWithAsyncMethods()
    {
        await Verifier.Verify(GetType<ClassWithAsyncMethods>());
    }

    [Test]
    public async Task ClassWithRefReturns()
    {
        await Verifier.Verify(GetType<ClassWithRefReturns>(), uniqueForRuntime);
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

    static TypeToDisassemble GetType<T>()
    {
        return new(new(typeof(T).Assembly.Location), typeof(T).Name);
    }
}