using System.Collections.Generic;
using NUnit.Framework;

public static class TestCaseHelper
{
    public static IEnumerable<TestCaseData> GetWovenTypes(string typeName)
    {
        yield return new TestCaseData(AssemblyWeaver.Assemblies[0].GetType(typeName)).SetName("net framework");
        yield return new TestCaseData(AssemblyWeaver.Assemblies[3].GetType(typeName)).SetName("net standard");
    }

    public static IEnumerable<TestCaseData> GetTypesWovenWithConfig(string typeName)
    {
        yield return new TestCaseData(AssemblyWeaver.Assemblies[1].GetType(typeName)).SetName("net framework");
        yield return new TestCaseData(AssemblyWeaver.Assemblies[4].GetType(typeName)).SetName("net standard");
    }
}