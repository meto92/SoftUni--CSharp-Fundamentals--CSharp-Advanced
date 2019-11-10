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
        private set { row = value; }
    }

    public int Col
    {
        get { return col; }
        private set { col = value; }
    }
}

class KnightGame
{
    static List<Cell> GetKnightsPositions(int boardSize)
    {
        List<Cell> knightsPositions = new List<Cell>();

        for (int row = 0; row < boardSize; row++)
        {
            string rowElements = Console.ReadLine();

            for (int col = 0; col < boardSize; col++)
            {
                if (rowElements[col] == 'K')
                {
                    knightsPositions.Add(new Cell(row, col));
                }
            }
        }

        return knightsPositions;
    }

    static Dictionary<Cell, HashSet<Cell>> GetReachableKnights(List<Cell> knightsPositions)
    {
        Dictionary<Cell, HashSet<Cell>> reachableKnights = 
            new Dictionary<Cell, HashSet<Cell>>();

        for (int i = 0; i < knightsPositions.Count; i++)
        {
            Cell currentKnight = knightsPositions[i];

            reachableKnights[currentKnight] = new HashSet<Cell>();

            foreach (Cell knight in knightsPositions)
            {
                int rowDistance = Math.Abs(currentKnight.Row - knight.Row),
                    colDistance = Math.Abs(currentKnight.Col - knight.Col);

                if (rowDistance == 1 && colDistance == 2 ||
                    rowDistance == 2 && colDistance == 1)
                {
                    reachableKnights[currentKnight].Add(knight);
                }
            } 
        }

        return reachableKnights;
    }

    static void RemoveKnight(Dictionary<Cell, HashSet<Cell>> reachableKnights, Cell knightToRemove)
    {
        reachableKnights.Remove(knightToRemove);

        foreach (KeyValuePair<Cell, HashSet<Cell>> pair in reachableKnights)
        {
            pair.Value.Remove(knightToRemove);
        }
    }

    static int CalcKnightsToRemoveMinCount(Dictionary<Cell, HashSet<Cell>> reachableKnights)
    {
        int knightsToRemoveMinCount = 0;

        while (true)
        {
            KeyValuePair<Cell, HashSet<Cell>> pairToRemove =
                reachableKnights.OrderByDescending(p => p.Value.Count).First();

            int maxReachableKnights = pairToRemove.Value.Count();

            if (maxReachableKnights == 0)
            {
                break;
            }

            knightsToRemoveMinCount++;
            RemoveKnight(reachableKnights, pairToRemove.Key);
        }

        return knightsToRemoveMinCount;
    }

    static void Main(string[] args)
    {
        int boardSize = int.Parse(Console.ReadLine());

        List<Cell> knightsPositions = GetKnightsPositions(boardSize);
        Dictionary<Cell, HashSet<Cell>> reachableKnights = GetReachableKnights(knightsPositions);

        int knightsToRemoveMinCount = CalcKnightsToRemoveMinCount(reachableKnights);
        
        Console.WriteLine(knightsToRemoveMinCount);
    }    
}