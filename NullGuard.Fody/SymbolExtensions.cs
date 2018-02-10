using System.Linq;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

static class SymbolExtensions
{
    static FieldInfo SequencePointOffsetFieldInfo = typeof(SequencePoint).GetField("offset", BindingFlags.Instance | BindingFlags.NonPublic);
    static FieldInfo InstructionOffsetInstructionFieldInfo = typeof(InstructionOffset).GetField("instruction", BindingFlags.Instance | BindingFlags.NonPublic);

    public static void UpdateDebugInfo(this MethodDefinition method)
    {
        if (!method.HasBody || !method.DebugInformation.HasSequencePoints)
            return;

        var debugInfo = method.DebugInformation;
        var instructions = method.Body.Instructions;
        var scope = debugInfo.Scope;

        if (scope == null || instructions.Count == 0)
        {
            return;
        }

        var oldSequencePoints = debugInfo.SequencePoints;
        var newSequencePoints = new Collection<SequencePoint>();

        // Step 1: check if all variables are present
        foreach (var variable in method.Body.Variables)
        {
            var hasVariable = scope.Variables.Any(x => x.Index == variable.Index);
            if (!hasVariable)
            {
                var variableDebugInfo = new VariableDebugInformation(variable, $"__var_{variable.Index}");
                scope.Variables.Add(variableDebugInfo);
            }
        }

        // Step 2: Make sure the instructions point to the correct items
        foreach (var oldSequencePoint in oldSequencePoints)
        {
            var instructionOffset = (InstructionOffset)SequencePointOffsetFieldInfo.GetValue(oldSequencePoint);
            var offsetInstruction = (Instruction)InstructionOffsetInstructionFieldInfo.GetValue(instructionOffset);

            // Fix offset
            for (var i = 0; i < instructions.Count; i++)
            {
                var instruction = instructions[i];
                if (instruction == offsetInstruction)
                {
                    var newSequencePoint = new SequencePoint(instruction, oldSequencePoint.Document)
                    {
                        StartLine = oldSequencePoint.StartLine,
                        StartColumn = oldSequencePoint.StartColumn,
                        EndLine = oldSequencePoint.EndLine,
                        EndColumn = oldSequencePoint.EndColumn
                    };

                    newSequencePoints.Add(newSequencePoint);
                    break;
                }
            }
        }

        debugInfo.SequencePoints.Clear();

        foreach (var newSequencePoint in newSequencePoints)
        {
            debugInfo.SequencePoints.Add(newSequencePoint);
        }

        // Step 3: update the scopes by setting the indices
        scope.Start = new InstructionOffset(instructions.First());
        scope.End = new InstructionOffset(instructions.Last());
    }

    public static void HideLineFromDebugger(this MethodDefinition method, Instruction i, Document doc)
    {
        if (doc == null || !method.HasBody || !method.DebugInformation.HasSequencePoints)
            return;

        // This tells the debugger to ignore and step through
        // all the following instructions to the next instruction
        // with a valid SequencePoint. That way IL can be hidden from
        // the Debugger. See
        // http://blogs.msdn.com/b/abhinaba/archive/2005/10/10/479016.aspx
        var hiddenSequencePoint = new SequencePoint(i, doc)
        {
            StartLine = 0xfeefee,
            EndLine = 0xfeefee
        };

        method.DebugInformation.SequencePoints.Add(hiddenSequencePoint);
    }
}