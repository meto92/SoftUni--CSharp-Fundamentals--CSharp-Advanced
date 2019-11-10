using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class LegoBlocks
{
    static void Main(string[] args)
    {
        int rows = int.Parse(Console.ReadLine());

        int[][] firstJaggedArray = new int[rows][];
        int[][] secondJaggedArray = new int[rows][];

        for (int row = 0; row < 2 * rows; row++)
        {
            IEnumerable<int> nums =
                Regex.Split(Console.ReadLine().Trim(), "\\s+").Select(int.Parse);

            if (row < rows)
            {
                firstJaggedArray[row] = nums.ToArray();
            }
            else
            {
                secondJaggedArray[row % rows] = nums.Reverse().ToArray();
            }
        }

        int[][] matchedArray = new int[rows][];

        for (int row = 0; row < rows; row++)
        {
            matchedArray[row] = firstJaggedArray[row]
                .Concat(secondJaggedArray[row])
                .ToArray();
        }

        bool isMatchedArrayMatrix = true;
        int totalNumberOfCells = 0;

        for (int row = 0; row < rows - 1; row++)
        {
            if (matchedArray[row].Length != matchedArray[row + 1].Length)
            {
                isMatchedArrayMatrix = false;
            }

            totalNumberOfCells += matchedArray[row].Length;
        }

        totalNumberOfCells += matchedArray[rows - 1].Length;

        if (isMatchedArrayMatrix)
        {
            foreach (int[] row in matchedArray)
            {
                Console.WriteLine($"[{string.Join(", ", row)}]");
            }
        }
        else
        {
            Console.WriteLine($"The total number of cells is: {totalNumberOfCells}");
        }
    }
}