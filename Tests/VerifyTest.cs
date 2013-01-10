using NUnit.Framework;

[TestFixture]
public class VerifyTest
{

#if(DEBUG)

    [Test]
    public void PeVerify()
    {
        Verifier.Verify(AssemblyWeaver.BeforeAssemblyPath, AssemblyWeaver.AfterAssemblyPath);
    }

#endif
}