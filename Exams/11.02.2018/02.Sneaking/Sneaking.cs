using System;
using System.Linq;
using System.Collections.Generic;

class Cell
{
    private int row;
    private int col;

    public Cell(int row, int col)
    {
        this.Row = row;
        this.Col = col;
    }

    public int Row
    {
        get { return row; }
        set { row = value; }
    }

    public int Col
    {
        get { return col; }
        set { col = value; }
    }
}

class Sneaking
{
    static List<Cell> GetEnemiesPositions(char[][] room)
    {
        List<Cell> enemiesPositions = new List<Cell>();

        int rows = room.Length,
            cols = room[0].Length;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (room[row][col] == 'b' || room[row][col] == 'd')
                {
                    enemiesPositions.Add(new Cell(row, col));
                }
            }
        }

        return enemiesPositions;
    }

    static void MoveEnemis(char[][] room, List<Cell> enemiesPositions)
    {
        int cols = room[0].Length;

        for (int i = 0; i < enemiesPositions.Count; i++)
        {
            int row = enemiesPositions[i].Row,
                col = enemiesPositions[i].Col;

            if (room[row][col] == 'b')
            {
                if (col == cols - 1)
                {
                    room[row][cols - 1] = 'd';
                }
                else
                {
                    room[row][col] = '.';
                    room[row][col + 1] = 'b';
                }
            }
            else if (room[row][col] == 'd')
            {
                if (col == 0)
                {
                    room[row][0] = 'b';
                }
                else
                {
                    room[row][col] = '.';
                    room[row][col - 1] = 'd';
                }
            }
        }
    }

    static bool IsPlayerKilled(char[][] room, Cell playerPosition)
    {
        int row = playerPosition.Row,
            col = playerPosition.Col,
            left = col - 1,
            right = col + 1;

        while (left >= 0 || right < room[0].Length)
        {
            if (left >= 0 && room[row][left] == 'b' ||
                right < room[0].Length && room[row][right] == 'd')
            {
                room[playerPosition.Row][playerPosition.Col] = 'X';

                return true;
            }

            left--;
            right++;
        }

        return false;
    }

    static void MovePlayer(char[][] room, Cell playerPosition, char move)
    {
        room[playerPosition.Row][playerPosition.Col] = '.';

        switch (move)
        {
            case 'U':
                playerPosition.Row--;
                break;
            case 'D':
                playerPosition.Row++;
                break;
            case 'L':
                playerPosition.Col--;
                break;
            case 'R':
                playerPosition.Col++;
                break;
        }

        room[playerPosition.Row][playerPosition.Col] = 'S';
    }

    static void Main(string[] args)
    {
        int rows = int.Parse(Console.ReadLine());

        char[][] room = new char[rows][];

        Cell playerPosition = new Cell(-1, -1);
        Cell bossPosition = new Cell(-1, -1);

        bool isPlayerKilled = false;

        for (int row = 0; row < rows; row++)
        {
            string line = Console.ReadLine();

            room[row] = line.ToCharArray();

            if (playerPosition.Row == -1 && line.Contains('S'))
            {
                playerPosition.Row = row;
                playerPosition.Col = line.IndexOf('S');
            }

            if (bossPosition.Row == -1 && line.Contains('N'))
            {
                bossPosition.Row = row;
                bossPosition.Col = line.IndexOf('N');
            }
        }

        string moves = Console.ReadLine();

        foreach (char move in moves)
        {
            List<Cell> enemiesPositions = GetEnemiesPositions(room);
            MoveEnemis(room, enemiesPositions);

            isPlayerKilled =  IsPlayerKilled(room, playerPosition);

            if (isPlayerKilled)
            {
                break;
            }

            MovePlayer(room, playerPosition, move);

            if (playerPosition.Row == bossPosition.Row)
            {
                room[bossPosition.Row][bossPosition.Col] = 'X';
                break;
            }
        }

        if (isPlayerKilled)
        {
            Console.WriteLine($"Sam died at {playerPosition.Row}, {playerPosition.Col}");
        }
        else
        {
            Console.WriteLine("Nikoladze killed!");
        }

        foreach (char[] row in room)
        {
            Console.WriteLine(new string(row));
        }
    }    
}