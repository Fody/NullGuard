
using Mono.Cecil;

public partial class ModuleWeaver
{

    void ProcessAssembly()
    {
        foreach (var type in types)
        {
            if (type.ContainsAllowNullAttribute())
            {
                continue;
            }
            foreach (var method in type.ConcreteMethods())
            {
                ProcessMethod(method);
            }
        }
    }

    void ProcessMethod(MethodDefinition method)
    {
        var methodProcessor = new MethodProcessor
            {
                ModuleWeaver = this,
                TypeSystem = ModuleDefinition.TypeSystem,
                Method = method,
            };
        methodProcessor.Process();
    }
}