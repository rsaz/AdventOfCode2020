using System;
using System.Collections.Generic;
using System.IO;

namespace Day19
{
    class Program
    {
        public const string FILE_NAME = "data.txt";

        public static int Part1()
        {
            var satelliteData = GetDay19Input();
            var result = SatelliteMessageHelper.GetNumberOfValidMatches(satelliteData, 0);
            return result;
        }

        public static int Part2()
        {
            var lineReplacements = new Dictionary<string, string>()
            {
                { "8: 42", "8: 42 | 42 8" },
                { "11: 42 31", "11: 42 31 | 42 11 31" }
            };
            var satelliteData = GetDay19Input(lineReplacements);
            var result = SatelliteMessageHelper.GetNumberOfValidMatches(satelliteData, 0);
            return result;

        }


        static void Main(string[] args)
        {
            Console.WriteLine("Part 1: " + Part1());
            Console.WriteLine("Part 2: " + Part2());
        }

        private static SatelliteData GetDay19Input(IDictionary<string, string> lineReplacements = null)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), FILE_NAME);
            if (!File.Exists(filePath))
            {
                throw new Exception($"Cannot locate file {filePath}");
            }

            var inputLines = File.ReadAllLines(filePath);
            var result = SatelliteMessageHelper.ParseInputLines(inputLines, lineReplacements);
            return result;
        }

    }
}
