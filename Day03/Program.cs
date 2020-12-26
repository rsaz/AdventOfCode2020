using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the data
            var data = GetData();

            char[,] map = new char[323,31];

            for (int i = 0; i < data.Count; i++)
            {
                var line = data[i];
                
                for (int j = 0; j < line.Length - 1; j++)
                {
                    map[i,j] = line[j];
                }
            }

            int slopeX = 1;
            int slopeY = 2;
            int x = 0;
            int y = 0;

            int trees = 0;

            while (y < data.Count)
            {
                char at = map[y, x];
                if (at == '#')
                {
                    trees++;
                }
                x = (x + slopeX) % 31;
                y += slopeY;
            }

            Console.WriteLine($"Trees: {trees}");
        }

        static List<string> GetData()
        {
            string file = "data.txt";
            List<string> passwords = new List<string>();
            List<string> data = null;

            using (var sr = new StreamReader(file))
            {
                data = sr.ReadToEnd().Split("\n").ToList();
            }

            return data;
        }
    }
}
