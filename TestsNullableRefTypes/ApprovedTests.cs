using System.Threading.Tasks;
using Fody;
using VerifyTests;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class ApprovedTests
{
    static VerifySettings uniqueForRuntime;

    static ApprovedTests()
    {
        uniqueForRuntime = new VerifySettings();
        uniqueForRuntime.UniqueForRuntime();
    }

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

    string Decompile<T>()
    {
        return Ildasm.Decompile(typeof(T).Assembly.Location, typeof(T).Name)
            .Replace("[netstandard]", "[mscorlib]");
    }
}