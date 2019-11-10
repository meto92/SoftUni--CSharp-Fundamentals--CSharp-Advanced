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

        int rows = lair.Length,
            cols = lair[0].Length;

        if (playerRow >= 0 &&
            playerRow < rows &&
            playerCol >= 0 &&
            playerCol < cols && 
            lair[playerRow][playerCol] == '.')
        {
            lair[playerRow][playerCol] = 'P';
        }
    }

    static void SpreadBunnies(char[][] lair)
    {
        int rows = lair.Length;
        int cols = lair[0].Length;

        bool[,] bunniesPositions = new bool[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                 bunniesPositions[row, col] = lair[row][col] == 'B';
            }
        }

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (bunniesPositions[row, col])
                {
                    //L
                    if (col > 0)
                    {
                        lair[row][col - 1] = 'B';
                    }

                    //R
                    if (col < cols - 1)
                    {
                        lair[row][col + 1] = 'B';
                    }

                    //U
                    if (row > 0)
                    {
                        lair[row - 1][col] = 'B';
                    }

                    //D
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
            .Split()
            .Select(int.Parse)
            .ToArray();

        int rows = dimensions[0],
            cols = dimensions[1];

        char[][] lair = new char[rows][];
        int playerRow = -1,
            playerCol = -1;

        for (int row = 0; row < rows; row++)
        {
            lair[row] = Console.ReadLine().ToCharArray();

            if (lair[row].Contains('P'))
            {
                playerRow = row;
                playerCol = new string(lair[row]).IndexOf('P');
            }
        }

        string moves = Console.ReadLine();

        bool hasPlayerWon = false;
        bool hasPlayerDied = false;

        foreach (char move in moves)
        {
            int prevPlayerRow = playerRow,
                prevPlayerCol = playerCol;

            MovePlayer(lair, ref playerRow, ref playerCol, move);            
            SpreadBunnies(lair);

            if (playerRow < 0 ||
                playerRow >= rows ||
                playerCol < 0 ||
                playerCol >= cols)
            {
                hasPlayerWon = true;
            }
            else if (lair[playerRow][playerCol] == 'B')
            {
                hasPlayerDied = true;
            }

            if (hasPlayerWon || hasPlayerDied)
            {
                PrintLair(lair);

                if (hasPlayerWon)
                {
                    Console.WriteLine($"won: {prevPlayerRow} {prevPlayerCol}");
                }
                else
                {
                    Console.WriteLine($"dead: {playerRow} {playerCol}");
                }

                break;
            }
        }
    }    
}