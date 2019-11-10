using System;
using System.Linq;

class BunkerBuster
{
    private static int[][] field;
    private static int rows;
    private static int cols;

    static void DamageField(int impactRow, int impactCol, int bombPower)
    {
        int halfBombPower = (int)Math.Ceiling(bombPower / 2.0);

        for (int row = Math.Max(0, impactRow - 1); row <= impactRow + 1 && row < rows; row++)
        {
            for (int col = Math.Max(0, impactCol - 1); col <= impactCol + 1 && col < cols; col++)
            {
                field[row][col] -= halfBombPower;
            }
        }

        field[impactRow][impactCol] -= bombPower - halfBombPower;
    }

    static int CountDestroyedBunkers()
    {
        int destroyedBunkersCount = 0;

        for (int row = 0; row < rows; row++)
        {
            destroyedBunkersCount += field[row].Count(x => x <= 0);
        }

        return destroyedBunkersCount;
    }

    static void Main(string[] args)
    {
        int[] dimensions = Console.ReadLine()            
            .Split()
            .Select(int.Parse)
            .ToArray();

        rows = dimensions[0];
        cols = dimensions[1];

        field = new int[rows][];

        for (int i = 0; i < rows; i++)
        {
            field[i] = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();
        }

        string input;

        while ((input = Console.ReadLine()) != "cease fire!")
        {
            string[] bombParams = input.Split(' ');

            int impactRow = int.Parse(bombParams[0]);
            int impactCol = int.Parse(bombParams[1]);
            int bombPower = (int)bombParams[2][0];

            DamageField(impactRow, impactCol, bombPower);
        }

        int destroyedBunkersCount = CountDestroyedBunkers();

        Console.WriteLine($"Destroyed bunkers: {destroyedBunkersCount}");
        Console.WriteLine($"Damage done: {destroyedBunkersCount * 100.0 / (rows * cols):f1} %");
    }
}