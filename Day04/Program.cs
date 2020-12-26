using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = GetData();

            var dataFiltered = FilterData(data);

            Console.WriteLine("Source: " +data.Count);
            Console.WriteLine("Valid Passports: " +dataFiltered.Count);
        }

        static List<string> GetData()
        {
            string file = "data.txt";
            List<string> data = null;

            using (var sr = new StreamReader(file))
            {
                data = sr.ReadToEnd().Split("\r\n\r\n").ToList();
            }

            return data;
        }

        static List<string> FilterData(List<string> data)
        {
            var filtered = new List<string>();
            bool newPassport = false;

            for (int i = 0; i < data.Count; i++)
            {
                newPassport = false;

                List<string> elements = new List<string>();

                elements = data[i].Split(null).ToList();

                for (int j = 0; j < elements.Count; j++)
                {
                    if (elements[j] == "")
                    {
                        elements.RemoveAt(j);
                    }
                }

                //validating the fields
                bool fieldValidation = (elements.Count == 7
                    && Regex.IsMatch(data[i], @"(^|)byr:(|)")
                    && Regex.IsMatch(data[i], @"(^|)iyr:(|)")
                    && Regex.IsMatch(data[i], @"(^|)eyr:(|)")
                    && Regex.IsMatch(data[i], @"(^|)hgt:(|)")
                    && Regex.IsMatch(data[i], @"(^|)hcl:(|)")
                    && Regex.IsMatch(data[i], @"(^|)ecl:(|)")
                    && Regex.IsMatch(data[i], @"(^|)pid:(|)")
                    || elements.Count == 8
                    && Regex.IsMatch(data[i], @"(^|)byr:(|)")
                    && Regex.IsMatch(data[i], @"(^|)iyr:(|)")
                    && Regex.IsMatch(data[i], @"(^|)eyr:(|)")
                    && Regex.IsMatch(data[i], @"(^|)hgt:(|)")
                    && Regex.IsMatch(data[i], @"(^|)hcl:(|)")
                    && Regex.IsMatch(data[i], @"(^|)ecl:(|)")
                    && Regex.IsMatch(data[i], @"(^|)pid:(|)")
                    && Regex.IsMatch(data[i], @"(^|)cid:(|)"));

                // validating the data
                string key = string.Empty;
                string value = string.Empty;
                
                if (fieldValidation)
                {
                    for (int k = 0; k < elements.Count; k++)
                    {
                        if (newPassport)
                        {
                            k = 0;
                            break;
                        }
                        
                        key = Regex.Match(elements[k], "[a-z]{3}[:]").Value;
                        int index = elements[k].IndexOf(":") + 1;
                        value = elements[k].Substring(index, elements[k].Length - index);

                        switch (key)
                        {
                            case "pid:":
                                if (!Regex.IsMatch(value, "[0-9]{9}") || value.Length != 9)
                                {
                                    newPassport = true;
                                    break;
                                }
                                continue;
                            case "byr:":
                                if (int.Parse(value) < 1920 || int.Parse(value) > 2002)
                                {
                                    newPassport = true;
                                    break;
                                }
                                continue;
                            case "iyr:":
                                if (int.Parse(value) < 2010 || int.Parse(value) > 2020)
                                {
                                    newPassport = true;
                                    break;
                                }
                                continue;
                            case "eyr:":
                                if (int.Parse(value) < 2020 || int.Parse(value) > 2030)
                                {
                                    newPassport = true;
                                    break;
                                }
                                continue;
                            case "hgt:":
                                if (!Regex.IsMatch(value, "[0-9]{1,3}in|[0-9]{1,3}cm"))
                                {
                                    newPassport = true;
                                    break;
                                }

                                string measure = Regex.Match(value, "in|cm").Value;
                                int n = int.Parse(Regex.Match(value, "[0-9]{1,3}").Value);
                                switch (measure)
                                {
                                    case "in":
                                        if (n < 59 || n > 76)
                                        {
                                            newPassport = true;
                                            break;
                                        }
                                        continue;
                                    case "cm":
                                        if (n < 150 || n > 193)
                                        {
                                            newPassport = true;
                                            break;
                                        }
                                        continue;
                                }
                                continue;
                            case "hcl:":
                                if (!Regex.IsMatch(value, "#[a-f0-9]{6}|#[0-9a-f]{6}"))
                                {
                                    newPassport = true;
                                    break;
                                }
                                continue;
                            case "ecl:":
                                if (!Regex.IsMatch(value, "amb|blu|brn|gry|grn|hzl|oth"))
                                {
                                    newPassport = true;
                                    break;
                                }
                                continue;
                        }
                    }
                    if (!newPassport)
                    {
                        filtered.Add(data[i]);
                    }
                }
            }

            return filtered;
        }
    }
}
