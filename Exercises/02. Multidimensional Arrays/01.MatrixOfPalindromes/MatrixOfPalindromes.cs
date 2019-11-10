using System;
using System.Linq;

class MatrixOfPalindromes
{
    static void Main(string[] args)
    {
        int[] dimensions = Console.ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();

        int rows = dimensions[0],
            cols = dimensions[1];

        string[][] matrixOfPalindromes = new string[rows][];

        for (int row = 0; row < rows; row++)
        {
            matrixOfPalindromes[row] = new string[cols];

            for (int col = 0; col < cols; col++)
            {
                matrixOfPalindromes[row][col] = $"{(char)('a' + row)}{(char)('a' + row + col)}{(char)('a' + row)}";
            }

            Console.WriteLine(string.Join(" ", matrixOfPalindromes[row]));
        }
    }
}