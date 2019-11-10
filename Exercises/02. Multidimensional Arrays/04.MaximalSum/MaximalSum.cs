using System;
using System.Linq;
using System.Text.RegularExpressions;

class MaximalSum
{
    static void Main(string[] args)
    {
        int[] dimensions = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        int rows = dimensions[0],
            cols = dimensions[1];

        long[][] matrix = new long[rows][];

        for (int row = 0; row < rows; row++)
        {
            matrix[row] = Regex.Split(Console.ReadLine().Trim(), "\\s+")
                .Select(long.Parse)
                .ToArray();
        }

        long bestSum = long.MinValue;
        int[] bestSumIndices = { 0, 0 };

        for (int row = 0; row < rows - 2; row++)
        {
            for (int col = 0; col < cols - 2; col++)
            {
                long currentSum = 0;

                for (int r = row; r < row + 3; r++)
                {
                    currentSum += matrix[r].Skip(col).Take(3).Sum();
                }

                if (currentSum > bestSum)
                {
                    bestSum = currentSum;
                    bestSumIndices[0] = row;
                    bestSumIndices[1] = col;
                }
            }
        }

        Console.WriteLine($"Sum = {bestSum}");

        for (int row = bestSumIndices[0]; row < bestSumIndices[0] + 3; row++)
        {
            Console.WriteLine(string.Join(" ", matrix[row].Skip(bestSumIndices[1]).Take(3)));
        }
    }
}