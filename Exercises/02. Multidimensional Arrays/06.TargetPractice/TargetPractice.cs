using System;
using System.Linq;

class TargetPractice
{
    static void FillMatrix(char[,] stairs, string snake)
    {
        int rows = stairs.GetLength(0),
            cols = stairs.GetLength(1);

        int row = rows - 1,
            col = cols - 1,
            step = -1,
            counter = 0;

        while (row >= 0)
        {
            while (col >= 0 && col < cols)
            {
                stairs[row, col] = snake[counter % snake.Length];

                col += step;
                counter++;
            }

            col -= step;
            step *= -1;
            row--;
        }
    }

    static void ShotStairs(char[,] stairs, int[] shotParams)
    {
        int rows = stairs.GetLength(0),
            cols = stairs.GetLength(1);

        int shotRow = shotParams[0],
            shotCol = shotParams[1],
            shotRadius = shotParams[2];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int yOffset = shotRow - i;
                int xOffset = shotCol - j;

                double distance = Math.Sqrt(yOffset * yOffset + xOffset * xOffset);

                if (distance <= shotRadius)
                {
                    stairs[i, j] = ' ';
                }
            }
        }
    }

    static void FallChars(char[,] stairs)
    {
        int rows = stairs.GetLength(0),
            cols = stairs.GetLength(1);

        for (int row = rows - 1; row > 0; row--)
        {
            for (int col = 0; col < cols; col++)
            {
                if (stairs[row, col] == ' ')
                {
                    int r = row - 1;

                    while (r >= 0 && stairs[r, col] == ' ')
                    {
                        r--;
                    }

                    if (r >= 0)
                    {
                        stairs[row, col] = stairs[r, col];
                        stairs[r, col] = ' ';
                    }
                }
            }
        }
    }

    static void Main(string[] args)
    {
        int[] dimensions = Console.ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();
        string snake = Console.ReadLine();
        int[] shotParams = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        int rows = dimensions[0],
            cols = dimensions[1];

        char[,] stairs = new char[rows, cols];

        FillMatrix(stairs, snake);
        ShotStairs(stairs, shotParams);
        FallChars(stairs);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Console.Write(stairs[row, col]);
            }

            Console.WriteLine();
        }
    }
}