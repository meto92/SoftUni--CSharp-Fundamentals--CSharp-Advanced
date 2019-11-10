using System;
using System.Linq;

class CommandInterpreter
{
    private static string[] array;
    
    static bool AreParamsValid(int start, int count)
    {
        if (start < 0 ||
            start >= array.Length ||
            count < 0 ||
            start + count > array.Length)
        {
            return false;
        }

        return true;
    }

    static void Reverse(int start, int count)
    {
        if (!AreParamsValid(start, count))
        {
            Console.WriteLine("Invalid input parameters.");            
            return;
        }
        
        Array.Reverse(array, start, count);
    }

    static void Sort(int start, int count)
    {
        if (!AreParamsValid(start, count))
        {
            Console.WriteLine("Invalid input parameters.");
            return;
        }

        Array.Sort(array, start, count);        
    }

    static void RollLeft(int count)
    {
        if (!AreParamsValid(0, count))
        {
            Console.WriteLine("Invalid input parameters.");
            return;
        }
        
        for (int c = 0; c < count; c++)
        {
            string first = array.First();

            for (int i = 0; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];
            }

            array[array.Length - 1] = first;
        }
    }

    static void RollRight(int count)
    {
        if (!AreParamsValid(0, count))
        {
            Console.WriteLine("Invalid input parameters.");
            return;
        }

        for (int c = 0; c < count; c++)
        {
            string last = array.Last();

            for (int i = array.Length - 1; i > 0; i--)
            {
                array[i] = array[i - 1];
            }

            array[0] = last;
        }
    }

    static void Main(string[] args)
    {
        array = Console.ReadLine().Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        
        string input;
        int start, count;

        while ((input = Console.ReadLine()) != "end")
        {
            string[] commandParams = input.Split();

            switch (commandParams[0])
            {
                case "reverse":
                    start = int.Parse(commandParams[2]);
                    count = int.Parse(commandParams[4]);
                    Reverse(start, count);
                    break;
                case "sort":
                    start = int.Parse(commandParams[2]);
                    count = int.Parse(commandParams[4]);
                    Sort(start, count);
                    break;
                case "rollLeft":
                    count = int.Parse(commandParams[1]);
                    RollLeft(count % array.Length);
                    break;
                case "rollRight":
                    count = int.Parse(commandParams[1]);
                    RollRight(count % array.Length);
                    break;
            }
        }

        Console.WriteLine($"[{string.Join(", ", array)}]");
    }
}