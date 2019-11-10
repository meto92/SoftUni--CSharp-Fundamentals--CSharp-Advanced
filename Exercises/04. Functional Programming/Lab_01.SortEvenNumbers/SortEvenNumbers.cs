using System;
using System.Linq;

class SortEvenNumbers
{
    static void Main(string[] args)
    {
        Console.WriteLine(string.Join(", ",
            Console.ReadLine()
                .Split(new[] { ", " }, StringSplitOptions.None)
                .Select(int.Parse)
                .Where(x => x % 2 == 0)
                .OrderBy(x => x)));
    }
}