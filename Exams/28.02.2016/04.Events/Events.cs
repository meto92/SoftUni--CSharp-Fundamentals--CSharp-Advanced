using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Events
{
    static bool IsTimeValid(Match match)
    {
        double hours = double.Parse(match.Groups[3].Value);
        double minutes = double.Parse(match.Groups[4].Value);

        return hours < 24 && minutes < 60;
    }

    static void Main(string[] args)
    {
        Dictionary<string, Dictionary<string, List<DateTime>>> events = 
            new Dictionary<string, Dictionary<string, List<DateTime>>>();
        Regex pattern = new Regex(@"^#([a-zA-Z]+):\s*@([a-zA-Z]+)\s*(\d+):(\d+)$");

        int eeventsCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < eeventsCount; i++)
        {
            string input = Console.ReadLine();

            Match match = pattern.Match(input);

            if (!match.Success || !IsTimeValid(match))
            {
                continue;
            }

            string person = match.Groups[1].Value;
            string location = match.Groups[2].Value;
            int hours = int.Parse(match.Groups[3].Value);
            int minutes = int.Parse(match.Groups[4].Value);

            if (!events.ContainsKey(location))
            {
                events[location] = new Dictionary<string, List<DateTime>>();
            }

            if (!events[location].ContainsKey(person))
            {
                events[location][person] = new List<DateTime>();
            }

            events[location][person].Add(new DateTime(2000, 1, 1, hours, minutes, 0));
        }

        string[] requestedLocations = Console.ReadLine().Split(',');

        foreach (string location in requestedLocations.OrderBy(p => p))
        {
            Console.WriteLine($"{location}:");

            if (!events.ContainsKey(location))
            {
                continue;
            }

            int personCounter = 0;

            foreach (KeyValuePair<string, List<DateTime>> personInfo 
                in events[location].OrderBy(p => p.Key))
            {
                string person = personInfo.Key;
                IEnumerable<DateTime> sortedDates = personInfo.Value.OrderBy(date => date);

                Console.WriteLine("{0}. {1} -> {2}",
                    ++personCounter,
                    person,
                    string.Join(", ",
                        sortedDates.Select(date => $"{date.Hour:d2}:{date.Minute:d2}")));
            }
        }
    }
}