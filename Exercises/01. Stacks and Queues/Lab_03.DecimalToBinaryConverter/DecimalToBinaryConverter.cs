using System;
using System.Collections.Generic;

class DecimalToBinaryConverter
{
    static void Main(string[] args)
    {
        int decimalNumber = int.Parse(Console.ReadLine());

        if (decimalNumber == 0)
        {
            Console.WriteLine(0);
            return;
        }

        Stack<int> remainders = new Stack<int>();

        while (decimalNumber > 0)
        {
            remainders.Push(decimalNumber % 2);
            decimalNumber /= 2;
        }

        Console.WriteLine(string.Join("", remainders));
    }
}