using System;
using System.Linq;
using System.Collections.Generic;

class Cell
{
    private int row;
    private int col;

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

    public Cell(int row, int col)
    {
        this.Row = row;
        this.Col = col;
    }
}

class PlusRemove
{
    static void GetCrossCenters(List<char[]> charArrays, List<Cell> crossCenters)
    {
        for (int row = 1; row < charArrays.Count - 1; row++)
        {
            for (int col = 1; col < charArrays[row].Length - 1; col++)
            {
                char c = char.ToUpper(charArrays[row][col]);

                if (char.ToUpper(charArrays[row][col - 1]) == c &&
                    char.ToUpper(charArrays[row][col + 1]) == c &&
                    charArrays[row - 1].Length > col &&
                    charArrays[row + 1].Length > col &&
                    char.ToUpper(charArrays[row - 1][col]) == c &&
                    char.ToUpper(charArrays[row + 1][col]) == c)
                {
                    crossCenters.Add(new Cell(row, col));
                }
            }
        }
    }

    static void RemoveCrosses(List<char[]> charArrays, List<Cell> crossCenters)
    {
        char emptyChar = '\0';

        foreach (Cell crossCenter in crossCenters)
        {
            int row = crossCenter.Row,
                col = crossCenter.Col;

            charArrays[row][col - 1] = emptyChar;
            charArrays[row][col] = emptyChar;
            charArrays[row][col + 1] = emptyChar;
            charArrays[row - 1][col] = emptyChar;
            charArrays[row + 1][col] = emptyChar;
        }
    }

    static void Main(string[] args)
    {
        List<char[]> charArrays = new List<char[]>();
        List<Cell> crossCenters = new List<Cell>();

        string input;

        while ((input = Console.ReadLine()) != "END")
        {
            charArrays.Add(input.ToCharArray());
        }

        GetCrossCenters(charArrays, crossCenters);
        RemoveCrosses(charArrays, crossCenters);

        foreach (char[] row in charArrays)
        {
            Console.WriteLine(string.Join("", row.Where(c => c != '\0')));
        }
    }
}