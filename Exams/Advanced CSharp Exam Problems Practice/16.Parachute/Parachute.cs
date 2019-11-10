using System;
using System.Linq;
using System.Collections.Generic;

class Parachute
{
    static void Main(string[] args)
    {
        List<string> matrix = new List<string>();

        int jumperRow = -1,
            jumperCol = -1;

        string input;

        while ((input = Console.ReadLine()) != "END")
        {
            matrix.Add(input);

            if (jumperRow == -1 && input.Contains('o'))
            {
                jumperRow = matrix.Count - 1;
                jumperCol = input.IndexOf('o');
            }
        }

        while (">-o-<".Contains(matrix[jumperRow][jumperCol]))
        {
            jumperRow++;
            jumperCol += matrix[jumperRow].Count(c => c == '>') - matrix[jumperRow].Count(c => c == '<');
        }

        switch (matrix[jumperRow][jumperCol])
        {
            case '_':
                Console.WriteLine("Landed on the ground like a boss!");
                break;
            case '~':
                Console.WriteLine("Drowned in the water like a cat!");
                break;
            case '/':
            case '|':
            case '\\':
                Console.WriteLine("Got smacked on the rock like a dog!");
                break;
        }

        Console.WriteLine($"{jumperRow} {jumperCol}");
    }
}