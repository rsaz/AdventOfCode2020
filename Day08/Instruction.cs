using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08
{
    public enum OperationType
    {
        acc, jmp, nop
    }

    class Instruction
    {
        public static int Accumulator { get; set; } = 0;

        public int Index { get; private set; }
        private int Counter { get; set; } = 0;
        private int Argument { get; set; }
        private OperationType Operation { get; set; }
        public bool IsJumpOrNop => Operation != OperationType.acc;
        public bool HasNotBeenInvertedYet { get; private set; } = true;
        public bool HasBeenExecuted => Counter > 0;

        public static Instruction Build(string line, int index)
        {
            var components = line.Split(" ");
            return new Instruction
            {
                Index = index,
                Operation = (OperationType)Enum.Parse(typeof(OperationType), components[0]),
                Argument = int.Parse(components[1])
            };
        }

        public int Execute()
        {
            if (Counter == 1) return -1;

            Counter++;
            return this.Operation switch
            {
                OperationType.acc => Accumulate(),
                OperationType.jmp => Index + Argument,
                OperationType.nop => Index + 1,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private int Accumulate()
        {
            Instruction.Accumulator += Argument;
            return Index + 1;
        }

        public override string ToString()
            => $"{Index} - {Operation} - arg: {Argument}";

        public void InvertOperation()
        {
            switch (Operation)
            {
                case OperationType.jmp:
                    Operation = OperationType.nop;
                    HasNotBeenInvertedYet = false;
                    return;
                case OperationType.nop:
                    Operation = OperationType.jmp;
                    HasNotBeenInvertedYet = false;
                    return;
                case OperationType.acc:
                default: return;
            }
        }

        public void ResetCounter()
            => Counter = 0;
    }
}
