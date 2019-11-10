using System;
using System.Linq;
using System.Collections.Generic;

class CubicAssault
{
    const string Green = "Green";
    const string Red = "Red";
    const string Black = "Black";
    const int Million = 1000000;

    static void Main(string[] args)
    {
        SortedDictionary<string, SortedDictionary<string, long>> meteorsStatistics = 
            new SortedDictionary<string, SortedDictionary<string, long>>();
        string input = null;

        while ((input = Console.ReadLine()) != "Count em all")
        {
            string[] inputParams = input.Split(new[] { " -> " }, StringSplitOptions.None);

            string regionName = inputParams[0];
            string meteorType = inputParams[1];
            int meteorsCount = int.Parse(inputParams[2]);

            if (!meteorsStatistics.ContainsKey(regionName))
            {
                meteorsStatistics[regionName] = new SortedDictionary<string, long>
                {
                    [Green] = 0,
                    [Red] = 0,
                    [Black] = 0
                };
            }

            meteorsStatistics[regionName][meteorType] += meteorsCount;

            if (meteorType == Green && meteorsStatistics[regionName][Green] >= Million)
            {
                long newRedMeteors = meteorsStatistics[regionName][Green] / Million;

                meteorsStatistics[regionName][Green] -= newRedMeteors * Million;
                meteorsStatistics[regionName][Red] += newRedMeteors;
            }

            if (meteorsStatistics[regionName][Red] >= Million)
            {
                long newBlackMeteors = meteorsStatistics[regionName][Red] / Million;

                meteorsStatistics[regionName][Red] -= newBlackMeteors * Million;
                meteorsStatistics[regionName][Black] += newBlackMeteors;
            }
        }

        foreach (KeyValuePair<string, SortedDictionary<string, long>> pair 
            in meteorsStatistics
                .OrderByDescending(p => p.Value[Black])
                .ThenBy(p => p.Key.Length))
        {
            Console.WriteLine(pair.Key);

            foreach (KeyValuePair<string, long> countByMeteorType 
                in pair.Value.OrderByDescending(p => p.Value))
            {
                string meteorType = countByMeteorType.Key;
                long meteorsCount = countByMeteorType.Value;

                Console.WriteLine($"-> {meteorType} : {meteorsCount}");
            }
        }
    }
}