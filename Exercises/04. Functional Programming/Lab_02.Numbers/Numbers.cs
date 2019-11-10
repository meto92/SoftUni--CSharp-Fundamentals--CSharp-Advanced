using System;
using System.Linq;

class Numbers
{
    static void Main(string[] args)
    {
        int[] numbers = Console.ReadLine()
            .Split(new[] { ", " }, StringSplitOptions.None)
            .Select(int.Parse)
            .ToArray();

        Console.WriteLine(numbers.Length);
        Console.WriteLine(numbers.Sum());
    }
}