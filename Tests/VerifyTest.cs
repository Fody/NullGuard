#if(DEBUG)

using NUnit.Framework;

[TestFixture]
public class VerifyTest
{
    [Test]
    public void PeVerify1()
    {
        Verifier.Verify(AssemblyWeaver.BeforeAssemblyPath, AssemblyWeaver.Assemblies[0].Location);
    }

    [Test]
    public void PeVerify2()
    {
        Verifier.Verify(AssemblyWeaver.BeforeAssemblyPath, AssemblyWeaver.Assemblies[1].Location);
    }

    [Test]
    public void PeVerify3()
    {
        Verifier.Verify(AssemblyWeaver.MonoBeforeAssemblyPath, AssemblyWeaver.Assemblies[2].Location);
    }
}

#endif