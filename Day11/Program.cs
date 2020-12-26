using System;
using System.IO;
using System.Linq;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("data.txt");
            var data = input.Select(l => l.ToArray()).ToArray();
            var next = data.Select(l => new char[l.Length]).ToArray();

            int neighbors_core(int r, int c, Func<(int dr, int dc), (int r, int c)> extend)
            {
                var count = 0;
                for (int i = -1; i < 2; ++i)
                {
                    if (r + i < 0 || r + i >= data.Length) continue;
                    for (int j = -1; j < 2; ++j)
                    {
                        if (c + j < 0 || c + j >= data[0].Length) continue;
                        if (i == 0 && j == 0) continue;
                        (var r0, var c0) = extend((i, j));
                        if (data[r0][c0] == '#')
                            count++;
                    }
                }
                return count;
            }
            int neighbors(int r, int c) => neighbors_core(r, c, d => (r + d.dr, c + d.dc));
            int visible_neighbors(int r, int c) => neighbors_core(r, c, d =>
            {
                var a = 1;
                do
                {
                    if (r + d.dr * (a + 1) < 0 || r + d.dr * (a + 1) >= data.Length) break;
                    if (c + d.dc * (a + 1) < 0 || c + d.dc * (a + 1) >= data[0].Length) break;
                    a++;
                } while (data[r + d.dr * a][c + d.dc * a] == '.');
                return (r + d.dr * a, c + d.dc * a);
            });

            bool step(Func<int, int, int> neigh, int tolerance)
            {
                bool changed = false;
                for (int r = 0; r < input.Length; ++r)
                {
                    var row = data[r];
                    for (int c = 0; c < row.Length; ++c)
                    {
                        var n = neigh(r, c);
                        next[r][c] = row[c] switch
                        {
                            'L' when n == 0 => '#',
                            '#' when n > tolerance => 'L',
                            _ => row[c]
                        };
                        if (next[r][c] != row[c])
                        {
                            changed = true;
                        }
                    }
                }
                return changed;
            }

            void sim(Func<int, int, int> neigh, int tolerance)
            {
                while (step(neigh, tolerance))
                {
                    var temp = next;
                    next = data;
                    data = temp;
                }
            }
            sim(neighbors, 3);
            var part1 = data.Sum(r => r.Count(c => c == '#'));
            Console.WriteLine($"Part 1: {part1}");

            data = input.Select(l => l.ToArray()).ToArray();
            next = data.Select(l => new char[l.Length]).ToArray();
            sim(visible_neighbors, 4);

            var part2 = data.Sum(r => r.Count(c => c == '#'));
            Console.WriteLine($"Part 2: {part2}");

        }
    }
}
