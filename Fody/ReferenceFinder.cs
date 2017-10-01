using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
public partial class ModuleWeaver
{
    public MethodReference ArgumentNullExceptionConstructor;
    public MethodReference ArgumentNullExceptionWithMessageConstructor;
    public MethodReference InvalidOperationExceptionConstructor;
    public MethodReference DebugAssertMethod;

    void AddAssemblyIfExists(string name, List<TypeDefinition> types)
    {
        try
        {
            var assembly = AssemblyResolver.Resolve(new AssemblyNameReference(name, null));

            if (assembly != null)
            {
                types.AddRange(assembly.MainModule.Types);
            }
        }
        catch (AssemblyResolutionException)
        {
            LogInfo($"Failed to resolve '{name}'. So skipping its types.");
        }
    }
    public void FindReferences()
    {
        var types = new List<TypeDefinition>();

        AddAssemblyIfExists("mscorlib", types);
        AddAssemblyIfExists("System.Runtime", types);
        AddAssemblyIfExists("System", types);
        AddAssemblyIfExists("netstandard", types);
        AddAssemblyIfExists("System.Diagnostics.Debug", types);

        var argumentNullException = types.FirstOrThrow(x => x.Name == "ArgumentNullException", "ArgumentNullException");
        ArgumentNullExceptionConstructor = ModuleDefinition.ImportReference(argumentNullException.Methods.First(x =>
            x.IsConstructor &&
            x.Parameters.Count == 1 &&
            x.Parameters[0].ParameterType.Name == "String"));
        ArgumentNullExceptionWithMessageConstructor = ModuleDefinition.ImportReference(argumentNullException.Methods.First(x =>
            x.IsConstructor &&
            x.Parameters.Count == 2 &&
            x.Parameters[0].ParameterType.Name == "String" &&
            x.Parameters[1].ParameterType.Name == "String"));

        var invalidOperationException = types.FirstOrThrow(x => x.Name == "InvalidOperationException", "InvalidOperationException");
        InvalidOperationExceptionConstructor = ModuleDefinition.ImportReference(invalidOperationException.Methods.First(x =>
            x.IsConstructor &&
            x.Parameters.Count == 1 &&
            x.Parameters[0].ParameterType.Name == "String"));


        var debug = types.FirstOrThrow(x => x.Name == "Debug", "Debug");
        DebugAssertMethod = ModuleDefinition.ImportReference(debug.Methods.First(x =>
            x.IsStatic &&
            x.Parameters.Count == 2 &&
            x.Parameters[0].ParameterType.Name == "Boolean" &&
            x.Parameters[1].ParameterType.Name == "String"));
    }
}