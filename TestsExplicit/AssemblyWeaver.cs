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

        var weavingTask = new ModuleWeaver
        {
            Config = new("NullGuard",
                new XAttribute("IncludeDebugAssert", false),
                new XAttribute("ExcludeRegex", "^ClassToExclude$")),
            DefineConstants = new() {"DEBUG"} // Always testing the debug weaver
        };

        TestResult = weavingTask.ExecuteTestRun(
            assemblyPath: "AssemblyToProcessExplicit.dll",
            ignoreCodes: new[]
            {
                "0x80131854", // Unexpected type on the stack (related to 0x801318DE)
                "0x801318DE", // Unmanaged pointers are not a verifiable type
                "0x80131869" // Unable to resolve token.
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