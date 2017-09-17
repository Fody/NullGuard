using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using Mono.Cecil;
using Mono.Cecil.Pdb;
using NUnit.Framework;

public static class AssemblyWeaver
{
    public class TestTraceListener : TraceListener
    {
        public  TestTraceListener()
        {
            Trace.Listeners.Add(this);
        }
        public string Message;

        public void Reset()
        {
            Message = null;
        }

        public override void Write(string message)
        {
            if (Message != null)
            {
                throw new Exception("More than one Debug message came through. Did you forget to reset before a test?");
            }

            Message = message;
        }

        public override void WriteLine(string message)
        {
            if (Message != null)
            {
                throw new Exception("More than one Debug message came through. Did you forget to reset before a test?");
            }

            Message = message;
        }
    }

    public static TestTraceListener TestListener;
    public static Assembly Assembly;
    public static string BeforeAssemblyPath;
    public static string BeforeSymbolPath;
    public static string AfterAssemblyPath;
    public static string AfterSymbolPath;

    public static dynamic GetInstance(string typeName)
    {
        var type = Assembly.GetType(typeName);
        return Activator.CreateInstance(type);
    }

    static AssemblyWeaver()
    {
        TestListener = new TestTraceListener();

        Debug.Listeners.Clear();
        Debug.Listeners.Add(TestListener);

        var testDirectory = TestContext.CurrentContext.TestDirectory;
        BeforeAssemblyPath = Path.GetFullPath(Path.Combine(testDirectory, @"..\..\..\..\AssemblyToProcess\bin\Debug\net452\AssemblyToProcess.dll"));
        BeforeSymbolPath = Path.ChangeExtension(BeforeAssemblyPath, "pdb");
#if (!DEBUG)
        BeforeAssemblyPath = BeforeAssemblyPath.Replace("Debug", "Release");
        BeforeSymbolPath = BeforeSymbolPath.Replace("Debug", "Release");
#endif

        AfterAssemblyPath = BeforeAssemblyPath.Replace(".dll", "2.dll");
        AfterSymbolPath = BeforeSymbolPath.Replace(".pdb", "2.pdb");

        if (File.Exists(AfterAssemblyPath))
        {
            File.Delete(AfterAssemblyPath);
        }
        if (File.Exists(AfterSymbolPath))
        {
            File.Delete(AfterSymbolPath);
        }

        var assemblyResolver = new MockAssemblyResolver();
        var readerParameters = new ReaderParameters {AssemblyResolver = assemblyResolver};
        var writerParameters = new WriterParameters();

        if (File.Exists(BeforeSymbolPath))
        {
            readerParameters.ReadSymbols = true;
            readerParameters.SymbolReaderProvider = new PdbReaderProvider();
            readerParameters.SymbolStream = new FileStream(BeforeSymbolPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            writerParameters.WriteSymbols = true;
            writerParameters.SymbolWriterProvider = new PdbWriterProvider();
        }

        var moduleDefinition = ModuleDefinition.ReadModule(BeforeAssemblyPath, readerParameters);


        var weavingTask = new ModuleWeaver
        {
            Config = new XElement("NullGuard",
                new XAttribute("IncludeDebugAssert", false),
                new XAttribute("ExcludeRegex", "^ClassToExclude$")),
            ModuleDefinition = moduleDefinition,
            AssemblyResolver = assemblyResolver,
            LogInfo = LogInfo,
            LogWarn = LogWarn,
            LogError = LogError,
            DefineConstants = new List<string> {"DEBUG"} // Always testing the debug weaver
        };

        weavingTask.Execute();

        moduleDefinition.Write(AfterAssemblyPath, writerParameters);

        Assembly = Assembly.LoadFile(AfterAssemblyPath);
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