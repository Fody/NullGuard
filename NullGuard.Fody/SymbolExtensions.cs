using System.Linq;

using Mono.Cecil;
using Mono.Cecil.Cil;

static class SymbolExtensions
{
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

        // Step 2: update the scopes by setting the indices
        scope.Start = new InstructionOffset(instructions.First());
        scope.End = new InstructionOffset(instructions.Last());
    }
}