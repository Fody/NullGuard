#if (!xNET472)
using System.Threading.Tasks;

using Fody;

using VerifyXunit;

using Xunit;
using Xunit.Abstractions;

public class ApprovedTests:
    VerifyBase
{
    public ApprovedTests(ITestOutputHelper output) :
        base(output)
    {
    }

    [Fact]
    public Task ClassWithNullableContext1()
    {
        return Verify(Decompile<ClassWithNullableContext1>());
    }

    [Fact]
    public Task ClassWithNullableContext2()
    {
        return Verify(Decompile<ClassWithNullableContext2>());
    }

    [Fact]
    public Task ClassWithNullableReferenceMethod()
    {
        return Verify(Decompile<ClassWithNullableReferenceMethod>());
    }

    [Fact]
    public Task ClassWithGenericNestedClass()
    {
        return Verify(Decompile<ClassWithGenericNestedClass>());
    }

    [Fact]
    public Task ClassWithAsyncMethods()
    {
        return Verify(Decompile<ClassWithAsyncMethods>());
    }

    string Decompile<T>()
    {
        return Ildasm.Decompile(typeof(T).Assembly.Location, typeof(T).Name)
            .Replace("[netstandard]", "[mscorlib]");
    }
}

#endif