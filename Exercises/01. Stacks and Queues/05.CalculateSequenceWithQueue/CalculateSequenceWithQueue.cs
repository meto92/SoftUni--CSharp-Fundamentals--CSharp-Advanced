using System;
using System.Collections.Generic;

class CalculateSequenceWithQueue
{
    static void Main(string[] args)
    {
        long firstElement = long.Parse(Console.ReadLine());

        Queue<long> queue = new Queue<long>();
        List<long> sequence = new List<long>();

        queue.Enqueue(firstElement);
    
        for (int i = 0; i < 50; i++)
        {
            long peek = queue.Dequeue();

            queue.Enqueue(peek + 1);
            queue.Enqueue(2 * peek + 1);
            queue.Enqueue(peek + 2);

            sequence.Add(peek);
        }

        Console.WriteLine(string.Join(" ", sequence));
    }
}