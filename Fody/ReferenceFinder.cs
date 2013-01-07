using System.Linq;
using Mono.Cecil;

public partial class ModuleWeaver
{

    public MethodReference ArgumentNullExceptionConstructor;
    
    public void FindReferences()
    {
        var mscorlib = AssemblyResolver.Resolve("mscorlib");
        var mscorlibTypes = mscorlib.MainModule.Types;
        var argumentNullException = mscorlibTypes.First(x => x.Name == "ArgumentNullException");
        ArgumentNullExceptionConstructor = ModuleDefinition.Import(argumentNullException.Methods.First(x =>
            x.IsConstructor &&
            x.Parameters.Count == 1 &&
            x.Parameters[0].ParameterType.Name == "String"));


    }

}