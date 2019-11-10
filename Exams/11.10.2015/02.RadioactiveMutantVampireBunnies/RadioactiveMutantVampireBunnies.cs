using System;
using System.Linq;

class RadioactiveMutantVampireBunnies
{
    static void MovePlayer(char[][] lair, ref int playerRow, ref int playerCol, char move)
    {
        lair[playerRow][playerCol] = '.';

        switch (move)
        {
            case 'L':
                playerCol--;
                break;
            case 'R':
                playerCol++;
                break;
            case 'U':
                playerRow--;
                break;
            case 'D':
                playerRow++;
                break;
        }

        if (playerRow >= 0 &&
            playerRow < lair.Length &&
            playerCol >= 0 &&
            playerCol < lair[0].Length &&
            lair[playerRow][playerCol] == '.')
        {
            lair[playerRow][playerCol] = 'P';
        }
    }

    static void SpreadBunnies(char[][] lair)
    {
        int rows = lair.Length;

        bool[][] bunniesPositions = new bool[rows][];

        for (int row = 0; row < rows; row++)
        {
            bunniesPositions[row] = lair[row].Select(p => p == 'B').ToArray();
        }

        int cols = lair[0].Length;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (bunniesPositions[row][col])
                {
                    // L
                    if (col > 0)
                    {
                        lair[row][col - 1] = 'B';
                    }

                    // R
                    if (col < cols - 1)
                    {
                        lair[row][col + 1] = 'B';
                    }

                    // U
                    if (row > 0)
                    {
                        lair[row - 1][col] = 'B';
                    }

                    // D
                    if (row < rows - 1)
                    {
                        lair[row + 1][col] = 'B';
                    }
                }
            }
        }
    }

    static void PrintLair(char[][] lair)
    {
        foreach (char[] row in lair)
        {
            Console.WriteLine(new string(row));
        }
    }

    static void Main(string[] args)
    {
        int[] dimensions = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        int rows = dimensions[0],
            cols = dimensions[1],
            playerRow = -1,
            playerCol = -1;

        char[][] lair = new char[rows][];

        for (int row = 0; row < rows; row++)
        {
            string line = Console.ReadLine();

            lair[row] = line.ToCharArray();

            if (playerRow == -1 && line.Contains('P'))
            {
                playerRow = row;
                playerCol = line.IndexOf('P');
            }
        }
        
        string playerMoves = Console.ReadLine();

        foreach (char move in playerMoves)
        {
            int oldPlayerRow = playerRow,
                oldPlayerCol = playerCol;

            MovePlayer(lair, ref playerRow, ref playerCol, move);
            SpreadBunnies(lair);

            bool won = false;
            bool dead = false;

            if (playerRow < 0 ||
                playerRow >= rows ||
                playerCol < 0 ||
                playerCol >= cols)
            {
                won = true;
            }
            else if (lair[playerRow][playerCol] == 'B')
            {
                dead = true;
            }

            if (won || dead)
            {
                PrintLair(lair);

                if (won)
                {
                    Console.WriteLine($"won: {oldPlayerRow} {oldPlayerCol}");
                }
                else
                {
                    Console.WriteLine($"dead: {playerRow} {playerCol}");
                }

                return;
            }
        }
    }
}