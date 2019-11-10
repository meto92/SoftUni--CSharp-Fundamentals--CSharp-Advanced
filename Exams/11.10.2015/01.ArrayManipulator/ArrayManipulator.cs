using System;
using System.Linq;
using System.Collections.Generic;

class ArrayManipulator
{
    static int[] Exchange(int[] nums, int index)
    {
        if (index < 0 || index >= nums.Length)
        {
            Console.WriteLine("Invalid index");
            return nums;
        }

        int[] exchangedArray = new int[nums.Length];

        Array.Copy(nums, index + 1, exchangedArray, 0, nums.Length - index - 1);
        Array.Copy(nums, 0, exchangedArray, nums.Length - index - 1, index + 1);

        return exchangedArray;

        //return nums.Skip(index + 1)
        //    .Take(nums.Length - index - 1)
        //    .Concat(nums.Take(index + 1))
        //    .ToArray();
    }

    static void PrintLastMaxTypeElementIndex(int[] nums, bool even)
    {
        int index = -1;

        if (even && nums.Any(x => x % 2 == 0))
        {
            int maxEvenElement = nums.Where(x => x % 2 == 0).Max();

            index = Array.LastIndexOf(nums, maxEvenElement);
        }
        else if (!even &&  nums.Any(x => x % 2 != 0))
        {
            int maxOddElement = nums.Where(x => x % 2 != 0).Max();

            index = Array.LastIndexOf(nums, maxOddElement);
        }

        if (index == -1)
        {
            Console.WriteLine("No matches");
        }
        else
        {
            Console.WriteLine(index);
        }
    }

    static void PrintLastMinTypeElementIndex(int[] nums, bool even)
    {
        int index = -1;

        if (even && nums.Any(x => x % 2 == 0))
        {
            int minEvenElement = nums.Where(x => x % 2 == 0).Min();

            index = Array.LastIndexOf(nums, minEvenElement);
        }
        else if (!even && nums.Any(x => x % 2 != 0))
        {
            int minOddElement = nums.Where(x => x % 2 != 0).Min();

            index = Array.LastIndexOf(nums, minOddElement);
        }

        if (index == -1)
        {
            Console.WriteLine("No matches");
        }
        else
        {
            Console.WriteLine(index);
        }
    }

    static void PrintFirstCountTypeElements(int[] nums, int count, bool even)
    {
        if (count > nums.Length)
        {
            Console.WriteLine("Invalid count");
            return;
        }

        IEnumerable<int> elements = nums
            .Where(x => even
                ? x % 2 == 0
                : x % 2 != 0)
            .Take(count);

        Console.WriteLine($"[{string.Join(", ", elements)}]");
    }

    static void PrintLastCountTypeElements(int[] nums, int count, bool even)
    {
        if (count > nums.Length)
        {
            Console.WriteLine("Invalid count");
            return;
        }

        IEnumerable<int> elements = nums
            .Reverse()
            .Where(x => even
                ? x % 2 == 0
                : x % 2 != 0)
            .Take(count)
            .Reverse();

        Console.WriteLine($"[{string.Join(", ", elements)}]");
    }

    static void Main(string[] args)
    {
        int[] nums = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        int count = -1;
        bool even = false;
        string command = null;
        const string evenStr = "even";

        while ((command = Console.ReadLine()) != "end")
        {
            string[] commandParams = command.Split(' ');

            switch (commandParams[0])
            {
                case "exchange":
                    int index = int.Parse(commandParams[1]);
                    nums = Exchange(nums, index);
                    break;
                case "max":
                    even = commandParams[1] == evenStr;
                    PrintLastMaxTypeElementIndex(nums, even);
                    break;
                case "min":
                    even = commandParams[1] == evenStr;
                    PrintLastMinTypeElementIndex(nums, even);
                    break;
                case "first":
                    count = int.Parse(commandParams[1]);
                    even = commandParams[2] == evenStr;
                    PrintFirstCountTypeElements(nums, count, even);
                    break;
                case "last":
                    count = int.Parse(commandParams[1]);
                    even = commandParams[2] == evenStr;
                    PrintLastCountTypeElements(nums, count, even);
                    break;
            }
        }

        Console.WriteLine($"[{string.Join(", ", nums)}]");
    }    
}