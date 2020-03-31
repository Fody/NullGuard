using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using Fody;

public static class AssemblyWeaver
{
    static AssemblyWeaver()
    {
        var weavingTask = new ModuleWeaver
        {
            Config = new XElement("NullGuard",
                new XAttribute("IncludeDebugAssert", false),
                new XAttribute("ExcludeRegex", "^ClassToExclude$")),
            DefineConstants = new List<string> {"DEBUG"} // Always testing the debug weaver
        };

        TestResult = weavingTask.ExecuteTestRun(
            assemblyPath: "AssemblyToProcessExplicit.dll", 
            ignoreCodes: new[]
            {
                "0x80131854", // Unexpected type on the stack (related to 0x801318DE)
                "0x801318DE", // Unmanaged pointers are not a verifiable type
            });
        Assembly = TestResult.Assembly;
        AfterAssemblyPath = TestResult.AssemblyPath;
    }

    public static string AfterAssemblyPath;

    public static Assembly Assembly;

    public static TestResult TestResult;
}