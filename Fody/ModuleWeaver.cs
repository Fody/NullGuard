using System;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;
using NullGuard;

public partial class ModuleWeaver
{
    public ValidationFlags ValidationFlags { get; set; }

    public string[] DefineConstants { get; set; }
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
        DefineConstants = new string[0];
    }

    public void Execute()
    {
        var nullGuardAttribute = ModuleDefinition.GetNullGuardAttribute();

        if (nullGuardAttribute == null)
            nullGuardAttribute = ModuleDefinition.Assembly.GetNullGuardAttribute();

        if (nullGuardAttribute != null)
            ValidationFlags = (ValidationFlags)nullGuardAttribute.ConstructorArguments[0].Value;

        ReferenceFinder.FindReferences(AssemblyResolver, ModuleDefinition);
        types = new List<TypeDefinition>(ModuleDefinition.GetTypes());
        CheckForBadAttributes();
        ProcessAssembly();
        RemoveAttributes();
        RemoveReference();
    }

    public Instruction[] CallDebugAssertInstructions(string message)
    {
        return new Instruction[] {
            // Load null onto the stack
            Instruction.Create(OpCodes.Ldnull),

            // Compare the top 2 items on the stack, and put the result back on the stack
            Instruction.Create(OpCodes.Ceq),

            // Loads constant int32 0 onto the stack
            Instruction.Create(OpCodes.Ldc_I4_0),

            // Compare the top 2 items on the stack, and put the result back on the stack
            Instruction.Create(OpCodes.Ceq),

            // Load assert message onto the stack
            Instruction.Create(OpCodes.Ldstr, message),

            // Call Debug.Assert
            Instruction.Create(OpCodes.Call, ReferenceFinder.DebugAssertMethod)
        };
    }
}