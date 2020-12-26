using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = GetData();

            //var common = FindCharacterCommon(data.ToArray(), (data.ToArray()).Length);
            // common.ForEach(Console.WriteLine);

            var total = ValidAnswers(data);

            Console.WriteLine($"Total {total.Sum()}");
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

        static List<int> ValidAnswers(List<string> data)
        {
            List<int> answersYes = new List<int>();
            List<string> group = new List<string>();
            string concat = string.Empty;

            for (int i = 0; i < data.Count; i++)
            {
                string line = data[i];

                if (line != string.Empty)
                {
                    group.Add(line);
                    continue;
                }
                
                if (line == string.Empty)
                {
                    if (group.Count == 1)
                    {
                        var counter = group[0].Length;
                        answersYes.Add(counter);
                        group.Clear();
                    }
                    else // compare asnwers
                    {
                        var common = FindCharacterCommon(group.ToArray(), (group.ToArray()).Length);
                        answersYes.Add(common.Count);
                        group.Clear();
                    }
                    //var counter = concat.Distinct().ToList();
                    //answersYes.Add(counter.Count);
                    //concat = string.Empty;

                }
                //concat += line;
            }

            return answersYes;
        }

        static List<char> FindCharacterCommon(string[] group, int n)
        {
            bool[] primary = new bool[26];

            for (int i = 0; i < primary.Length; i++)
            {
                primary[i] = true;
            }

            // for each string
            for (int i = 0; i < n; i++)
            {
                bool[] common = new bool[26];

                for (int j = 0; j < common.Length; j++)
                {
                    common[j] = false;
                }

                // for every char in each string
                for (int k = 0; k < group[i].Length; k++)
                {
                    if (primary[group[i][k] - 'a'])
                    {
                        common[group[i][k] - 'a'] = true;
                    }
                }

                // copy common array into the first array
                Array.Copy(common, 0, primary, 0, 26);
            }

            // store common characters
            List<char> commonChar = new List<char>();
            for (int o = 0; o < 26; o++)
            {
                if (primary[o])
                {
                    commonChar.Add((char)(o + 97));
                }
            }

            return commonChar;
        }
    }
}
