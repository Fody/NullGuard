#if(DEBUG)

using Xunit;

public class VerifyTest
{
    [SkippableFact]
    public void PeVerify1()
    {
        Verifier.Verify(AssemblyWeaver.BeforeAssemblyPath, AssemblyWeaver.AfterAssemblyPaths[0]);
    }

    [SkippableFact]
    public void PeVerify2()
    {
        Verifier.Verify(AssemblyWeaver.BeforeAssemblyPath, AssemblyWeaver.AfterAssemblyPaths[1]);
    }

    //[SkippableFact]
    //public void PeVerify3()
    //{
    //    Verifier.Verify(AssemblyWeaver.MonoBeforeAssemblyPath, AssemblyWeaver.AfterAssemblyPaths[2]);
    //}
}

#endif