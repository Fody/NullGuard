using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NullGuard;

public class SpecialClass
{
	public IEnumerable<int> CountTo(int end)
	{
		for (var i = 0; i < end; i++)
		{
			yield return i;
		}
	}

#if (DEBUG)
	public async Task SomeMethodAsync(string nonNullArg, [AllowNull] string nullArg)
	{
		await Task.Run(() => Console.WriteLine(nonNullArg));
	}

	public async Task<string> MethodWithReturnValueAsync(bool returnNull)
	{
		return await Task.Run<string>(() => returnNull ? null : "");
	}
#endif
}