using System.Linq;

public partial class ModuleWeaver
{
    
    public void RemoveReference()
    {
        var referenceToRemove = ModuleDefinition.AssemblyReferences.FirstOrDefault(x => x.Name == "NullGuard");
        if (referenceToRemove == null)
        {
            LogInfo("\tNo reference to 'NullGuard.dll' found. References not modified.");
            return;
        }

        ModuleDefinition.AssemblyReferences.Remove(referenceToRemove);
        LogInfo("\tRemoving reference to 'NullGuard.dll'.");
    }
}