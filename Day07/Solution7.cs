using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day07
{
    struct Bag
    {
        public int Quantity { get; set; }
        public string Name { get; set; }

        public Bag(int quantity, string name)
        {
            Quantity = quantity;
            Name = name;
        }
    }

    class Solution7
    {
        Dictionary<string, List<Bag>> bags = new Dictionary<string, List<Bag>>();
        Dictionary<string, bool> bagTemp = new Dictionary<string, bool>();

        // get system data
        public List<string> GetData()
        {
            string file = "data.txt";
            List<string> data = null;

            using (var sr = new StreamReader(file))
            {
                data = sr.ReadToEnd().Split("\r\n").ToList();
            }

            return data;
        }

        public void FindInnerAndOutterBags(List<string> data)
        {
            foreach (var d in data)
            {
                List<string> split = Regex.Split(d, "bags contain").ToList();
                string outterBag = split[0].Trim();
                
                List<string> innerBags = split[1].Split(",").ToList();

                // cleaning names
                for (int i = 0; i < innerBags.Count; i++)
                {
                    innerBags[i] = innerBags[i].Replace("bags", "")
                        .Replace("bag", "")
                        .Replace(".", "")
                        .Replace(" .", "");
                    innerBags[i] = innerBags[i].Trim();
                }

                List<Bag> bagInfos = new List<Bag>();

                foreach (var innerBag in innerBags)
                {
                    if (!(innerBag.Substring(0, 2)).Equals("no"))
                    {
                        int quantity = int.Parse(innerBag.Substring(0, 1));
                        string name = innerBag.Substring(2);
                        Bag bag = new Bag(quantity, name);
                        bagInfos.Add(bag);
                    }
                }
                bags.Add(outterBag, bagInfos);
            }

            // finding the bags that contains the shiny bags
            int count = 0;

            foreach (var bag in bags.Keys)
            {
                if (IsGoldBagInBag(bag))
                {
                    count++;
                }
            }

            int finalCount = 0;
            foreach (var bag in bagTemp.Keys)
            {
                if (bagTemp[bag] == true && bag != "shiny gold")
                {
                    finalCount++;
                }
            }
            Console.WriteLine($"Final Count: {finalCount/2}");
        }

        public bool IsGoldBagInBag(string bag)
        {
            if (bagTemp.ContainsKey(bag))
            {
                return (bagTemp.TryGetValue(bag, out bool value));
            }

            foreach (var subBag in bags[bag])
            {
                if (subBag.Name.Equals("shiny gold"))
                {
                    bagTemp.Add(bag, true);
                    return true;
                }
            }

            foreach (var subBag in bags[bag])
            {
                if (IsGoldBagInBag(subBag.Name))
                {
                    bagTemp.Add(bag, true);
                    return true;
                }
            }

            bagTemp.Add(bag, false);
            return false;
        }

        public int CountGoldBags(string bag)
        {
            int count = 0;
            List<Bag> bagList = bags[bag];
            
            foreach (var bagInfo in bagList)
            {
                count += (bagInfo.Quantity * CountGoldBags(bagInfo.Name));
            }

            return count;
        }
    }
}
