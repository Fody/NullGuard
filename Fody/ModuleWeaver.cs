using System;
using System.Collections.Generic;
using Mono.Cecil;
using NullGuard;

public partial class ModuleWeaver
{
    public ValidationFlags ValidationFlags { get; set; }

    public Action<string> LogInfo { get; set; }
    public Action<string> LogError { get; set; }
    public ModuleDefinition ModuleDefinition { get; set; }
    public IAssemblyResolver AssemblyResolver { get; set; }
    List<TypeDefinition> types;

    public ModuleWeaver()
    {
        LogInfo = s => { };
        LogError = s => { };
        ValidationFlags = ValidationFlags.AllPublic;
    }

    public void Execute()
    {
        var nullGuardAttribute = ModuleDefinition.GetNullGuardAttribute();

        if (nullGuardAttribute == null)
            nullGuardAttribute = ModuleDefinition.Assembly.GetNullGuardAttribute();

        if (nullGuardAttribute != null)
            ValidationFlags = (ValidationFlags)nullGuardAttribute.ConstructorArguments[0].Value;

        FindReferences();
        types = new List<TypeDefinition>(ModuleDefinition.GetTypes());
        CheckForBadAttributes();
        ProcessAssembly();
        RemoveAttributes();
        RemoveReference();
    }
}