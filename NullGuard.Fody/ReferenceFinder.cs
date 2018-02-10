using System.Linq;
using Mono.Cecil;

public partial class ModuleWeaver
{
    public MethodReference ArgumentNullExceptionConstructor;
    public MethodReference ArgumentNullExceptionWithMessageConstructor;
    public MethodReference InvalidOperationExceptionConstructor;
    public MethodReference DebugAssertMethod;

    public void FindReferences()
    {
        var argumentNullException = FindType("ArgumentNullException");
        ArgumentNullExceptionConstructor = ModuleDefinition.ImportReference(
            argumentNullException.Methods.First(x =>
                x.IsConstructor &&
                x.Parameters.Count == 1 &&
                x.Parameters[0].ParameterType.Name == "String"));
        ArgumentNullExceptionWithMessageConstructor = ModuleDefinition.ImportReference(
            argumentNullException.Methods.First(x =>
                x.IsConstructor &&
                x.Parameters.Count == 2 &&
                x.Parameters[0].ParameterType.Name == "String" &&
                x.Parameters[1].ParameterType.Name == "String"));

        var invalidOperationException = FindType("InvalidOperationException");
        InvalidOperationExceptionConstructor = ModuleDefinition.ImportReference(
            invalidOperationException.Methods.First(x =>
                x.IsConstructor &&
                x.Parameters.Count == 1 &&
                x.Parameters[0].ParameterType.Name == "String"));


        var debug = FindType("Debug");
        DebugAssertMethod = ModuleDefinition.ImportReference(
            debug.Methods.First(x =>
                x.IsStatic &&
                x.Parameters.Count == 2 &&
                x.Parameters[0].ParameterType.Name == "Boolean" &&
                x.Parameters[1].ParameterType.Name == "String"));
    }
}