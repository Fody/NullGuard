using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;

public static class InstructionPatterns
{
    public static void CallDebugAssertInstructions(IList<Instruction> instructions, string message)
    {
        // Load null onto the stack
        instructions.Add(Instruction.Create(OpCodes.Ldnull));

        // Compare the top 2 items on the stack, and put the result back on the stack
        instructions.Add(Instruction.Create(OpCodes.Ceq));

        // Loads constant int32 0 onto the stack
        instructions.Add(Instruction.Create(OpCodes.Ldc_I4_0));

        // Compare the top 2 items on the stack, and put the result back on the stack
        instructions.Add(Instruction.Create(OpCodes.Ceq));

        // Load assert message onto the stack
        instructions.Add(Instruction.Create(OpCodes.Ldstr, message));

        // Call Debug.Assert
        instructions.Add(Instruction.Create(OpCodes.Call, ReferenceFinder.DebugAssertMethod));
    }

    public static void DuplicateReturnValue(IList<Instruction> instructions, TypeReference methodReturnType)
    {
        // Duplicate the stack (this should be the return value)
        instructions.Add(Instruction.Create(OpCodes.Dup));

        if (methodReturnType.GetElementType().IsGenericParameter)
        {
            // Generic parameters must be boxed before access
            instructions.Add(Instruction.Create(OpCodes.Box, methodReturnType));
        }
    }

    public static void LoadArgumentOntoStack(IList<Instruction> instructions, ParameterDefinition parameter)
    {
        // Load the argument onto the stack
        instructions.Add(Instruction.Create(OpCodes.Ldarg, parameter));

        var elementType = parameter.ParameterType.GetElementType();

        if (parameter.ParameterType.IsByReference)
        {
            if (elementType.IsGenericParameter)
            {
                // Loads an object reference onto the stack
                instructions.Add(Instruction.Create(OpCodes.Ldobj, elementType));

                // Box the type to an object
                instructions.Add(Instruction.Create(OpCodes.Box, elementType));
            }
            else
            {
                // Loads an object reference onto the stack
                instructions.Add(Instruction.Create(OpCodes.Ldind_Ref));
            }
        }
        else if (elementType.IsGenericParameter)
        {
            // Box the type to an object
            instructions.Add(Instruction.Create(OpCodes.Box, parameter.ParameterType));
        }
    }
}