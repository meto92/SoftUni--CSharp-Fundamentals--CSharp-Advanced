using System;
using System.Linq;
using System.Collections.Generic;

class PredicateParty
{
    static void Main(string[] args)
    {
        Func<string, string, Predicate<string>> getPredicate = (filterType, filterParameter) =>
        {
            Predicate<string> predicate = null;

            switch (filterType)
            {
                case "StartsWith":
                    predicate = str => str.StartsWith(filterParameter);
                    break;
                case "EndsWith":
                    predicate = str => str.EndsWith(filterParameter);
                    break;
                case "Length":
                    int length = int.Parse(filterParameter);

                    predicate = str => str.Length == length;
                    break;
            }

            return predicate;
        };

        Action<List<string>, Predicate<string>> doubleElements = (list, predicate) =>
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    list.Insert(i + 1, list[i]);
                    i++;
                }
            }
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

        Func<string, Predicate<string>, Action<List<string>, Predicate<string>>> getAction = (command, predicate) =>
        {
            Action<List<string>, Predicate<string>> action = null;

            if (command == "Double")
            {
                 action =  doubleElements;
            }
            else if (command == "Remove")
            {
                action = removeElements;
            }

            return action;
        };

        List<string> comingPeople = Console.ReadLine().Split(' ').ToList();

        string input = null;

        while ((input = Console.ReadLine()) != "Party!")
        {
            string[] commandArgs = input.Split();

            string command = commandArgs[0];
            string filterType = commandArgs[1];
            string filterParameter = commandArgs[2];

            Predicate<string> predicate = getPredicate(filterType, filterParameter);
            Action<List<string>, Predicate<string>> action = getAction(command, predicate);

            action(comingPeople, predicate);
        }

        Console.WriteLine("{0} going to the party!", 
            comingPeople.Count == 0
            ? "Nobody is"
            : string.Join(", ", comingPeople) + " are");
    }
}