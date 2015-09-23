using System.Threading.Tasks;
using NullGuard;

[InjectJetBrainsAnnotations(null, null)]
public class SampleClassWithDisabledInjectJetBrainsAnnotations
{
    public string SomeMethod(string nonNullArg)
    {
        return null;
    }

    public async Task<string> SomeAsyncMethod ()
    {
        await Task.Delay(0);
        return null;
    }

    public string SomeProperty { get; set; }
}