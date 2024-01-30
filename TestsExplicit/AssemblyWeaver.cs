using System.Reflection;
using System.Xml.Linq;
using DiffEngine;
using Fody;
using ICSharpCode.Decompiler.Metadata;

public static class AssemblyWeaver
{
    static AssemblyWeaver()
    {
        VerifyTests.VerifyICSharpCodeDecompiler.Initialize();
        DiffRunner.MaxInstancesToLaunch(100);

        var weaver = new ModuleWeaver
        {
            Config = new(
                "NullGuard",
                new XAttribute("IncludeDebugAssert", false),
                new XAttribute("ExcludeRegex", "^ClassToExclude$")),
            // Always testing the debug weaver
            DefineConstants = ["DEBUG"]
        };

        TestResult = weaver.ExecuteTestRun(
            assemblyPath: "AssemblyToProcessExplicit.dll",
            ignoreCodes: new[]
            {
                // Unexpected type on the stack (related to 0x801318DE)
                "0x80131854",
                // Unmanaged pointers are not a verifiable type
                "0x801318DE",
                // Unable to resolve token.
                "0x80131869"
            });
        Assembly = TestResult.Assembly;
        AfterAssemblyPath = TestResult.AssemblyPath;
        PeFile = new(AfterAssemblyPath);
    }

    public static PEFile PeFile;

    public static string AfterAssemblyPath;

    public static Assembly Assembly;

    public static TestResult TestResult;
}