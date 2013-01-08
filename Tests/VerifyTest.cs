using NUnit.Framework;

[TestFixture]
public class VerifyTest
{

#if(DEBUG)

    [Test]
    public void PeVerify()
    {
        Verifier.Verify(AssemblyWeaver.Assembly.CodeBase.Remove(0, 8));
    }

#endif
}