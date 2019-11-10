using System;
using System.Linq;
using System.Numerics;

class ArraySlider
{
    static void PerformOperation(BigInteger[] nums, int index, char operation, int operand)
    {
        switch (operation)
        {
            case '+':
                nums[index] += operand;
                break;
            case '-':
                nums[index] = nums[index] - operand < 0
                    ? 0
                    : nums[index] - operand;
                break;
            case '*':
                nums[index] *= operand;
                break;
            case '/':
                nums[index] /= operand;
                break;
            case '&':
                nums[index] &= operand;
                break;
            case '|':
                nums[index] |= operand;
                break;
            case '^':
                nums[index] ^= operand;
                break;
        }
    }

    static void Main(string[] args)
    {
        BigInteger[] nums = Console.ReadLine()
            .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(BigInteger.Parse)
            .ToArray();

        string input;
        int index = 0;
        
        while ((input = Console.ReadLine()) != "stop")
        {
            string[] command = input.Split();

            int offset = int.Parse(command[0]);
            char operation = command[1][0];
            int operand = int.Parse(command[2]);

            index += offset;
            index %= nums.Length;
            index = index < 0
                ? index + nums.Length
                : index;

            PerformOperation(nums, index, operation, operand);
        }

        Console.WriteLine($"[{string.Join(", ", nums)}]");
    }
}