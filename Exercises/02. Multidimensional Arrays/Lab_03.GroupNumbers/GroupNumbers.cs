using System;
using System.Linq;

class GroupNumbers
{
    static void Main(string[] args)
    {
        int[] nums = Console.ReadLine()
            .Split(new[] { ", " }, StringSplitOptions.None)
            .Select(int.Parse)
            .ToArray();

        int[] dimensions = { 0, 0, 0 };

        for (int i = 0; i < nums.Length; i++)
        {
            int remainder = Math.Abs(nums[i] % 3);
            
            dimensions[remainder]++;
        }

        int[][] jaggedArray = new int[3][];

        for (int row = 0; row < 3; row++)
        {
            jaggedArray[row] = new int[dimensions[row]];
        }

        int[] indices = { 0, 0, 0 };

        foreach (int num in nums)
        {
            int remainder = Math.Abs(num % 3);

            jaggedArray[remainder][indices[remainder]++] = num;
        }

        foreach (int[] row in jaggedArray)
        {
            Console.WriteLine(string.Join(" ", row));
        }
    }
}