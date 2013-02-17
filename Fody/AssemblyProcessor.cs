using Mono.Cecil;
using System.Linq;

public partial class ModuleWeaver
{
    private bool isDebug;

    void ProcessAssembly()
    {
        isDebug = DefineConstants.Any(c => c == "DEBUG");

        foreach (var type in types)
        {
            if (type.ContainsAllowNullAttribute() || type.IsCompilerGenerated())
            {
                continue;
            }
            foreach (var method in type.MethodsWithBody())
            {
                ProcessMethod(method);
            }
            foreach (var property in type.ConcreteProperties())
            {
                ProcessProperty(property);
            }
        }
    }

    void ProcessMethod(MethodDefinition method)
    {
        var methodProcessor = new MethodProcessor
            {
                ModuleWeaver = this,
                Method = method,
                IsDebug = isDebug,
            };
        methodProcessor.Process();
    }

    void ProcessProperty(PropertyDefinition property)
    {
        var propertyProcessor = new PropertyProcessor
        {
            ModuleWeaver = this,
            Property = property,
            IsDebug = isDebug,
        };
        propertyProcessor.Process();
    }
}