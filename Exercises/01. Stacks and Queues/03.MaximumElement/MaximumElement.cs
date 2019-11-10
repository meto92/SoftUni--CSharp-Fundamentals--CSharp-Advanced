using System;
using System.Linq;
using System.Collections.Generic;

class MaximumElement
{
    static void Main(string[] args)
    {
        int lines = int.Parse(Console.ReadLine());

        Stack<int> stack = new Stack<int>();
        Stack<int> maxNumbers = new Stack<int>();
        
        for (int i = 0; i < lines; i++)
        {
            int[] query = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            switch (query[0])
            {
                case 1:
                    stack.Push(query[1]);

                    if (maxNumbers.Count == 0 || maxNumbers.Peek() < query[1])
                    {
                        maxNumbers.Push(query[1]);
                    }

                    break;
                case 2:
                    if (stack.Pop() == maxNumbers.Peek())
                    {
                        maxNumbers.Pop();
                    }

                    break;
                case 3:
                    Console.WriteLine(maxNumbers.Peek());
                    break;
            }
        }
    }
}