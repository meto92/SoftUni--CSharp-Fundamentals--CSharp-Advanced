using System;
using System.Collections.Generic;

class StackFibonacci
{
    static void Main(string[] args)
    {
        int wantedFibonacciNumber = int.Parse(Console.ReadLine());

        Stack<long> fibonacciNumbers = new Stack<long>(new long[] { 0, 1 });

        for (int i = 2; i <= wantedFibonacciNumber; i++)
        {
            long oldPeek = fibonacciNumbers.Pop();
            long newPeek = oldPeek + fibonacciNumbers.Peek();

            fibonacciNumbers.Push(oldPeek);
            fibonacciNumbers.Push(newPeek);
        }

        Console.WriteLine(fibonacciNumbers.Peek());
    }
}