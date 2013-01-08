using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Mono.Cecil;

public static class AssemblyWeaver
{
    public static Assembly Assembly;

    static AssemblyWeaver()
    {
        var assemblyPath = Path.GetFullPath(@"..\..\..\AssemblyToProcess\bin\Debug\AssemblyToProcess.dll");

#if (!DEBUG)
        assemblyPath = assemblyPath.Replace("Debug", "Release");
#endif

        var newAssembly = assemblyPath.Replace(".dll", "2.dll");

        File.Copy(assemblyPath, newAssembly, true);

        var moduleDefinition = ModuleDefinition.ReadModule(newAssembly);
        var weavingTask = new ModuleWeaver
            {
                ModuleDefinition = moduleDefinition,
                AssemblyResolver = new MockAssemblyResolver(),
                LogError = LogError
            };

        weavingTask.Execute();
        moduleDefinition.Write(newAssembly);

        Assembly = Assembly.LoadFile(newAssembly);
    }

    static void LogError(string error)
    {
        Errors.Add(error);
    }

    public static List<string> Errors = new List<string>();
}