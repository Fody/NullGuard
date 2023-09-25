public partial class ModuleWeaver
{
    public MethodReference ArgumentNullExceptionConstructor;
    public MethodReference ArgumentNullExceptionWithMessageConstructor;
    public MethodReference InvalidOperationExceptionConstructor;
    public MethodReference DebugAssertMethod;

    public void FindReferences()
    {
        var argumentNullException = FindTypeDefinition("ArgumentNullException");
        ArgumentNullExceptionConstructor = ModuleDefinition.ImportReference(
            argumentNullException.Methods.First(_ =>
                _.IsConstructor &&
                _.Parameters.Count == 1 &&
                _.Parameters[0].ParameterType.Name == "String"));
        ArgumentNullExceptionWithMessageConstructor = ModuleDefinition.ImportReference(
            argumentNullException.Methods.First(_ =>
                _.IsConstructor &&
                _.Parameters.Count == 2 &&
                _.Parameters[0].ParameterType.Name == "String" &&
                _.Parameters[1].ParameterType.Name == "String"));

        var invalidOperationException = FindTypeDefinition("InvalidOperationException");
        InvalidOperationExceptionConstructor = ModuleDefinition.ImportReference(
            invalidOperationException.Methods.First(_ =>
                _.IsConstructor &&
                _.Parameters.Count == 1 &&
                _.Parameters[0].ParameterType.Name == "String"));


        var debug = FindTypeDefinition("Debug");
        DebugAssertMethod = ModuleDefinition.ImportReference(
            debug.Methods.First(_ =>
                _.IsStatic &&
                _.Parameters.Count == 2 &&
                _.Parameters[0].ParameterType.Name == "Boolean" &&
                _.Parameters[1].ParameterType.Name == "String"));
    }
}