using System;
using System.Linq;
using System.Collections.Generic;

class BasicQueueOperations
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

        Queue<int> queue = new Queue<int>();

        for (int i = 0; i < elementsToPush; i++)
        {
            queue.Enqueue(elements[i]);
        }

        for (int i = 0; i < elementsToPop && queue.Any(); i++)
        {
            queue.Dequeue();
        }

        if (queue.Count == 0)
        {
            Console.WriteLine(0);
            return;
        }

        int smallestElement = queue.Peek();

        while (queue.Count > 0)
        {
            int element = queue.Dequeue();

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