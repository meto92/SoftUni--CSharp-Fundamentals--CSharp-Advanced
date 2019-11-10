using System;
using System.Linq;
using System.Collections.Generic;

class BasicStackOperations
{
    static void Main(string[] args)
    {
        int[] nums = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();
        int[] elements = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        int elementsToPush = nums[0],
            elementsToPop = nums[1],
            numberToCheck = nums[2];

        Stack<int> stack = new Stack<int>();

        for (int i = 0; i < elementsToPush; i++)
        {
            stack.Push(elements[i]);
        }

        for (int i = 0; i < elementsToPop && stack.Any(); i++)
        {
            stack.Pop();
        }

        if (stack.Count == 0)
        {
            Console.WriteLine(0);
            return;
        }

        int smallestElement = stack.Peek();

        while (stack.Count > 0)
        {
            int element = stack.Pop();

            if (element == numberToCheck)
            {
                Console.WriteLine("true");
                return;
            }
            else if (element < smallestElement)
            {
                smallestElement = element;
            }
        }

        Console.WriteLine(smallestElement);
    }
}