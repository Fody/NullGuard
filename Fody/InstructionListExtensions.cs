using System.Collections.Generic;
using System.Linq;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

public static class InstructionListExtensions
{
    public static void Prepend(this Collection<Instruction> collection, IEnumerable<Instruction> instructions)
    {
        var index = 0;
        foreach (var instruction in instructions)
        {
            collection.Insert(index, instruction);
            index++;
        }
    }

    public static int Insert(this Collection<Instruction> collection, int index, IEnumerable<Instruction> instructions)
    {
        foreach (var instruction in instructions.Reverse())
        {
            collection.Insert(index, instruction);
        }
        return index + instructions.Count();
    }
}