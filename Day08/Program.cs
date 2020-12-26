using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day08
{
    class Program
    {
        static void Main(string[] args)
        {
            int accumulator = Part02();           

            Console.WriteLine("Accumulator: {0}", accumulator);
        }

        static List<string> GetData() => File.ReadLines("data.txt").ToList();

        static int Part01()
            => Compute(File.ReadAllLines("data.txt").Select(Instruction.Build).ToList());

        private static int Compute(List<Instruction> instructions)
        {
            var doNext = instructions[0].Execute();
            while (doNext > -1)
                doNext = instructions[doNext].Execute();

            return Instruction.Accumulator;
        }

        static int Part02()
            => ComputeAndExitNormally(File.ReadAllLines("data.txt").Select(Instruction.Build).ToList());

        private static int ComputeAndExitNormally(List<Instruction> instructions)
        {
            Instruction.Accumulator = 0;
            var doNext = instructions[0].Execute();
            var executedInstruction = new List<Instruction> { instructions[0] };
            Instruction instructionToChange = null;
            while (doNext < instructions.Count)
            {
                executedInstruction.Insert(0, instructions[doNext]);
                doNext = instructions[doNext].Execute();

                if (doNext != -1) continue;

                //Reset previously changed
                instructionToChange?.InvertOperation();

                //Find last jmp or nop or the one before or the one before...
                instructionToChange = executedInstruction.FirstOrDefault(i => i.IsJumpOrNop && i.HasNotBeenInvertedYet);

                //Invert operation
                instructionToChange?.InvertOperation();

                //Restart from top
                doNext = 0;
                executedInstruction = new List<Instruction> { instructions[0] };
                Instruction.Accumulator = 0;
                instructions.Where(i => i.HasBeenExecuted).ToList().ForEach(i => i.ResetCounter());
            }
            return Instruction.Accumulator;
        }

    }
}
