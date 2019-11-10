using System;
using System.Linq;

class DiagonalDifference
{
    static void Main(string[] args)
    {
        int squareMatrixSize = int.Parse(Console.ReadLine());

        long[][] squareMatrix = new long[squareMatrixSize][];

        long primaryDiagonalSum = 0;
        long secondaryDiagonalSum = 0;

        for (int row = 0; row < squareMatrixSize; row++)
        {
            squareMatrix[row] = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();

            primaryDiagonalSum += squareMatrix[row][row];
            secondaryDiagonalSum += squareMatrix[row][squareMatrixSize - row - 1];
        }

        Console.WriteLine(Math.Abs(primaryDiagonalSum - secondaryDiagonalSum));
    }
}