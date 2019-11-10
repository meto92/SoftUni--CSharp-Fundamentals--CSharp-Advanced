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

class XRemoval
{
    static List<Cell> GetXCenters(List<char[]> charArrays)
    {
        List<Cell> xCenters = new List<Cell>();

        for (int row = 1; row < charArrays.Count - 1; row++)
        {
            for (int col = 1; col < charArrays[row].Length; col++)
            {
                char c = char.ToUpper(charArrays[row][col]);

                if (charArrays[row - 1].Length > col + 1 &&
                    charArrays[row + 1].Length > col + 1 &&
                    char.ToUpper(charArrays[row - 1][col - 1]) == c &&
                    char.ToUpper(charArrays[row - 1][col + 1]) == c &&
                    char.ToUpper(charArrays[row + 1][col - 1]) == c &&
                    char.ToUpper(charArrays[row + 1][col + 1]) == c)
                {
                    xCenters.Add(new Cell(row, col));
                }
            }
        }

        return xCenters;
    }

    static void SetXsToEmptyChar(List<char[]> charArrays, List<Cell> xCenters)
    {
        char emptyChar = '\0';

        foreach (Cell xCenter in xCenters)
        {
            int row = xCenter.Row,
                col = xCenter.Col;

            charArrays[row - 1][col - 1] = emptyChar;
            charArrays[row - 1][col + 1] = emptyChar;
            charArrays[row][col] = emptyChar;
            charArrays[row + 1][col - 1] = emptyChar;
            charArrays[row + 1][col + 1] = emptyChar;
        }
    }

    static void PrintRemainingChars(List<char[]> charArrays)
    {
        foreach (char[] row in charArrays)
        {
            Console.WriteLine(new string(row.Where(c => c != '\0').ToArray()));
        }
    }

    static void Main(string[] args)
    {
        List<char[]> charArrays = new List<char[]>();

        string input;

        while ((input = Console.ReadLine()) != "END")
        {
            charArrays.Add(input.ToCharArray());
        }
        
        List<Cell> xCenters = GetXCenters(charArrays);

        SetXsToEmptyChar(charArrays, xCenters);
        PrintRemainingChars(charArrays);     
    }
}