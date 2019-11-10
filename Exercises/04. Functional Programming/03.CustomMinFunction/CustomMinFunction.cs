using System;
using System.Linq;
using System.Collections.Generic;

class CustomMinFunction
{
    static void Main(string[] args)
    {
        Func<IEnumerable<int>, int> min = numbers =>
        {
            int minNumber = int.MaxValue;

            foreach (int number in numbers)
            {
                if (number < minNumber)
                {
                    minNumber = number;
                }
            }

            return minNumber;
        };

        Console.WriteLine(min(Console.ReadLine().Split(' ').Select(int.Parse)));
    }
}