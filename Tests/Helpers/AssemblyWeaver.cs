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
    public static string[] AfterAssemblyPaths;
    public static string[] AfterAssemblySymbolPaths;

    static AssemblyWeaver()
    {
        TestListener = new TestTraceListener();

        Debug.Listeners.Clear();
        Debug.Listeners.Add(TestListener);

        BeforeAssemblyPath = Path.GetFullPath(@"..\..\..\AssemblyToProcess\bin\Debug\AssemblyToProcess.dll");
        var beforePdbPath = Path.ChangeExtension(BeforeAssemblyPath, "pdb");
        MonoBeforeAssemblyPath = Path.GetFullPath(@"..\..\..\AssemblyToProcessMono\bin\Debug\AssemblyToProcessMono.dll");
        var monoBeforeMdbPath = MonoBeforeAssemblyPath + ".mdb";

#if (!DEBUG)
        BeforeAssemblyPath = BeforeAssemblyPath.Replace("Debug", "Release");
        beforePdbPath = beforePdbPath.Replace("Debug", "Release");
        MonoBeforeAssemblyPath = MonoBeforeAssemblyPath.Replace("Debug", "Release");
        monoBeforeMdbPath = monoBeforeMdbPath.Replace("Debug", "Release");
#endif

        AfterAssemblyPaths = new string[] {
            BeforeAssemblyPath.Replace(".dll", "2.dll"),
            BeforeAssemblyPath.Replace(".dll", "3.dll"),
            MonoBeforeAssemblyPath.Replace(".dll", "2.dll")
        };
        AfterAssemblySymbolPaths = new string[] {
            beforePdbPath.Replace(".pdb", "2.pdb"),
            beforePdbPath.Replace(".pdb", "3.pdb"),
            monoBeforeMdbPath.Replace(".mdb", "2.mdb")
        };

        Assemblies = new Assembly[3];
        Assemblies[0] = WeaveAssembly(BeforeAssemblyPath, AfterAssemblyPaths[0], beforePdbPath, AfterAssemblySymbolPaths[0], moduleDefinition =>
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

        Assemblies[1] = WeaveAssembly(BeforeAssemblyPath, AfterAssemblyPaths[1], beforePdbPath, AfterAssemblySymbolPaths[1], moduleDefinition =>
        {
            var assemblyResolver = new MockAssemblyResolver();

            var weavingTask = new ModuleWeaver
            {
                Config = new XElement("NullGuard", new XAttribute("IncludeDebugAssert", false)),
                ModuleDefinition = moduleDefinition,
                AssemblyResolver = assemblyResolver,
                LogInfo = LogInfo,
                LogWarn = LogWarn,
                LogError = LogError,
                DefineConstants = new List<string> { "DEBUG" } // Always testing the debug weaver
            };

            weavingTask.Execute();
        });

        Assemblies[2] = WeaveAssembly(MonoBeforeAssemblyPath, AfterAssemblyPaths[2], monoBeforeMdbPath, AfterAssemblySymbolPaths[2], moduleDefinition =>
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
    }

    private static Assembly WeaveAssembly(string beforeAssemblyPath, string afterAssemblyPath, string beforePdbPath, string afterPdbPath, Action<ModuleDefinition> weaveAction)
    {
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

    private static void LogInfo(string error)
    {
        Infos.Add(error);
    }

    private static void LogWarn(string error)
    {
        Warns.Add(error);
    }

    private static void LogError(string error)
    {
        Errors.Add(error);
    }

    public static List<string> Infos = new List<string>();
    public static List<string> Warns = new List<string>();
    public static List<string> Errors = new List<string>();
}