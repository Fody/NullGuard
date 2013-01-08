using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using NullGuard;

public partial class ModuleWeaver
{
    public ValidationFlags ValidationFlags { get; set; }

    public Action<string> LogInfo { get; set; }
    public Action<string> LogError { get; set; }
    public ModuleDefinition ModuleDefinition { get; set; }
    public IAssemblyResolver AssemblyResolver { get; set; }
    private List<TypeDefinition> types;

    public ModuleWeaver()
    {
        LogInfo = s => { };
        LogError = s => { };
        ValidationFlags = ValidationFlags.AllPublic;
    }

    public void Execute()
    {
        FindReferences();
        types = ModuleDefinition.GetTypes().ToList();
        CheckForBadAttributes();
        ProcessAssembly();
        RemoveAttributes();
        RemoveReference();
    }
}