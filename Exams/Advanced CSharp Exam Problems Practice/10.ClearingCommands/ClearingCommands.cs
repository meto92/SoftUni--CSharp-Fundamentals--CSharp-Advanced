using System;
using System.Linq;
using System.Security;
using System.Collections.Generic;

class ClearingCommands
{
    private static int targetRow;
    private static int targetCol;
    private static List<char[]> table;
    private static string commands;

    static void SetSpacesLeft()
    {
        int row = targetRow,
            col = targetCol - 1;

        while (col >= 0 && !commands.Contains(table[row][col]))
        {
            table[row][col] = ' ';
            col--;
        }
    }

    static void SetSpacesUp()
    {
        int row = targetRow - 1,
            col = targetCol;

        while (row >= 0 && !commands.Contains(table[row][col]))
        {
            table[row][col] = ' ';
            row--;
        }
    }

    static void SetSpacesRight()
    {
        int row = targetRow,
            col = targetCol + 1,
            cols = table[0].Length;

        while (col < cols && !commands.Contains(table[row][col]))
        {
            table[row][col] = ' ';
            col++;
        }
    }

    static void SetSpacesDown()
    {
        int row = targetRow + 1,
            col = targetCol,
            rows = table.Count;

        while (row < rows && !commands.Contains(table[row][col]))
        {
            table[row][col] = ' ';
            row++;
        }
    }

    static void SetSpaces()
    {
        switch (table[targetRow][targetCol])
        {
            case '<':
                SetSpacesLeft();
                return;
            case '^':
                SetSpacesUp();
                return;
            case '>':
                SetSpacesRight();
                return;
            case 'v':
                SetSpacesDown();
                return;
        }
    }

    static void PrintResult()
    {
        foreach (char[] row in table)
        {
            Console.WriteLine($"<p>{SecurityElement.Escape(new string(row))}</p>");
        }
    }

    static void Main(string[] args)
    {
        table = new List<char[]>();
        commands = "<^>v";

        string input;

        while ((input = Console.ReadLine()) != "END")
        {
            table.Add(input.ToCharArray());
        }

        int rows = table.Count,
            cols = table[0].Length;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                char c = table[row][col];

                if (c == '<' || c == '^' || c == '>' || c == 'v')
                {
                    targetRow = row;
                    targetCol = col;
                    SetSpaces();
                }                
            }
        }

        PrintResult();
    }
}