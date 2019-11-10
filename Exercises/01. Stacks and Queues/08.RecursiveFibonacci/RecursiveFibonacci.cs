using System;

class RecursiveFibonacci
{
    private static long[] fibonacciNumbers;

    static long GetFibonacci(int n)
    {
        if (n == 1 || n == 2)
        {
            fibonacciNumbers[n] = 1;
            return 1;
        }
        else
        {
            if (fibonacciNumbers[n] == 0)
            {
                fibonacciNumbers[n] = GetFibonacci(n - 1) + GetFibonacci(n - 2);
            }

            return fibonacciNumbers[n];
        }
    }

    static void Main(string[] args)
    {
        int wantedFibonacciNumber = int.Parse(Console.ReadLine());

        fibonacciNumbers = new long[wantedFibonacciNumber + 1];
        
        Console.WriteLine(GetFibonacci(wantedFibonacciNumber));
    }
}