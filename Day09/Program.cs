using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day09
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("First Number: {0}", Part2());
        }

        private static List<double> Entry { get; } = File.ReadAllLines("data.txt").Select(double.Parse).ToList();

        public static double Part01()
            => FindNotASum(Entry);

        private static double FindNotASum(List<double> xMas)
        {
            var index = 25;
            double found;
            do
            {
                found = AnyOneEqualsTo(xMas.Skip(index - 25).Take(25).ToList(), xMas.Skip(index - 25).Skip(25).First());
                index++;
            } while (found.Equals(-1.0));

            return found;
        }

        private static double AnyOneEqualsTo(List<double> scanThis, double findThis)
        {
            for (var i = 0; i < scanThis.Count; i++)
            {
                var j = i + 1;
                var n = scanThis[i];
                while (j < scanThis.Count)
                {
                    var m = scanThis.Skip(j++).ElementAt(0);
                    if ((m + n).Equals(findThis)) return -1.0;
                }
            }

            return findThis;
        }

        public static double Part2()
            => SumMinAndMax(SearchingForTheSuite(Part01(), Entry, 0));

        private static double SumMinAndMax(List<double> suite) => suite.Min() + suite.Max();

        private static List<double> SearchingForTheSuite(double findThis, List<double> xMas, int initialIndex)
        {
            var sum = 0.0;
            var index = initialIndex++;
            do
            {
                sum += xMas[index++];
            } while (sum < findThis);

            return sum.Equals(findThis)
                ? xMas.GetRange(initialIndex, index - initialIndex + 1)
                : SearchingForTheSuite(findThis, xMas, initialIndex);
        }
    }
}
