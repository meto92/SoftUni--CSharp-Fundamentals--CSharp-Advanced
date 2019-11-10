using System;
using System.Linq;

class KnightsOfHonor
{
    static void Main(string[] args)
    {
        string[] names = Console.ReadLine().Split(' ');

        Action<string> printOnNewLine = str => Console.WriteLine($"Sir {str}");

        names.ToList().ForEach(printOnNewLine);
    }
}