using System.Linq;
using Mono.Cecil;

public static class ReferenceFinder
{
    public static MethodReference ArgumentExceptionConstructor;
    public static MethodReference ArgumentNullExceptionConstructor;
    public static MethodReference ArgumentNullExceptionWithMessageConstructor;
    public static MethodReference InvalidOperationExceptionConstructor;

    public static MethodReference DebugAssertMethod;

    public static void FindReferences(IAssemblyResolver assemblyResolver, ModuleDefinition moduleDefinition)
    {
        var mscorlib = assemblyResolver.Resolve("mscorlib");
        var mscorlibTypes = mscorlib.MainModule.Types;

        var argumentException = mscorlibTypes.First(x => x.Name == "ArgumentException");
        ArgumentExceptionConstructor = moduleDefinition.Import(argumentException.Methods.First(x =>
            x.IsConstructor &&
            x.Parameters.Count == 2 &&
            x.Parameters[0].ParameterType.Name == "String" &&
            x.Parameters[1].ParameterType.Name == "String"));

        var argumentNullException = mscorlibTypes.First(x => x.Name == "ArgumentNullException");
        ArgumentNullExceptionConstructor = moduleDefinition.Import(argumentNullException.Methods.First(x =>
            x.IsConstructor &&
            x.Parameters.Count == 1 &&
            x.Parameters[0].ParameterType.Name == "String"));
        ArgumentNullExceptionWithMessageConstructor = moduleDefinition.Import(argumentNullException.Methods.First(x =>
            x.IsConstructor &&
            x.Parameters.Count == 2 &&
            x.Parameters[0].ParameterType.Name == "String" &&
            x.Parameters[1].ParameterType.Name == "String"));

        var invalidOperationException = mscorlibTypes.First(x => x.Name == "InvalidOperationException");
        InvalidOperationExceptionConstructor = moduleDefinition.Import(invalidOperationException.Methods.First(x =>
            x.IsConstructor &&
            x.Parameters.Count == 1 &&
            x.Parameters[0].ParameterType.Name == "String"));

        var systemlib = assemblyResolver.Resolve("System");
        var systemlibTypes = systemlib.MainModule.Types;

        var debug = systemlibTypes.First(x => x.Name == "Debug");
        DebugAssertMethod = moduleDefinition.Import(debug.Methods.First(x =>
            x.IsStatic &&
            x.Parameters.Count == 2 &&
            x.Parameters[0].ParameterType.Name == "Boolean" &&
            x.Parameters[1].ParameterType.Name == "String"));
    }
}