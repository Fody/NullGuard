using System.Linq;

public partial class ModuleWeaver
{
    private void ProcessAssembly()
    {
        var isDebug = DefineConstants.Any(c => c == "DEBUG");

        var methodProcessor = new MethodProcessor(ValidationFlags, isDebug);
        var propertyProcessor = new PropertyProcessor(ValidationFlags, isDebug);

        foreach (var type in types)
        {
            if (type.ContainsAllowNullAttribute() || type.IsCompilerGenerated())
                continue;

            foreach (var method in type.MethodsWithBody())
                methodProcessor.Process(method);

            foreach (var property in type.ConcreteProperties())
                propertyProcessor.Process(property);
        }
    }
}