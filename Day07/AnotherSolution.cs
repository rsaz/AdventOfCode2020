using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07
{
    class AnotherSolution
    {
        public int FindShinyGoldBags()
         => LookForAll(File.ReadAllLines("data.txt").ToList(), "shiny gold")
            .Distinct().Count();

        private IEnumerable<string> LookForAll(List<string> data, string search)
        {
            var found = data.Where(d => d.Contains(search))
                .Select(d => d.Split(" bags contain ")[0].Trim())
                .Where(b => !b.Equals(search)).ToList();

            return found.Union((found.Any()) ? found.SelectMany(f => LookForAll(data, f)) : new List<string>());
        }

        public int FindBagsQuantity()
            => LookForAllQty(File.ReadAllLines("data.txt").ToList(), "shiny gold");


        private int LookForAllQty(List<string> data, string search)
        {
            static int ExtractQty(string b) => Convert.ToInt32(b.Split(" ")[0]);
            static string ExtractName(string b) => string.Join(' ', b.Split(" ").Skip(1));

            var bags = data.Where(d => d.StartsWith(search))
                .Select(b => b.Split(" bags contain ")[1].Replace(".", ""))
                .SelectMany(l => l.Split(", "))
                .Where(b => !b.Contains("no other bags"))
                .Select(b => b.Replace("bags", "").Replace("bag", "").Trim())
                .Select(b => (ExtractQty(b), ExtractName(b))).ToList();

            return bags.Any() ? bags.Select(b => b.Item1 + (b.Item1 * LookForAllQty(data, b.Item2))).Sum() : 0;
        }
    }
}
