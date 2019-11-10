using System;
using System.Linq;
using System.Collections.Generic;

class TriFunction
{
    static void MainFunction()
    {
        Func<string, int, bool> func = (str, minCharsSum) =>
        {
            return str.ToCharArray().Sum(c => (int)c) >= minCharsSum;
        };

        Func<IEnumerable<string>, int, Func<string, int, bool>, string> getFirstElementThatSufficesThrRequirement = (collection, minSum, f) =>
        {
            foreach (string str in collection)
            {
                if (f(str, minSum))
                {
                    return str;
                }
            }

            return string.Empty;
        };

        int requiredMinCharsSum = int.Parse(Console.ReadLine());
        string[] names = Console.ReadLine().Split(' ');

        Console.WriteLine(getFirstElementThatSufficesThrRequirement(names, requiredMinCharsSum, func));
    }

    static void Main(string[] args)
    {
        MainFunction();
    }
}