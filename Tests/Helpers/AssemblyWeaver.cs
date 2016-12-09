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

    public static TestTraceListener TestListener;
    public static Assembly[] Assemblies;
    public static string BeforeAssemblyPath;
    public static string MonoBeforeAssemblyPath;
    public static Assembly[] RewritingTestAssemblies;

    static AssemblyWeaver()
    {
        TestListener = new TestTraceListener();

        Debug.Listeners.Clear();
        Debug.Listeners.Add(TestListener);

        // Needed for local test run with NUnit 3.5
        // Directory.SetCurrentDirectory(Path.GetDirectoryName(typeof(AssemblyWeaver).Assembly.Location));

        BeforeAssemblyPath = Path.GetFullPath(@"..\..\..\AssemblyToProcess\bin\Debug\AssemblyToProcess.dll");
        MonoBeforeAssemblyPath = Path.GetFullPath(@"..\..\..\AssemblyToProcessMono\bin\Debug\AssemblyToProcessMono.dll");
        var explicitBeforeAssemblyPath = Path.GetFullPath(@"..\..\..\AssemblyToProcessExplicit\bin\Debug\AssemblyToProcessExplicit.dll");

        Func<string, string> getSymbolFilePath = assemblyPath => Path.ChangeExtension(assemblyPath, "pdb");
        Func<string, string> getMonoSymbolFilePath = assemblyPath => assemblyPath + ".mdb";

#if (!DEBUG)
        BeforeAssemblyPath = BeforeAssemblyPath.Replace("Debug", "Release");
        MonoBeforeAssemblyPath = MonoBeforeAssemblyPath.Replace("Debug", "Release");
        explicitBeforeAssemblyPath = explicitBeforeAssemblyPath.Replace("Debug", "Release");
#endif

        Assemblies = new Assembly[4];

        Assemblies[0] = WeaveAssembly(BeforeAssemblyPath, getSymbolFilePath, 2, moduleDefinition =>
        {
            var assemblyResolver = new MockAssemblyResolver();

            var weavingTask = new ModuleWeaver
            {
                ModuleDefinition = moduleDefinition,
                AssemblyResolver = assemblyResolver,
                LogInfo = LogInfo,
                LogWarn = LogWarn,
                LogError = LogError,
                DefineConstants = new List<string> { "DEBUG" } // Always testing the debug weaver
            };

            weavingTask.Execute();
        });

        Assemblies[1] = WeaveAssembly(BeforeAssemblyPath, getSymbolFilePath, 3, moduleDefinition =>
        {
            var assemblyResolver = new MockAssemblyResolver();

            var weavingTask = new ModuleWeaver
            {
                Config = new XElement("NullGuard", new XAttribute("IncludeDebugAssert", false), new XAttribute("ExcludeRegex", "^ClassToExclude$")),
                ModuleDefinition = moduleDefinition,
                AssemblyResolver = assemblyResolver,
                LogInfo = LogInfo,
                LogWarn = LogWarn,
                LogError = LogError,
                DefineConstants = new List<string> { "DEBUG" } // Always testing the debug weaver
            };

            weavingTask.Execute();
        });

        Assemblies[2] = WeaveAssembly(MonoBeforeAssemblyPath, getMonoSymbolFilePath, 2, moduleDefinition =>
        {
            var assemblyResolver = new MockAssemblyResolver();

            var weavingTask = new ModuleWeaver
            {
                ModuleDefinition = moduleDefinition,
                AssemblyResolver = assemblyResolver,
                LogInfo = LogInfo,
                LogWarn = LogWarn,
                LogError = LogError,
                DefineConstants = new List<string> { "DEBUG" } // Always testing the debug weaver
            };

            weavingTask.Execute();
        });

        Assemblies[3] = WeaveAssembly(explicitBeforeAssemblyPath, getSymbolFilePath, 4, moduleDefinition =>
        {
            var assemblyResolver = new MockAssemblyResolver();

            var weavingTask = new ModuleWeaver
            {
                Config = new XElement("NullGuard", new XAttribute("Mode", "Explicit")),
                ModuleDefinition = moduleDefinition,
                AssemblyResolver = assemblyResolver,
                DefineConstants = new List<string> { "DEBUG" } // Always testing the debug weaver
            };

            weavingTask.Execute();
        });

        RewritingTestAssemblies = new[] { Assemblies[0], Assemblies[3] };
    }

    static Assembly WeaveAssembly(string beforeAssemblyPath, Func<string, string> getSymbolFilePath, int pathIndex, Action<ModuleDefinition> weaveAction)
    {
        var afterAssemblyPath = AddIndex(beforeAssemblyPath, pathIndex);
        var beforePdbPath = getSymbolFilePath(beforeAssemblyPath);
        var afterPdbPath = AddIndex(beforePdbPath, pathIndex);

        if (File.Exists(afterAssemblyPath))
            File.Delete(afterAssemblyPath);
        if (File.Exists(afterPdbPath))
            File.Delete(afterPdbPath);

        var readerParameters = new ReaderParameters();
        var writerParameters = new WriterParameters();

        if (File.Exists(beforePdbPath))
        {
            readerParameters.ReadSymbols = true;
            readerParameters.SymbolStream = new FileStream(beforePdbPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            writerParameters.WriteSymbols = true;
        }

        var moduleDefinition = ModuleDefinition.ReadModule(beforeAssemblyPath, readerParameters);

        weaveAction(moduleDefinition);

        moduleDefinition.Write(afterAssemblyPath, writerParameters);

        return Assembly.LoadFile(afterAssemblyPath);
    }

    static string AddIndex(string filePath, int index)
    {
        return Path.ChangeExtension(filePath, index.ToString()) + Path.GetExtension(filePath);
    }

    static void LogInfo(string error)
    {
        Infos.Add(error);
    }

    static void LogWarn(string error)
    {
        Warns.Add(error);
    }

    static void LogError(string error)
    {
        Errors.Add(error);
    }

    public static List<string> Infos = new List<string>();
    public static List<string> Warns = new List<string>();
    public static List<string> Errors = new List<string>();
}