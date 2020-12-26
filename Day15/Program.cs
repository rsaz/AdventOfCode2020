using System;
using System.Linq;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Part 1: " + GuessingGame(2020, 15, 5, 1, 4, 7, 0));
            Console.WriteLine("Part 2: " + GuessingGame(30000000, 15, 5, 1, 4, 7, 0));
        }

        static int GuessingGame(int nth, params int[] startNums)
        {
            int position = 0;
            int lastNumber = startNums.Last();
            var positions = new int[nth];
            foreach (var number in startNums)
                positions[number] = ++position;

            while (position < nth)
            {
                int lastPosition = positions[lastNumber];
                int nextNumber = lastPosition != 0 ? position - lastPosition : 0;
                positions[lastNumber] = position++;
                lastNumber = nextNumber;
            }

            return lastNumber;
        }
    }
}
