using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
#if (NET472)
using ApprovalTests.Core;
using ApprovalTests.Reporters;
#endif
using Fody;
#pragma warning disable 618

public static class AssemblyWeaver
{
    public static Assembly Assembly;

    static AssemblyWeaver()
    {
#if (NET472)
        //TODO: this works around https://github.com/approvals/ApprovalTests.Net/issues/159
        var reporters = (IEnvironmentAwareReporter[])FrameworkAssertReporter.INSTANCE.Reporters;
        reporters[2] = XUnit2Reporter.INSTANCE;
#endif
        var weavingTask = new ModuleWeaver
        {
            Config = new XElement("NullGuard",
                new XAttribute("IncludeDebugAssert", false),
                new XAttribute("ExcludeRegex", "^ClassToExclude$")),
            DefineConstants = new List<string> {"DEBUG"} // Always testing the debug weaver
        };

        TestResult = weavingTask.ExecuteTestRun("AssemblyToProcess.dll",
            ignoreCodes: new[] {"0x80131854", "0x801318DE", "0x80131205", "0x80131252" });
        Assembly = TestResult.Assembly;
        AfterAssemblyPath = TestResult.AssemblyPath;
    }

    public static string AfterAssemblyPath;

    public static TestResult TestResult;
}