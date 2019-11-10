using System;
using System.Linq;
using System.Collections.Generic;

class EvenBeforeOddIntComparer : Comparer<int>
{
    Func<int, int, bool> areBothEvenOrOdd = (x, y) =>
    {
        return (x % 2 == 0 && y % 2 == 0) ||
            (x % 2 != 0 && y % 2 != 0);
    };

    Func<int, bool> isEven = x => x % 2 == 0;

    public override int Compare(int x, int y)
    {
        if (areBothEvenOrOdd(x, y))
        {
            return x.CompareTo(y);
        }

        if (isEven(x))
        {
            return -1;
        }

        return 1;
    }
}

class CustomComparator
{
    static void Main(string[] args)
    {
        int[] numbers = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        Array.Sort(numbers, new EvenBeforeOddIntComparer());

        Console.WriteLine(string.Join(" ", numbers));
    }
}