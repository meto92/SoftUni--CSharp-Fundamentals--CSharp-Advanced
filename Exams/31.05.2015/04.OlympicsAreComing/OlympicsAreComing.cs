using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class OlympicsAreComing
{
    static void ReadInput(Dictionary<string, Dictionary<string, int>> countriesInfo)
    {
        string input;

        while ((input = Console.ReadLine()) != "report")
        {
            string[] inputParams = input.Split('|');

            string athlete = Regex.Replace(inputParams[0].Trim().Replace('\t', ' '), "\\s{2,}", " ");
            string country = Regex.Replace(inputParams[1].Trim().Replace('\t', ' '), "\\s{2,}", " ");

            if (!countriesInfo.ContainsKey(country))
            {
                countriesInfo[country] = new Dictionary<string, int>();
            }

            if (!countriesInfo[country].ContainsKey(athlete))
            {
                countriesInfo[country][athlete] = 0;
            }

            countriesInfo[country][athlete]++;
        }
    }

    static void PrintResult(Dictionary<string, Dictionary<string, int>> countriesInfo)
    {
        foreach (KeyValuePair<string, Dictionary<string, int>> pair in countriesInfo.OrderByDescending(p => p.Value.Values.Sum()))
        {
            string country = pair.Key;
            int participantsCount = pair.Value.Values.Count;
            int wins = pair.Value.Values.Sum();

            Console.WriteLine($"{country} ({participantsCount} participants): {wins} wins");
        }
    }

    static void Main(string[] args)
    {
        Dictionary<string, Dictionary<string, int>> countriesInfo = new Dictionary<string, Dictionary<string, int>>();

        ReadInput(countriesInfo);
        PrintResult(countriesInfo);
    }
}