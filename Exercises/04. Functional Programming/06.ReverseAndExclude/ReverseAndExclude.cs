using System;
using System.Collections.Generic;
using System.Linq;

class ReverseAndExclude
{
    static void Main(string[] args)
    {
        List<int> numbers = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToList();
        int divisor = int.Parse(Console.ReadLine());

        Predicate<int> isDivisible = num => num % divisor == 0;

        Action<List<int>> removeDivisibleElements = nums =>
        {
            for (int i = nums.Count - 1; i >= 0; i--)
            {
                if (isDivisible(nums[i]))
                {
                    nums.RemoveAt(i);
                }
            }
        };

        Action<List<int>> reverseIntList = nums =>
        {
            for (int i = 0; i < nums.Count / 2; i++)
            {
                int left = nums[i];

                nums[i] = nums[nums.Count - i - 1];
                nums[nums.Count - i - 1] = left;
            }
        };

        removeDivisibleElements(numbers);
        reverseIntList(numbers);

        Console.WriteLine(string.Join(" ", numbers));
    }
}