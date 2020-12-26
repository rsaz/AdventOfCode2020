using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day05
{
    struct RowsInterval
    {
        public char Digit { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
   
        public RowsInterval(char d, int min, int max)
        {
            Digit = d;
            Min = min;
            Max = max;
        }
    }

    struct ColInterval
    {
        public char Digit { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }

        public ColInterval(char d, int min, int max)
        {
            Digit = d;
            Min = min;
            Max = max;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var data = GetData();

            var rows = DiscoverRow(data);
            var cols = DiscoverCol(data);

            CalculateSeatID(rows, cols);
        }
        static List<string> GetData()
        {
            string file = "data.txt";
            List<string> data = null;

            using (var sr = new StreamReader(file))
            {
                data = sr.ReadToEnd().Split("\r\n").ToList();
            }

            return data;
        }

        static List<int> DiscoverRow(List<string> data)
        {
            List<RowsInterval> intervals = new List<RowsInterval>();
            List<int> rows = new List<int>();
            int i;

            // looping the data
            for (i = 0; i < data.Count; i++)
            {
                string line = data[i];
                int min = 0;
                int max = 127;

                // discover the rows
                for (int k = 0; k < 7; k++)
                {
                    // if is the first digit
                    if (k == 0)
                    {
                        if (line[k] == 'F')
                        {
                            min = min;
                            max = max / 2;
                        }
                        else if (line[k] == 'B')
                        {
                            min = (max / 2) + 1;
                            max = max;
                        }

                    }
                    else
                    {
                        if (line[k] == 'F')
                        {
                            min = intervals[k - 1].Min;
                            max = intervals[k-1].Max - (int)(128/ Math.Pow(2, k+1));
                        }
                        else if (line[k] == 'B')
                        {
                            min = intervals[k-1].Min + (int)(128/ Math.Pow(2, k+1));
                            max = intervals[k-1].Max;
                        }
                    }

                    RowsInterval interval = new RowsInterval(line[k], min, max);
                    intervals.Add(interval);
                }
                rows.Add(intervals[intervals.Count - 1].Max);
                intervals.Clear();
            }
            return rows;
        }

        static List<int> DiscoverCol(List<string> data)
        {
            List<ColInterval> intervals = new List<ColInterval>();
            List<int> cols = new List<int>();
            int i;

            // looping the data
            for (i = 0; i < data.Count; i++)
            {
                string line = data[i];
                int min = 0;
                int max = 7;

                // discover the cols
                for (int k = 7; k < 10; k++)
                {
                    // if is the first digit
                    if (k == 7)
                    {
                        if (line[k] == 'L')
                        {
                            min = min;
                            max = max / 2;
                        }
                        else if (line[k] == 'R')
                        {
                            min = (max / 2) + 1;
                            max = max;
                        }
                    }
                    else
                    {
                        if (line[k] == 'L')
                        {
                            min = intervals[k - 8].Min;
                            max = intervals[k - 8].Max - (int)(8 / Math.Pow(2, k - 6));
                        }
                        else if (line[k] == 'R')
                        {
                            min = intervals[k - 8].Min + (int)(8 / Math.Pow(2, k - 6));
                            max = intervals[k - 8].Max;
                        }
                    }

                    ColInterval interval = new ColInterval(line[k], min, max);
                    intervals.Add(interval);
                }
                cols.Add(intervals[intervals.Count - 1].Max);
                intervals.Clear();
            }
            return cols;
        }

        static void CalculateSeatID(List<int> rows, List<int> cols)
        {
            List<int> ids = new List<int>();

            for (int i = 0; i < rows.Count; i++)
            {
                ids.Add((rows[i] * 8) + cols[i]);
            }

            Console.WriteLine($"Max Seat ID is: {ids.Max()}");

            int lastID = -1;
            ids.Sort();
            foreach (var id in ids)
            {
                if (lastID != -1 && id - lastID == 2)
                {
                    Console.WriteLine(id - 1);
                }
                lastID = id;
            }
        }

        static void SimpleBinarySolution(List<string> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                string line = data[i];
                string binary = string.Empty;

                for (int k = 0; k < line.Length; k++)
                {
                    if (line[k] == 'F')
                    {
                        binary += 0;
                    }
                    else
                    {
                        binary += 1;
                    }
                }

                Console.WriteLine(Convert.ToInt32(binary, 2));
            }
        }
    }
}
