using System;
using System.Linq;
using System.Collections.Generic;

class PoisonousPlants
{
    static void Main(string[] args)
    {
        int plantsCount = int.Parse(Console.ReadLine());
        int[] plants = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        Stack<int> prevPlantsIndices = new Stack<int>();
        int[] daysPlantsDied = new int[plantsCount];

        prevPlantsIndices.Push(0);

        for (int i = 1; i < plantsCount; i++)
        {
            int lastDay = 0;

            while (prevPlantsIndices.Count() > 0 && plants[prevPlantsIndices.Peek()] >= plants[i])
            {
                lastDay = Math.Max(lastDay, daysPlantsDied[prevPlantsIndices.Pop()]);
            }

            if (prevPlantsIndices.Count() > 0)
            {
                daysPlantsDied[i] = lastDay + 1;
            }

            prevPlantsIndices.Push(i);
        }

        Console.WriteLine(daysPlantsDied.Max());
    }
}