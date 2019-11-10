using System;
using System.Linq;
using System.Collections.Generic;

class Crossfire
{
    static void InitializeLists(List<List<int>> lists, int rows, int cols)
    {
        for (int row = 0; row < rows; row++)
        {
            lists.Add(new List<int>());

            for (int col = 0; col < cols; col++)
            {
                lists[row].Add(row * cols + col + 1);
            }
        }
    }

    static void DestroyTargetRowCells(List<List<int>> lists, int targetRow, int startCol, int endCol)
    {
        for (int col = endCol; col >= startCol; col--)
        {
            if (lists[targetRow].Count > col)
            {
                lists[targetRow].RemoveAt(col);
            }
        }
    }

    static void DestroyTargetColumnCells(List<List<int>> lists, int targetCol, int targetRow, int startRow, int endRow)
    {
        for (int row = startRow; row <= endRow; row++)
        {
            if (row == targetRow)
            {
                continue;
            }

            if (targetCol >= 0 && targetCol < lists[row].Count)
            {
                lists[row].RemoveAt(targetCol);
            }
        }
    }

    static void RemoveEmptyRows(List<List<int>> lists, int startRow, int endRow)
    {
        for (int row = endRow; row >= startRow; row--)
        {
            if (lists[row].Count == 0)
            {
                lists.RemoveAt(row);
            }
        }
    }

    static void DestroyCells(List<List<int>> lists, int targetRow, int targetCol, int radius, int cols)
    {
        int rows = lists.Count;

        int startRow = targetRow - radius < 0 ? 0 : targetRow - radius,
            endRow = targetRow + radius >= rows ? rows - 1 : targetRow + radius;

        int startCol = targetCol - radius < 0 ? 0 : targetCol - radius,
            endCol = targetCol + radius >= cols ? cols - 1 : targetCol + radius;

        if (targetRow >= 0 && targetRow < rows)
        {
            DestroyTargetRowCells(lists, targetRow, startCol, endCol);
        }

        DestroyTargetColumnCells(lists, targetCol, targetRow, startRow, endRow);
        RemoveEmptyRows(lists, startRow, endRow);
    }

    static void Main(string[] args)
    {
        int[] dimensions = Console.ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();

        int rows = dimensions[0],
            cols = dimensions[1];

        List<List<int>> lists = new List<List<int>>();
        InitializeLists(lists, rows, cols);

        string input = null;

        while ((input = Console.ReadLine()) != "Nuke it from orbit")
        {
            int[] nums = input.Split(' ')
                .Select(int.Parse)
                .ToArray();

            int row = nums[0],
                col = nums[1],
                radius = nums[2];

            DestroyCells(lists, row, col, radius, cols);
        }

        foreach (List<int> list in lists)
        {
            Console.WriteLine(string.Join(" ", list));
        }
    }
}