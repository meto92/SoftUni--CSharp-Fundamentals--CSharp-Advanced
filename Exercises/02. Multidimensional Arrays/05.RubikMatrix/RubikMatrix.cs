using System;
using System.Linq;

class RubikMatrix
{
    static void ShuffleRowLeft(int[,] matrix, int row, int moves)
    {
        int cols = matrix.GetLength(1);

        for (int i = 0; i < moves; i++)
        {
            int temp = matrix[row, 0];

            for (int col = 0; col < cols - 1; col++)
            {
                matrix[row, col] = matrix[row, col + 1];
            }

            matrix[row, cols - 1] = temp;
        }
    }

    static void ShuffleRowRight(int[,] matrix, int row, int moves)
    {
        int cols = matrix.GetLength(1);

        for (int i = 0; i < moves; i++)
        {
            int temp = matrix[row, cols - 1];

            for (int col = cols - 1; col > 0; col--)
            {
                matrix[row, col] = matrix[row, col - 1];
            }

            matrix[row, 0] = temp;
        }
    }

    static void ShuffleColumnUp(int[,] matrix, int col, int moves)
    {
        int rows = matrix.GetLength(0);

        for (int i = 0; i < moves; i++)
        {
            int temp = matrix[0, col];

            for (int row = 0; row < rows - 1; row++)
            {
                matrix[row, col] = matrix[row + 1, col];
            }

            matrix[rows - 1, col] = temp;
        }
    }

    static void ShuffleColumnDown(int[,] matrix, int col, int moves)
    {
        int rows = matrix.GetLength(0);

        for (int i = 0; i < moves; i++)
        {
            int temp = matrix[rows - 1, col];

            for (int row = rows - 1; row > 0; row--)
            {
                matrix[row, col] = matrix[row - 1, col];
            }

            matrix[0, col] = temp;
        }
    }

    static int[] GetElementIndices(int[,] matrix, int element, int startRow)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int row = startRow; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (matrix[row, col] == element)
                {
                    return new int[] { row, col };
                }
            }
        }

        return new int[] { -1, -1 };
    }

    static void RearrangeMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                int rightElement = cols * row + col + 1;

                if (matrix[row, col] == rightElement)
                {
                    Console.WriteLine("No swap required");
                }
                else
                {
                    int[] indices = GetElementIndices(matrix, rightElement, row);

                    matrix[indices[0], indices[1]] = matrix[row, col];
                    matrix[row, col] = rightElement;

                    Console.WriteLine($"Swap ({row}, {col}) with ({indices[0]}, {indices[1]})");
                }
            }
        }
    }

    static void Main(string[] args)
    {
        int[] dimensions = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        int rows = dimensions[0],
            cols = dimensions[1];

        int[,] matrix = new int[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                matrix[row, col] = cols * row + col + 1;
            }
        }

        int commandsCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < commandsCount; i++)
        {
            string[] inputParams = Console.ReadLine().Split(' ');

            int index = int.Parse(inputParams[0]);
            string direction = inputParams[1];
            int moves = int.Parse(inputParams[2]);

            switch (direction)
            {
                case "up":
                    ShuffleColumnUp(matrix, index, moves % rows);
                    break;
                case "down":
                    ShuffleColumnDown(matrix, index, moves % rows);
                    break;
                case "left":
                    ShuffleRowLeft(matrix, index, moves % cols);
                    break;
                case "right":
                    ShuffleRowRight(matrix, index, moves % cols);
                    break;
            }
        }

        RearrangeMatrix(matrix);
    }
}