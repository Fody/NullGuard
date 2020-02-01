using System.Threading.Tasks;

using Fody;

using Verify;

using VerifyXunit;

using Xunit;
using Xunit.Abstractions;

public class ApprovedTests :
    VerifyBase
{
    private static readonly VerifySettings _uniqueForRuntime;

    static ApprovedTests()
    {
        _uniqueForRuntime = new VerifySettings();
        _uniqueForRuntime.UniqueForRuntime();
    }

    public ApprovedTests(ITestOutputHelper output) :
        base(output)
    {
    }

    [Fact]
    public Task ClassWithNullableContext1()
    {
        return Verify(Decompile<ClassWithNullableContext1>(), _uniqueForRuntime);
    }

    [Fact]
    public Task ClassWithNullableContext2()
    {
        return Verify(Decompile<ClassWithNullableContext2>(), _uniqueForRuntime);
    }

    [Fact]
    public Task ClassWithNullableReferenceMethod()
    {
        return Verify(Decompile<ClassWithNullableReferenceMethod>());
    }

    [Fact]
    public Task ClassWithGenericNestedClass()
    {
        return Verify(Decompile<ClassWithGenericNestedClass>(), _uniqueForRuntime);
    }

    [Fact]
    public Task ClassWithAsyncMethods()
    {
        return Verify(Decompile<ClassWithAsyncMethods>());
    }

    [Fact]
    public Task ClassWithRefReturns()
    {
        return Verify(Decompile<ClassWithRefReturns>(), _uniqueForRuntime);
    }

    string Decompile<T>()
    {
        return Ildasm.Decompile(typeof(T).Assembly.Location, typeof(T).Name)
            .Replace("[netstandard]", "[mscorlib]");
    }
}