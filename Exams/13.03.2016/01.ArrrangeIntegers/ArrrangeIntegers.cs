using System;
using System.Linq;
using System.Collections.Generic;

class NumberAsStringComparer : Comparer<int>
{
    Dictionary<char, string> stringByDigit = new Dictionary<char, string>()
        {
            { '0', "zero" },
            { '1', "one" },
            { '2', "two" },
            { '3', "three" },
            { '4', "four" },
            { '5', "five" },
            { '6', "six" },
            { '7', "seven" },
            { '8', "eight" },
            { '9', "nine" }
        };

    public override int Compare(int x, int y)
    {
        string xStr = x.ToString();
        string yStr = y.ToString();

        int minLength = Math.Min(xStr.Length, yStr.Length);

        for (int i = 0; i < minLength; i++)
        {
            if (stringByDigit[xStr[i]] != stringByDigit[yStr[i]])
            {
                return stringByDigit[xStr[i]].CompareTo(stringByDigit[yStr[i]]);
            }
        }

        return xStr.Length.CompareTo(yStr.Length);
    }
}

class ArrrangeIntegers
{
    static void Main(string[] args)
    {
        int[] numbers = Console.ReadLine()
            .Split(new[] { ", " }, StringSplitOptions.None)
            .Select(int.Parse)
            .ToArray();

        Array.Sort(numbers, new NumberAsStringComparer());

        Console.WriteLine(string.Join(", ", numbers));
    }
}