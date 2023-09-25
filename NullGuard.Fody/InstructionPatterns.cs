public partial class ModuleWeaver
{
    public void CallDebugAssertInstructions(List<Instruction> instructions, string message)
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
        instructions.Add(Instruction.Create(OpCodes.Call, DebugAssertMethod));
    }

    public static void DuplicateReturnValue(List<Instruction> instructions, TypeReference methodReturnType)
    {
        // Duplicate the stack (this should be the return value)
        instructions.Add(Instruction.Create(OpCodes.Dup));

        if (methodReturnType != null)
        {
            PrepareStackValue(instructions, methodReturnType);
        }
    }

    public static void LoadArgumentOntoStack(List<Instruction> instructions, ParameterDefinition parameter)
    {
        // Load the argument onto the stack
        instructions.Add(Instruction.Create(OpCodes.Ldarg, parameter));

        PrepareStackValue(instructions, parameter.ParameterType);
    }

    public void LoadArgumentNullException(List<Instruction> instructions, string valueName, string errorString)
    {
        // Load the name of the argument onto the stack
        instructions.Add(Instruction.Create(OpCodes.Ldstr, valueName));

        // Load the exception text onto the stack
        instructions.Add(Instruction.Create(OpCodes.Ldstr, errorString));

        // Load the ArgumentNullException onto the stack
        instructions.Add(Instruction.Create(OpCodes.Newobj, ArgumentNullExceptionWithMessageConstructor));
    }

    public void LoadInvalidOperationException(List<Instruction> instructions, string errorString)
    {
        // Load the exception text onto the stack
        instructions.Add(Instruction.Create(OpCodes.Ldstr, errorString));

        // Load the InvalidOperationException onto the stack
        instructions.Add(Instruction.Create(OpCodes.Newobj, InvalidOperationExceptionConstructor));
    }

    public void IfNull(List<Instruction> instructions, Instruction returnInstruction, Action<List<Instruction>> trueBlock)
    {
        // Branch if value on stack is true, not null or non-zero
        instructions.Add(Instruction.Create(OpCodes.Brtrue_S, returnInstruction));

        trueBlock(instructions);
    }

    private static void PrepareStackValue(List<Instruction> instructions, TypeReference valueType)
    {
        var elementType = valueType.GetElementType();

        if (valueType.IsByReference)
        {
            if (elementType.NeedsBoxing())
            {
                // Loads an object reference onto the stack
                instructions.Add(Instruction.Create(OpCodes.Ldobj, elementType));
                // Box the type to an object
                instructions.Add(Instruction.Create(OpCodes.Box, elementType));
                return;
            }

            // Loads an object reference onto the stack
            instructions.Add(Instruction.Create(OpCodes.Ldind_Ref));
            return;
        }

        if (elementType.NeedsBoxing())
        {
            // Box the type to an object
            instructions.Add(Instruction.Create(OpCodes.Box, valueType));
        }
    }
}