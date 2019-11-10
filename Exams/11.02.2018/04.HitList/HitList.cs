using System;
using System.Linq;
using System.Collections.Generic;

class HitList
{
    static void Main(string[] args)
    {
        int targetInfoIndex = int.Parse(Console.ReadLine());

        Dictionary<string, SortedDictionary<string, string>> peopleInfo = 
            new Dictionary<string, SortedDictionary<string, string>>();

        string input = null;

        while ((input = Console.ReadLine()) != "end transmissions")
        {
            string[] inputParams = input.Split('=');
            string name = inputParams[0];
            string[] keyValues = inputParams[1].Split(';');

            if (!peopleInfo.ContainsKey(name))
            {
                peopleInfo[name] = new SortedDictionary<string, string>();
            }

            foreach (string keyValue in keyValues)
            {
                string[] pair = keyValue.Split(':');
                string key = pair[0];
                string value = pair[1];

                peopleInfo[name][key] = value;
            }
        }

        string requestedPersonName = Console.ReadLine().Split()[1];

        Console.WriteLine($"Info on {requestedPersonName}:");
        Console.WriteLine(string.Join(Environment.NewLine,
            peopleInfo[requestedPersonName].Select(pair => $"---{pair.Key}: {pair.Value}")));

        int infoIndex = peopleInfo[requestedPersonName].Keys.Select(key => key.Length).Sum() +
            peopleInfo[requestedPersonName].Values.Select(value => value.Length).Sum();

        Console.WriteLine($"Info index: {infoIndex}");

        if (infoIndex >= targetInfoIndex)
        {
            Console.WriteLine("Proceed");
        }
        else
        {
            int infoNeeded = targetInfoIndex - infoIndex;

            Console.WriteLine($"Need {infoNeeded} more info.");
        }
    }
}