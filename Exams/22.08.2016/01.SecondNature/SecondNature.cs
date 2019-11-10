using System;
using System.Linq;
using System.Collections.Generic;

class SecondNature
{
    static void Main(string[] args)
    {
        List<int> flowersDust = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToList();
        Stack<int> bucketsWater = new Stack<int>(Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray());

        List<int> secondNatureFlowers = new List<int>();

        int flowerIndex = 0;

        while (flowerIndex < flowersDust.Count && bucketsWater.Count > 0)
        {
            int flowerDust = flowersDust[flowerIndex];
            int bucketWater = bucketsWater.Pop();

            if (bucketWater == flowerDust)
            {
                flowersDust[flowerIndex++] = 0;
                secondNatureFlowers.Add(flowerDust);
            }
            else if (flowerDust < bucketWater)
            {
                flowersDust[flowerIndex++] -= bucketWater;

                if (bucketsWater.Count > 0)
                {
                    bucketsWater.Push(bucketsWater.Pop() + bucketWater - flowerDust);
                }
            }
            else
            {
                flowersDust[flowerIndex] -= bucketWater;
            }
        }

        if (bucketsWater.Any(x => x > 0))
        {
            Console.WriteLine(string.Join(" ", bucketsWater));
        }
        else
        {
            Console.WriteLine(string.Join(" ", flowersDust.Where(x => x > 0)));
        }

        if (secondNatureFlowers.Any(x => x > 0))
        {
            Console.WriteLine(string.Join(" ", secondNatureFlowers));
        }
        else
        {
            Console.WriteLine("None");
        }
    }
}