using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class AshesOfRoses
{
    static void Main(string[] args)
    {
        SortedDictionary<string, SortedDictionary<string, long>> rosesStatistics = 
            new SortedDictionary<string, SortedDictionary<string, long>>();
        Regex pattern = new Regex(@"^Grow <([A-Z][a-z]*)> <([a-zA-Z\d]+)> (\d+)$");
        string input = null;

        while ((input = Console.ReadLine()) != "Icarus, Ignite!")
        {
            Match match = pattern.Match(input);

            if (!match.Success)
            {
                continue;
            }

            string region = match.Groups[1].Value;
            string color = match.Groups[2].Value;
            int rosesAmount = int.Parse(match.Groups[3].Value);

            if (!rosesStatistics.ContainsKey(region))
            {
                rosesStatistics[region] = new SortedDictionary<string, long>();
            }

            if (!rosesStatistics[region].ContainsKey(color))
            {
                rosesStatistics[region][color] = 0;
            }

            rosesStatistics[region][color] += rosesAmount;
        }

        foreach (KeyValuePair<string, SortedDictionary<string, long>> pair 
            in rosesStatistics.OrderByDescending(p => p.Value.Values.Sum()))
        {
            string region = pair.Key;

            Console.WriteLine(region);

            foreach (KeyValuePair<string, long> rosesAmountByColor 
                in pair.Value.OrderBy(p => p.Value))
            {
                string color = rosesAmountByColor.Key;
                long rosesAmount = rosesAmountByColor.Value;

                Console.WriteLine($"*--{color} | {rosesAmount}");
            }
        }
    }
}