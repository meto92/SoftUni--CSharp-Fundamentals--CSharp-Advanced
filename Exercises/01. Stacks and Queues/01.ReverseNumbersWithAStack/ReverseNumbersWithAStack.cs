using System;
using System.Linq;
using System.Collections.Generic;

class ReverseNumbersWithAStack
{
    static void Main(string[] args)
    {
        Stack<int> numsStack = new Stack<int>(
            Console.ReadLine()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray());

        Console.WriteLine(string.Join(" ", numsStack));
    }
}