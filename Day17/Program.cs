using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day17
{
    class Vec : IEquatable<Vec>
    {
        public readonly int[] Coordinates;
        readonly int scalar;
        public Vec(params int[] scalars)
        {
            Coordinates = scalars;

            scalar = 1;
            var mul = 1;
            for (var i = 0; i < Coordinates.Length; i++)
            {
                scalar += mul * Coordinates[i];
                mul *= 500;
            }
        }

        public Vec[] GetNeighbours()
        {
            var neighbours = new List<Vec>();
            FillNeighbours(neighbours, Array.Empty<int>());
            return neighbours.ToArray();
        }

        void FillNeighbours(List<Vec> neighbours, int[] dim)
        {
            if (dim.Length == Coordinates.Length)
            {
                if (!Coordinates.SequenceEqual(dim)) neighbours.Add(new Vec(dim));
                return;
            }

            for (int i = -1; i < 2; i++)
            {
                FillNeighbours(neighbours, dim.Concat(new[] { Coordinates[dim.Length] + i }).ToArray());
            }
        }

        public bool Equals(Vec other) => other.scalar == scalar;
        public override bool Equals(object obj) => Equals(obj as Vec);
        public override int GetHashCode() => scalar;

        internal Vec Add(Vec x)
        {
            var newvec = new int[Coordinates.Length];
            for (var i = 0; i < newvec.Length; i++)
            {
                newvec[i] = Coordinates[i] + x.Coordinates[i];
            }
            return new Vec(newvec);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("data.txt");
            var timer = Stopwatch.StartNew();
            Console.WriteLine("Answer #1: " + Solve(input, 3));
            Console.WriteLine("Answer #2: " + Solve(input, 4));
            Console.WriteLine("Elapsed: " + timer.Elapsed);
        }

        public static int Solve(string[] input, int dimentions)
        {
            var activeCells = new HashSet<Vec>();

            var y = 0;
            foreach (var row in input)
            {
                for (var x = 0; x < row.Length; x++)
                {
                    if (row[x] == '#')
                    {
                        var coordinates = new int[dimentions];
                        coordinates[0] = x;
                        coordinates[1] = y;
                        activeCells.Add(new Vec(coordinates));
                    }
                }
                y++;
            }

            var nVectors = new Vec(new int[dimentions]).GetNeighbours();

            for (int i = 0; i < 6; i++)
            {
                var activeCells2 = activeCells.ToHashSet();

                var cells = activeCells.Concat(activeCells.SelectMany(x => nVectors.Select(nv => nv.Add(x)))).ToHashSet();

                foreach (var c in cells)
                {
                    var activeNeighbours = nVectors.Select(nv => nv.Add(c)).Count(n => activeCells.Contains(n));
                    if (activeCells.Contains(c))
                    {
                        if (activeNeighbours < 2 || activeNeighbours > 3) activeCells2.Remove(c);
                    }
                    else
                    {
                        if (activeNeighbours == 3) activeCells2.Add(c);
                    }
                }

                activeCells = activeCells2;
            }

            return activeCells.Count;
        }
    }
}
