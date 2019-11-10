using System;
using System.Linq;

class SumMatrixElements
{
    static void Main(string[] args)
    {
        int[] dimensions = Console.ReadLine()
            .Split(new[] { ", " }, StringSplitOptions.None)
            .Select(int.Parse)
            .ToArray();

        int rows = dimensions[0],
            cols = dimensions[1];

        int sum = 0;
        int[][] matrix = new int[rows][];

        for (int row = 0; row < rows; row++)
        {
            int[] rowElements = Console.ReadLine()
                .Split(new[] { ", " }, StringSplitOptions.None)
                .Select(int.Parse)
                .ToArray();

            matrix[row] = rowElements;
            sum += rowElements.Sum();
        }

        Console.WriteLine(rows);
        Console.WriteLine(cols);
        Console.WriteLine(sum);
    }
}