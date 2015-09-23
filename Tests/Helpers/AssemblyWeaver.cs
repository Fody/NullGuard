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
    public static string WithoutJetBrainsAnnotationsAssemblyPath;

    // REVIEW: do we want to use _named_ test data assembly paths (instead of indexes)? I.e. WeavedAssemblyToProcess,
    //         WeavedAssemblyToProcessWithoutDebugAssert, WeavedAssemblyToProcessMono, WeavedAssemblyToProcessWithoutJetBrainsAnnotations
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
        WithoutJetBrainsAnnotationsAssemblyPath = Path.GetFullPath(@"..\..\..\AssemblyToProcessWithoutJetBrainsAnnotations\bin\Debug\AssemblyToProcessWithoutJetBrainsAnnotations.dll");
        var withoutJetBrainsAnnotationsAssemblyBeforePdbPath = Path.ChangeExtension(WithoutJetBrainsAnnotationsAssemblyPath, "pdb");

#if (!DEBUG)
        BeforeAssemblyPath = BeforeAssemblyPath.Replace("Debug", "Release");
        beforePdbPath = beforePdbPath.Replace("Debug", "Release");
        MonoBeforeAssemblyPath = MonoBeforeAssemblyPath.Replace("Debug", "Release");
        monoBeforeMdbPath = monoBeforeMdbPath.Replace("Debug", "Release");
#endif

        AfterAssemblyPaths = new [] {
            BeforeAssemblyPath.Replace(".dll", "2.dll"),
            BeforeAssemblyPath.Replace(".dll", "3.dll"),
            MonoBeforeAssemblyPath.Replace(".dll", "2.dll"),
            WithoutJetBrainsAnnotationsAssemblyPath.Replace(".dll", "2.dll")
        };
        AfterAssemblySymbolPaths = new [] {
            beforePdbPath.Replace(".pdb", "2.pdb"),
            beforePdbPath.Replace(".pdb", "3.pdb"),
            monoBeforeMdbPath.Replace(".mdb", "2.mdb"),
            withoutJetBrainsAnnotationsAssemblyBeforePdbPath.Replace(".pdb", "2.pdb")
        };

        Assemblies = new Assembly[4];
        Assemblies[0] = WeaveAssembly(BeforeAssemblyPath, AfterAssemblyPaths[0], beforePdbPath, AfterAssemblySymbolPaths[0]);
        Assemblies[1] = WeaveAssembly(BeforeAssemblyPath, AfterAssemblyPaths[1], beforePdbPath, AfterAssemblySymbolPaths[1], moduleWeaver =>
        {
            moduleWeaver.Config =
                    new XElement("NullGuard", new XAttribute("IncludeDebugAssert", false), new XAttribute("ExcludeRegex", "^ClassToExclude$"));
        });
        Assemblies[2] = WeaveAssembly(MonoBeforeAssemblyPath, AfterAssemblyPaths[2], monoBeforeMdbPath, AfterAssemblySymbolPaths[2]);
        Assemblies[3] = WeaveAssembly(WithoutJetBrainsAnnotationsAssemblyPath, AfterAssemblyPaths[3], withoutJetBrainsAnnotationsAssemblyBeforePdbPath, AfterAssemblySymbolPaths[3]);
    }

    static Assembly WeaveAssembly(string beforeAssemblyPath, string afterAssemblyPath, string beforePdbPath, string afterPdbPath, Action<ModuleWeaver> changeConfiguration = null)
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

        var assemblyResolver = new MockAssemblyResolver();

        var moduleWeaver = new ModuleWeaver
        {
            ModuleDefinition = moduleDefinition,
            AssemblyResolver = assemblyResolver,
            LogInfo = LogInfo,
            LogWarn = LogWarn,
            LogError = LogError,
            DefineConstants = new List<string> { "DEBUG" } // Always testing the debug weaver
        };

        changeConfiguration?.Invoke(moduleWeaver);

        moduleWeaver.Execute();

        moduleDefinition.Write(afterAssemblyPath, writerParameters);

        return Assembly.LoadFile(afterAssemblyPath);
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