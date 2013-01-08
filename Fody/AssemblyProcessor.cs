using Mono.Cecil;

public partial class ModuleWeaver
{
    private void ProcessAssembly()
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
            foreach (var property in type.ConcreteProperties())
            {
                ProcessProperty(property);
            }
        }
    }

    private void ProcessMethod(MethodDefinition method)
    {
        var methodProcessor = new MethodProcessor
            {
                ModuleWeaver = this,
                Method = method,
            };
        methodProcessor.Process();
    }

    private void ProcessProperty(PropertyDefinition property)
    {
        var propertyProcessor = new PropertyProcessor
        {
            ModuleWeaver = this,
            Property = property,
        };
        propertyProcessor.Process();
    }
}