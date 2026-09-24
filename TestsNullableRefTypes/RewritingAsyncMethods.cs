using System;
using System.Threading.Tasks;

public class RewritingAsyncMethods
{
    [Test]
    public async Task RequiresNonNullConcreteTypeAsync()
    {
        await Assert.That(ClassWithAsyncMethods.GetNonNullAsync).Throws<InvalidOperationException>();
    }

    [Test]
    public async Task AllowsNullConcreteTypeAsync()
    {
        var result = await ClassWithAsyncMethods.GetNullAsync();
        await Assert.That((object)result).IsNull();
    }

    [Test]
    public async Task RequiresNonNullGenericTypeAsyncWithDelay()
    {
        await Assert.That(ClassWithAsyncMethods.GetNonNullAsyncWithDelay<string>).Throws<InvalidOperationException>();
    }

    [Test]
    public async Task AllowsMaybeNullGenericTypeAsync()
    {
        var result = await ClassWithAsyncMethods.GetMaybeNullAsync<string>();
        await Assert.That((object)result).IsNull();
    }

    [Test]
    public async Task AllowsNullGenericTypeAsyncWithDelay()
    {
        var result = await ClassWithAsyncMethods.GetNullAsyncWithDelay<string>();
        await Assert.That((object)result).IsNull();
    }

    [Test]
    public async Task AllowsNullGenericTypeAsyncWithDelay2()
    {
        var result = await ClassWithAsyncMethods.GetNullAsyncWithDelay2<string>();
        await Assert.That((object)result).IsNull();
    }

    [Test]
    public async Task AllowsNullTask()
    {
        var result = ClassWithAsyncMethods.GetNullTask();
        await Assert.That((object)result).IsNull();
    }

    [Test]
    public async Task RequiresNonNullTask()
    {
        await Assert.That(async () => await ClassWithAsyncMethods.GetNonNullTask()).Throws<InvalidOperationException>();
    }
}
