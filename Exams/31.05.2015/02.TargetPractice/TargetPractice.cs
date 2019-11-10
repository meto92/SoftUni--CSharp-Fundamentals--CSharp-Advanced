using System;
using System.Linq;

class TargetPractice
{
    private static char[,] stairs; 
    private static int rows;
    private static int cols;

    static void FillMatrix(string snake)
    {
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

            row--;
            col -= step;
            step *= -1;
        }
    }

    static void ShotStairs(int[] shotParameters)
    {
        int impactRow = shotParameters[0],
            impactCol = shotParameters[1],
            radius = shotParameters[2];

        for (int row = 0; row < rows; row++)
        {
            int rowOffset = impactRow - row;

            for (int col = 0; col < cols; col++)
            {
                int colOffset = impactCol - col;
                double distance = Math.Sqrt(rowOffset * rowOffset + colOffset * colOffset);

                if (distance <= radius)
                {
                    stairs[row, col] = ' ';
                }
            }
        }
    }

    static void FallChars()
    {
        for (int row = rows - 1; row >= 0; row--)
        {
            for (int col = cols - 1; col >= 0; col--)
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

    static void PrintMatrix()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Console.Write(stairs[row, col]);
            }

            Console.WriteLine();
        }
    }

    static void Main(string[] args)
    {
        int[] dimensions = Console.ReadLine().Split().Select(int.Parse).ToArray();
        string snake = Console.ReadLine();
        int[] shotParameters = Console.ReadLine().Split().Select(int.Parse).ToArray();

        rows = dimensions[0];
        cols = dimensions[1];

        stairs = new char[rows, cols];

        FillMatrix(snake);
        ShotStairs(shotParameters);
        FallChars();
        PrintMatrix();
    }
}