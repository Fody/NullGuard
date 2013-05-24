using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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

        File.Copy(BeforeAssemblyPath, AfterAssemblyPath, true);
        if (File.Exists(beforePdbPath))
            File.Copy(beforePdbPath, afterPdbPath, true);

        var assemblyResolver = new MockAssemblyResolver();
        var readerParameters = new ReaderParameters();
        var writerParameters = new WriterParameters();

        if (File.Exists(afterPdbPath))
        {
            readerParameters.ReadSymbols = true;
            writerParameters.WriteSymbols = true;
        }

        var moduleDefinition = ModuleDefinition.ReadModule(AfterAssemblyPath, readerParameters);

        var weavingTask = new ModuleWeaver
        {
            ModuleDefinition = moduleDefinition,
            AssemblyResolver = assemblyResolver,
            LogError = LogError,
            DefineConstants = new[] { "DEBUG" } // Always testing the debug weaver
        };

        weavingTask.Execute();
        moduleDefinition.Write(AfterAssemblyPath, writerParameters);

        Assembly = Assembly.LoadFile(AfterAssemblyPath);
    }

    public static string BeforeAssemblyPath;
    public static string AfterAssemblyPath;

	static void LogError(string error)
    {
        Errors.Add(error);
    }

    public static List<string> Errors = new List<string>();
}