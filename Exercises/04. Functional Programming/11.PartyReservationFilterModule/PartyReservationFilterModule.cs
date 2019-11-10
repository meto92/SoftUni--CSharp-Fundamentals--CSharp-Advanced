using System;
using System.Linq;
using System.Collections.Generic;

class PartyReservationFilterModule
{
    static void Main(string[] args)
    {
        Func<string, string, Predicate<string>> getPredicate = (filterType, filterParameter) =>
        {
            Predicate<string> predicate = null;

            switch (filterType)
            {
                case "Starts with":
                    predicate = str => str.StartsWith(filterParameter);
                    break;
                case "Ends with":
                    predicate = str => str.EndsWith(filterParameter);
                    break;
                case "Contains":
                    predicate = str => str.Contains(filterParameter);
                    break;
                case "Length":
                    int length = int.Parse(filterParameter);

                    predicate = str => str.Length == length;
                    break;
            }

            return predicate;
        };

        Action<List<string>, Predicate<string>> removeElements = (list, predicate) =>
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                }
            }
        };

        List<string> invitedPeople = Console.ReadLine().Split(' ').ToList();

        Dictionary<string, Dictionary<string, Predicate<string>>> filters =
            new Dictionary<string, Dictionary<string, Predicate<string>>>();
        string input = null;

        while ((input = Console.ReadLine()) != "Print")
        {
            string[] commandArgs = input.Split(';');

            string command = commandArgs[0];
            string filterType = commandArgs[1];
            string filterParameter = commandArgs[2];

            if (command == "Add filter")
            {
                Predicate<string> predicate = getPredicate(filterType, filterParameter);

                if (!filters.ContainsKey(filterType))
                {
                    filters[filterType] = new Dictionary<string, Predicate<string>>();
                }

                filters[filterType][filterParameter] = predicate;
            }
            else if (command == "Remove filter")
            {
                filters[filterType].Remove(filterParameter);
            }
        }

        foreach (Dictionary<string, Predicate<string>> pairs in filters.Values)
        {
            foreach (Predicate<string> predicate in pairs.Values)
            {
                removeElements(invitedPeople, predicate);
            }
        }

        Console.WriteLine(string.Join(" ", invitedPeople));
    }
}