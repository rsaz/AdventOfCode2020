using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            //var solution = new Solution7();
            //var data = solution.GetData();

            //solution.FindInnerAndOutterBags(data);

            AnotherSolution asol = new AnotherSolution();
            //int total = asol.FindShinyGoldBags();

            int total = asol.FindBagsQuantity();
         

            Console.WriteLine($"Total: {total}");
        }
    }
}
