using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day02
{
    struct Rule
    {
        public string Min { get; set; }
        public string Max { get; set; }
        public string Character { get; set; }
        public string Password { get; set; }

    }

    class Program
    {


        static void Main(string[] args)
        {
            string file = "data.txt";
            List<string> passwords = new List<string>();
            List<string> data;

            using (var sr = new StreamReader(file))
            {
                data = sr.ReadToEnd().Split("\n").ToList();
            }

            // Structure the data
            List<Rule> rules = StructureData(data);

            // Validate the rules
            int total = ValidateRulesNew(rules);

            // Result
            Console.WriteLine(total);
        }

        static List<Rule> StructureData(List<string> data)
        {
            List<Rule> rules = new List<Rule>();
            int length = 0;

            string minPattern = "[0-9]{1,2}[-]";
            string maxPattern = "[-][0-9]{1,2}";
            string charPattern = "[a-z][:]";
            string pwdPattern = "[:]\\s[a-z]{1,50}";

            for (int i = 0; i < data.Count; i++)
            {
                var rule = new Rule();

                // min
                length = (Regex.Match(data[i], minPattern).Value).Length;
                rule.Min = (Regex.Match(data[i], minPattern).Value).Substring(0, length - 1);
                length = 0;

                // max
                length = (Regex.Match(data[i], maxPattern).Value).Length;
                rule.Max = (Regex.Match(data[i], maxPattern).Value).Substring(1, length - 1);
                length = 0;

                // char
                length = (Regex.Match(data[i], charPattern).Value).Length;
                rule.Character = (Regex.Match(data[i], charPattern).Value).Substring(0, length - 1);
                length = 0;

                // password
                length = (Regex.Match(data[i], pwdPattern).Value).Length;
                rule.Password = (Regex.Match(data[i], pwdPattern).Value).Substring(2, length - 2);
                length = 0;


                rules.Add(rule);
            }

            return rules;
        }

        // Challenge 01
        static int ValidateRules(List<Rule> rules)
        {
            int valids = 0;
            int charCounter = 0;

            for (int i = 0; i < rules.Count; i++)
            {
                for (int k = 0; k < rules[i].Password.Length; k++)
                {
                    if (char.Parse(rules[i].Character) == (rules[i].Password)[k])
                    {
                        charCounter++;
                    }
                }

                if (charCounter <= int.Parse(rules[i].Max) && charCounter >= int.Parse(rules[i].Min))
                {
                    valids++;
                }
                charCounter = 0;
            }

            return valids;
        }

        // Challenge 02
        static int ValidateRulesNew(List<Rule> rules)
        {
            int valids = 0;
            bool minTrue = false, maxTrue = false;

            for (int i = 0; i < rules.Count; i++)
            {
                minTrue = false;
                maxTrue = false;
                if ((rules[i].Password)[int.Parse(rules[i].Min) - 1] == char.Parse(rules[i].Character))
                {
                    minTrue = true;
                }

                if ((rules[i].Password)[int.Parse(rules[i].Max) - 1] == char.Parse(rules[i].Character))
                {
                    maxTrue = true;
                }

                if ((minTrue && !maxTrue) || (maxTrue && !minTrue))
                {
                    valids++;
                }
            }

            return valids;
        }
    }
}

