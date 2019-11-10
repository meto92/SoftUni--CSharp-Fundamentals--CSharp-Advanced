using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class SrubskoUnleashed
{
    static void Main(string[] args)
    {
        Regex pattern = new Regex(@"^(?<singer>[a-zA-Z]+( [a-zA-Z]+){0,2}) @(?<venue>[a-zA-Z]+(( [a-zA-Z]+){0,2})) (?<ticketsPrice>\d+) (?<ticketsCount>\d+)$");
        Dictionary<string, Dictionary<string, int>> venuesStatistics = 
            new Dictionary<string, Dictionary<string, int>>();

        string input = null;

        while ((input = Console.ReadLine()) != "End")
        {
            Match match = pattern.Match(input);

            if (!match.Success)
            {
                continue;
            }

            string singer = match.Groups["singer"].Value;
            string venue = match.Groups["venue"].Value;
            int ticketsPrice = int.Parse(match.Groups["ticketsPrice"].Value);
            int ticketsCount = int.Parse(match.Groups["ticketsCount"].Value);

            if (!venuesStatistics.ContainsKey(venue))
            {
                venuesStatistics[venue] = new Dictionary<string, int>();
            }

            if (!venuesStatistics[venue].ContainsKey(singer))
            {
                venuesStatistics[venue][singer] = 0;
            }

            venuesStatistics[venue][singer] += ticketsCount * ticketsPrice;
        }

        foreach (KeyValuePair<string, Dictionary<string, int>> venueInfo in venuesStatistics)
        {
            Console.WriteLine($"{venueInfo.Key}");

            foreach (KeyValuePair<string, int> singerInfo in 
                venueInfo.Value.OrderByDescending(p => p.Value))
            {
                string singer = singerInfo.Key;
                int money = singerInfo.Value;

                Console.WriteLine($"#  {singer} -> {money}");
            }
        }
    }
}