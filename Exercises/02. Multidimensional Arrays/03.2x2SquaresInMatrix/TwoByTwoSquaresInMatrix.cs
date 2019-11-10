using System;
using System.Linq;
using System.Text.RegularExpressions;

class TwoByTwoSquaresInMatrix
{
    static void Main(string[] args)
    {
        int[] dimensions = (Console.ReadLine())
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        int rows = dimensions[0],
            cols = dimensions[1];
        
        char[][] matrix = new char[rows][];

        for (int row = 0; row < rows; row++)
        {
            matrix[row] = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c[0])
                .ToArray();
        }

        int squaresOfEqualCharsCount = 0;

        for (int row = 0; row < rows - 1; row++)
        {
            for (int col = 0; col < cols - 1; col++)
            {
                char c = matrix[row][col];

                if (c == matrix[row][col + 1] &&
                    c == matrix[row + 1][col] &&
                    c == matrix[row + 1][col + 1])
                {
                    squaresOfEqualCharsCount++;
                }
            }
        }

        Console.WriteLine(squaresOfEqualCharsCount);
    }
}