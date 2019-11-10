using System;
using System.Linq;

class NaturesProphet
{
    static void Bloom(int[,] garden, int row, int col)
    {
        int rows = garden.GetLength(0),
            cols = garden.GetLength(1);

        for (int r = 0; r < rows; r++)
        {
            garden[r, col]++;
        }

        for (int c = 0; c < cols; c++)
        {
            garden[row, c]++;
        }

        garden[row, col]--;
    }

    static void Main(string[] args)
    {
        int[] dimensions = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        int rows = dimensions[0],
            cols = dimensions[1];

        int[,] garden = new int[rows, cols];

        string input = null;

        while ((input = Console.ReadLine()) != "Bloom Bloom Plow")
        {
            int[] plantingFlowerCoordinates = input
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

            int row = plantingFlowerCoordinates[0],
                col = plantingFlowerCoordinates[1];

            Bloom(garden, row, col);
        }

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Console.Write($"{garden[row, col]} ");
            }

            Console.WriteLine();
        }
    }
}