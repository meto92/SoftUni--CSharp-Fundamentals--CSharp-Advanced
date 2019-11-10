using System;
using System.Linq;

class MaximumSumOf2x2Submatrix
{
    static void Main(string[] args)
    {
        int[] dimensions = Console.ReadLine()
            .Split(new[] { ", " }, StringSplitOptions.None)
            .Select(int.Parse)
            .ToArray();

        int rows = dimensions[0],
            cols = dimensions[1];

        int[][] matrix = new int[rows][];

        for (int row = 0; row < rows; row++)
        {
            int[] rowElements = Console.ReadLine()
                .Split(new[] { ", " }, StringSplitOptions.None)
                .Select(int.Parse)
                .ToArray();

            matrix[row] = rowElements;
        }

        int bestSum = matrix[0][0] + matrix[0][1] + matrix[1][0] + matrix[1][1];
        int[] bestSumIndices = { 0, 0 };
            
        for (int row = 0; row < rows - 1; row++)
        {
            for (int col = 0; col < cols - 1; col++)
            {
                int currentSum = matrix[row][col] + matrix[row][col + 1] + matrix[row + 1][col] + matrix[row + 1][col + 1];

                if (currentSum > bestSum)
                {
                    bestSum = currentSum;
                    bestSumIndices[0] = row;
                    bestSumIndices[1] = col;
                }
            }
        }

        Console.WriteLine(string.Join(" ", matrix[bestSumIndices[0]].Skip(bestSumIndices[1]).Take(2)));        
        Console.WriteLine(string.Join(" ", matrix[bestSumIndices[0] + 1].Skip(bestSumIndices[1]).Take(2)));
        Console.WriteLine(bestSum);
    }
}