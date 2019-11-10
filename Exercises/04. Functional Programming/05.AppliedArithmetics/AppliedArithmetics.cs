using System;
using System.Linq;

class AppliedArithmetics
{
    static void Main(string[] args)
    {        
        Action<long[]> add = nums =>
        {
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i]++;
            }
        };

        Action<long[]> subtract = nums =>
        {
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i]--;
            }
        };

        Action<long[]> multiply = nums =>
        {
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] *= 2;
            }
        };

        Action<long[]> print = nums =>
        {
            for (int i = 0; i < nums.Length - 1; i++)
            {
                Console.Write($"{nums[i]} ");
            }

            Console.WriteLine(nums[nums.Length - 1]);
        };

        Func<string, Action<long[]>> getAction = com =>
        {
            Action<long[]> action = null;

            switch (com)
            {
                case "add":
                    action =  add;
                    break;
                case "subtract":
                    action = subtract;
                    break;
                case "multiply":
                    action = multiply;
                    break;
                case "print":
                    action =  print;
                    break;
            }

            return action;
        };

        long[] numbers = Console.ReadLine()
            .Split(' ')
            .Select(long.Parse)
            .ToArray();

        string command = null;

        while ((command = Console.ReadLine()) != "end")
        {
            Action<long[]> action = getAction(command);

            action(numbers);
        }
    }
}