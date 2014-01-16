using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using Mono.Cecil;

public static class AssemblyWeaver
{
    public class TestTraceListener : TraceListener
    {
        public string Message;

        public void Reset()
        {
            Message = null;
        }

        public override void Write(string message)
        {
            if (Message != null)
                throw new Exception("More than one Debug message came through. Did you forget to reset before a test?");

            Message = message;
        }

        public override void WriteLine(string message)
        {
            if (Message != null)
                throw new Exception("More than one Debug message came through. Did you forget to reset before a test?");

            Message = message;
        }
    }

    public static Assembly Assembly;
    public static Assembly Assembly2;
    public static TestTraceListener TestListener;

    static AssemblyWeaver()
    {
        TestListener = new TestTraceListener();

        Debug.Listeners.Clear();
        Debug.Listeners.Add(TestListener);

        BeforeAssemblyPath = Path.GetFullPath(@"..\..\..\AssemblyToProcess\bin\Debug\AssemblyToProcess.dll");
        var beforePdbPath = Path.ChangeExtension(BeforeAssemblyPath, "pdb");

#if (!DEBUG)
        BeforeAssemblyPath = BeforeAssemblyPath.Replace("Debug", "Release");
        beforePdbPath = beforePdbPath.Replace("Debug", "Release");
#endif
        AfterAssemblyPath = BeforeAssemblyPath.Replace(".dll", "2.dll");
        var afterPdbPath = beforePdbPath.Replace(".pdb", "2.pdb");

        AfterAssembly2Path = BeforeAssemblyPath.Replace(".dll", "3.dll");
        var afterPdb2Path = beforePdbPath.Replace(".pdb", "3.pdb");

        Assembly = WeaveAssembly(AfterAssemblyPath, beforePdbPath, afterPdbPath, moduleDefinition =>
        {
            var assemblyResolver = new MockAssemblyResolver();

            var weavingTask = new ModuleWeaver
            {
                ModuleDefinition = moduleDefinition,
                AssemblyResolver = assemblyResolver,
                LogError = LogError,
                DefineConstants = new List<string> { "DEBUG" } // Always testing the debug weaver
            };

            weavingTask.Execute();
        });

        Assembly2 = WeaveAssembly(AfterAssembly2Path, beforePdbPath, afterPdb2Path, moduleDefinition =>
        {
            var assemblyResolver = new MockAssemblyResolver();

            var weavingTask = new ModuleWeaver
            {
                Config = new XElement("NullGuard", new XAttribute("IncludeDebugAssert", false), new XAttribute("ExcludeRegex", "^ClassToExclude$")),
                ModuleDefinition = moduleDefinition,
                AssemblyResolver = assemblyResolver,
                LogError = LogError,
                DefineConstants = new List<string> { "DEBUG" } // Always testing the debug weaver
            };

            weavingTask.Execute();
        });
    }

    private static Assembly WeaveAssembly(string afterAssemblyPath, string beforePdbPath, string afterPdbPath, Action<ModuleDefinition> weaveAction)
    {
        File.Copy(BeforeAssemblyPath, afterAssemblyPath, true);
        if (File.Exists(beforePdbPath))
            File.Copy(beforePdbPath, afterPdbPath, true);

        var readerParameters = new ReaderParameters();
        var writerParameters = new WriterParameters();

        if (File.Exists(afterPdbPath))
        {
            readerParameters.ReadSymbols = true;
            writerParameters.WriteSymbols = true;
        }

        var moduleDefinition = ModuleDefinition.ReadModule(afterAssemblyPath, readerParameters);

        weaveAction(moduleDefinition);

        moduleDefinition.Write(afterAssemblyPath, writerParameters);

        return Assembly.LoadFile(afterAssemblyPath);
    }

    public static string BeforeAssemblyPath;
    public static string AfterAssemblyPath;
    public static string AfterAssembly2Path;

    private static void LogError(string error)
    {
        Errors.Add(error);
    }

    public static List<string> Errors = new List<string>();
}