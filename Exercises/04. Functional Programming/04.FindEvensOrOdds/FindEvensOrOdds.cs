using System;
using System.Linq;
using System.Collections.Generic;

class FindEvensOrOdds
{
    static void Main(string[] args)
    {
        int[] nums = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();
        bool even = Console.ReadLine() == "even";

        int min = nums[0],
            max = nums[1];

        List<int> result = new List<int>();
        Predicate<int> predicate = num => even ? num % 2 == 0 : num % 2 != 0;

        for (int num = min; num <= max; num++)
        {
            if (predicate(num))
            {
                result.Add(num);
            }
        }

        Console.WriteLine(string.Join(" ", result));
    }
}