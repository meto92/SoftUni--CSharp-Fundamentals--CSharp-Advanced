using System;
using System.Collections.Generic;

class HotPotato
{
    static void Main(string[] args)
    {
        Queue<string> children = new Queue<string>(Console.ReadLine().Split(' '));
        int tosses = int.Parse(Console.ReadLine());

        while (children.Count > 1)
        {
            for (int i = 1; i < tosses; i++)
            {
                children.Enqueue(children.Dequeue());
            }

            Console.WriteLine($"Removed {children.Dequeue()}");
        }

        Console.WriteLine($"Last is {children.Peek()}");
    }
}