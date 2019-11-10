using System;
using System.Collections.Generic;
using System.Linq;

class ListOfPredicates
{
    static void Main(string[] args)
    {
        long lastNumber = long.Parse(Console.ReadLine());
        int[] divisors = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();
        
        Func<long, int, bool> isFirstDivisibleBySecond = (num, divisor) => num % divisor == 0;

        Func<long, bool> isNumberValid = num =>
        {
            foreach (int divisor in divisors)
            {
                if (!isFirstDivisibleBySecond(num, divisor))
                {
                    return false;
                }
            }

            return true;
        };

        List<long> result = new List<long>();

        for (int num = 1; num <= lastNumber; num++)
        {
            if (isNumberValid(num))
            {
                result.Add(num);
            }
        }

        Console.WriteLine(string.Join(" ", result));
    }
}