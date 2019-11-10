using System;
using System.Collections.Generic;

class ReverseStrings
{
    static void Main(string[] args)
    {
        Console.WriteLine(
            new string(
                new Stack<char>(
                    Console.ReadLine().ToCharArray())
                .ToArray()));
    }
}