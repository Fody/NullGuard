using System.Linq;

using Mono.Cecil;
using Mono.Cecil.Cil;

static class SymbolExtensions
{
    public static void UpdateDebugInfo(this MethodDefinition method)
    {
        var debugInfo = method?.DebugInformation;

        if (debugInfo == null || !method.HasBody || !debugInfo.HasSequencePoints)
            return;

        var methodBody = method.Body;
        var instructions = methodBody.Instructions;
        var scope = debugInfo.Scope;

        if (scope == null || instructions.Count == 0)
        {
            return;
        }

        // Step 1: check if all variables are present
        foreach (var variable in methodBody.Variables)
        {
            var hasVariable = scope.Variables.Any(_ => _.Index == variable.Index);
            if (!hasVariable)
            {
                var variableDebugInfo = new VariableDebugInformation(variable, $"__var_{variable.Index}");
                scope.Variables.Add(variableDebugInfo);
            }
        }

        // Step 2: point the first sequence point to the first instruction; this will include null guard added IL in the first instruction.
        if (debugInfo.HasSequencePoints)
        {
            var sequencePoints = debugInfo.SequencePoints;

            var s = sequencePoints?.FirstOrDefault();

            if (s?.Offset == 0)
            {
                var entry = instructions[0];
                sequencePoints[0] = new SequencePoint(entry, s.Document) { StartColumn = s.StartColumn, StartLine = s.StartLine, EndColumn = s.EndColumn, EndLine = s.EndLine };
                scope.Start = new InstructionOffset(entry);
            }
        }
    }
}