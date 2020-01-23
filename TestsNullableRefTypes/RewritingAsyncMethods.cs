using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;
using Xunit.Abstractions;

public class RewritingAsyncMethods : VerifyBase
{
    public RewritingAsyncMethods(ITestOutputHelper output) :
         base(output)
    {
    }

    [Fact]
    public async void RequiresNonNullConcreteTypeAsync()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(() => ClassWithAsyncMethods.GetNonNullAsync());
    }

    [Fact]
    public async void AllowsNullConcreteTypeAsync()
    {
        var result = await ClassWithAsyncMethods.GetNullAsync();
        Assert.Null(result);
    }

    [Fact]
    public async void RequiresNonNullGenericTypeAsyncWithDelay()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(() => ClassWithAsyncMethods.GetNonNullAsyncWithDelay<string>());
    }

    [Fact]
    public async void AllowsMaybeNullGenericTypeAsync()
    {
        var result = await ClassWithAsyncMethods.GetMaybeNullAsync<string>();
        Assert.Null(result);
    }

    [Fact]
    public async void AllowsNullGenericTypeAsyncWithDelay()
    {
        var result = await ClassWithAsyncMethods.GetNullAsyncWithDelay<string>();
        Assert.Null(result);
    }

    [Fact]
    public async void AllowsNullGenericTypeAsyncWithDelay2()
    {
        var result = await ClassWithAsyncMethods.GetNullAsyncWithDelay2<string>();
        Assert.Null(result);
    }

    [Fact]
    public void AllowsNullTask()
    {
        var result = ClassWithAsyncMethods.GetNullTask();
        Assert.Null(result);
    }

    [Fact]
    public void RequiresNonNullTask()
    {
        Assert.Throws<InvalidOperationException>(new Action(() => ClassWithAsyncMethods.GetNonNullTask()));
    }
}
