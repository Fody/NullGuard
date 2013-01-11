using System;
using System.Diagnostics;
using System.IO;
using Mono.Cecil;
using NUnit.Framework;

[TestFixture]
public class LargeAssemblyTest
{
    [TestCase("Esent.Interop")]
    [TestCase("GeoAPI")]
    //[TestCase("ICSharpCode.NRefactory.CSharp")]
    [TestCase("ICSharpCode.NRefactory")]
    [TestCase("Jint.Raven")]
    [TestCase("log4net")]
    [TestCase("Lucene.Net.Contrib.Spatial.NTS")]
    //[TestCase("Lucene.Net")]
    //[TestCase("Mono.Cecil")]
    //[TestCase("Mono.Cecil.Mdb")]
    [TestCase("Mono.Cecil.Pdb")]
    [TestCase("Mono.Cecil.Rocks")]
    [TestCase("NetTopologySuite")]
    //[TestCase("NServiceBus.Core")]
    [TestCase("NServiceBus")]
    [TestCase("NullGuard")]
    [TestCase("NullGuard.Fody")]
    [TestCase("nunit.framework")]
    //[TestCase("PowerCollections")]
    //[TestCase("Raven.Abstractions")]
    //[TestCase("Raven.Database")]
    [TestCase("Spatial4n.Core.NTS")]
    [TestCase("Tests")]
    public void VerifyAssembly(string assemblyName)
    {
        var assemblyPath = Path.Combine(Environment.CurrentDirectory, assemblyName + ".dll");

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