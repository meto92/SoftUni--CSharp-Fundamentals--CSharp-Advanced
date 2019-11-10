using System;

class PredicateForNames
{
    static void Main(string[] args)
    {
        int maxLength = int.Parse(Console.ReadLine());
        string[] names = Console.ReadLine().Split(' ');

        Predicate<string> hasRequiredLength = name => name.Length <= maxLength;

        Action<string[]> printFittingNames = namesArray =>
        {
            foreach (string name in namesArray)
            {
                if (hasRequiredLength(name))
                {
                    Console.WriteLine(name);
                }
            }
        };

        printFittingNames(names);
    }
}