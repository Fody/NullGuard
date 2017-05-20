﻿using System;
using System.Diagnostics;
using System.IO;
using Mono.Cecil;
using Xunit;

public class LargeAssemblyTest
{
    [Fact]
    public void ProcessAllAssemblies()
    {
        foreach (var assemblyPath in Directory.EnumerateFiles(Environment.CurrentDirectory, "*.dll"))
        {
            if (assemblyPath.EndsWith("2.dll") || assemblyPath.EndsWith("3.dll"))
            {
                continue;
            }
            VerifyAssembly(assemblyPath);
        }
    }

    public void VerifyAssembly(string assemblyPath)
    {

        Debug.WriteLine("Verifying " + assemblyPath);
        var cleanAssembly = assemblyPath.Replace(".dll", "2.dll");
        var newAssembly = assemblyPath.Replace(".dll", "3.dll");
        File.Copy(assemblyPath, cleanAssembly, true);
        File.Copy(assemblyPath, newAssembly, true);

        var assemblyResolver = new MockAssemblyResolver();
        var moduleDefinition = ModuleDefinition.ReadModule(cleanAssembly);
        var weavingTask = new ModuleWeaver
        {
            ModuleDefinition = moduleDefinition,
            AssemblyResolver = assemblyResolver,
        };
        moduleDefinition.Write(cleanAssembly);

        moduleDefinition = ModuleDefinition.ReadModule(newAssembly);
        weavingTask.ModuleDefinition = moduleDefinition;
        weavingTask.Execute();
        moduleDefinition.Write(newAssembly);

        Verifier.Verify(cleanAssembly, newAssembly);
    }
}