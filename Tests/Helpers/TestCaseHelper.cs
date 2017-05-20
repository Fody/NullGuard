using System.Collections.Generic;
using NUnit.Framework;

public static class TestCaseHelper
{
    public static IEnumerable<TestCaseData> ClassWithExplicitInterfaceTypes => GetWovenTypes("ClassWithExplicitInterface");

    public static IEnumerable<TestCaseData> SampleClassTypes => GetWovenTypes("SimpleClass");

    public static IEnumerable<TestCaseData> SpecialClassTypes => GetWovenTypes("SpecialClass");

    public static IEnumerable<TestCaseData> ClassWithPrivateMethodTypes => GetWovenTypes("ClassWithPrivateMethod");

    public static IEnumerable<TestCaseData> IndexersClassTypes => GetWovenTypes("Indexers");

    public static IEnumerable<TestCaseData> ClassToExcludeTypes => GetTypesWovenWithConfig("ClassToExclude");

    public static IEnumerable<TestCaseData> GenericClassTypes => GetWovenTypes("GenericClass`1");

    private static IEnumerable<TestCaseData> GetWovenTypes(string typeName)
    {
        yield return new TestCaseData(AssemblyWeaver.Assemblies[0].GetType(typeName)).SetName("net framework");
        yield return new TestCaseData(AssemblyWeaver.Assemblies[3].GetType(typeName)).SetName("net standard");
    }

    private static IEnumerable<TestCaseData> GetTypesWovenWithConfig(string typeName)
    {
        yield return new TestCaseData(AssemblyWeaver.Assemblies[1].GetType(typeName)).SetName("net framework");
        yield return new TestCaseData(AssemblyWeaver.Assemblies[4].GetType(typeName)).SetName("net standard");
    }
}