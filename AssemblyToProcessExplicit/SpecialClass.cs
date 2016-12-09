using System.Collections.Generic;
#if (DEBUG)
using System;
using System.Threading.Tasks;
using NullGuard;
#endif

public class SpecialClass
{
    [NotNull]
    public IEnumerable<int> CountTo(int end)
    {
        for (var i = 0; i < end; i++)
        {
            yield return i;
        }
    }

#if (DEBUG)

    [NotNull]
    public async Task SomeMethodAsync([NotNull] string nonNullArg, string nullArg)
    {
        await Task.Run(() => Console.WriteLine(nonNullArg));
    }

    [NotNull]
    public async Task<string> MethodWithReturnValueAsync(bool returnNull)
    {
        return await Task.Run(() => returnNull ? null : "");
    }

    public async Task<string> MethodAllowsNullReturnValueAsync()
    {
        await Task.Delay(100);

        return null;
    }

#pragma warning disable 1998

    [NotNull]
    public async Task<int> NoAwaitCode()
    {
        return 42;
    }

#pragma warning restore 1998
#endif
}