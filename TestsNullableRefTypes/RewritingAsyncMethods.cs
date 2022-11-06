using System;
using System.Threading.Tasks;
using Xunit;

public class RewritingAsyncMethods
{
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
    public async Task RequiresNonNullTask()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await ClassWithAsyncMethods.GetNonNullTask());
    }
}
