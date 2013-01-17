
#if(DEBUG)

using NUnit.Framework;

[TestFixture]
public class VerifyTest
{

    [Test]
    public void PeVerify()
    {
        Verifier.Verify(AssemblyWeaver.BeforeAssemblyPath, AssemblyWeaver.AfterAssemblyPath);
    }

}
#endif
