using System;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

public class MethodProcessor
{
    public ModuleWeaver ModuleWeaver;
    public TypeSystem TypeSystem;
    public MethodDefinition Method;
    MethodBody body;

    public void Process()
    {
        try
        {
            InnerProcess();
        }
        catch (Exception exception)
        {
            throw new WeavingException(string.Format("An error occurred processing '{0}'. Error: {1}", Method.FullName, exception.Message));
        }
    }

    void InnerProcess()
    {
        body = Method.Body;
        body.SimplifyMacros();

        var instructions = body.Instructions;
        var index = 0;
        foreach (var parameterDefinition in Method.Parameters.Where(x=>!x.ContainsAllowNullAttribute()))
        {
            var branchOutNop = Instruction.Create(OpCodes.Nop);
            instructions.Insert(index, Instruction.Create(OpCodes.Ldarg, parameterDefinition));
            index++;
            instructions.Insert(index, Instruction.Create(OpCodes.Brtrue_S, branchOutNop));
            index++;
            instructions.Insert(index, Instruction.Create(OpCodes.Ldstr, parameterDefinition.Name));
            index++;
            instructions.Insert(index, Instruction.Create(OpCodes.Newobj, ModuleWeaver.ArgumentNullExceptionConstructor));
            index++;
            instructions.Insert(index, Instruction.Create(OpCodes.Throw));
            index++;
            instructions.Insert(index, branchOutNop);
            index++;
        }

        body.InitLocals = true;
        body.OptimizeMacros();
    }

}