using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using MoreLinq;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1();
        }

        static void Part1()
        {
            var jolts = File.ReadLines("data.txt")
            .Select(int.Parse)
            .ToImmutableSortedSet();

            jolts = jolts.Add(0).Add(jolts.Max + 3);

            var diffCounts = jolts
              .Window(2)
              .GroupBy(js => js[^1] - js[0])
              .ToImmutableDictionary(diff => diff.Key, diff => diff.Count());

            var routes = new Dictionary<int, long> { { 0, 1 } };

            jolts.Skip(1).ForEach(jolt =>
            {
                routes[jolt] = routes.GetValueOrDefault(jolt - 1) +
                               routes.GetValueOrDefault(jolt - 2) +
                               routes.GetValueOrDefault(jolt - 3);
            });

            Console.WriteLine(diffCounts[1] * diffCounts[3]);
            Console.WriteLine(routes[jolts.Max]);
        }
    }
}
