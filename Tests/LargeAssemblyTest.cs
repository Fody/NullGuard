using System;
using System.Diagnostics;
using System.IO;
using Mono.Cecil;
using NUnit.Framework;

[TestFixture]
public class LargeAssemblyTest
{
	[Test]
	[Ignore]
	public void Run()
	{
		foreach (var assemblyPath in Directory.EnumerateFiles(Environment.CurrentDirectory, "*.dll"))
		{
			if (assemblyPath.Contains("AssemblyToProcess"))
			{
				continue;
			}
			Debug.WriteLine("Verifying " + assemblyPath);
			var newAssembly = assemblyPath.Replace(".dll", "2.dll");
			File.Copy(assemblyPath, newAssembly, true);
			var assemblyResolver = new MockAssemblyResolver();
			var moduleDefinition = ModuleDefinition.ReadModule(newAssembly);

			var weavingTask = new ModuleWeaver
				{
					ModuleDefinition = moduleDefinition,
					AssemblyResolver = assemblyResolver,
				};

			weavingTask.Execute();
			moduleDefinition.Write(newAssembly);
			Verifier.Verify(assemblyPath, newAssembly);
		}
	}

}