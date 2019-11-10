using System;
using System.Linq;
using System.Collections.Generic;

class InfernoIII
{
    static void Main(string[] args)
    {
        Func<int[], int, int> sumLeft = (numbers, index) =>
        {
            int leftSum = numbers[index];

            if (index > 0)
            {
                leftSum += numbers[index - 1];
            }

            return leftSum;
        };

        Func<int[], int, int> sumRight = (numbers, index) =>
        {
            int rightSum = numbers[index];

            if (index < numbers.Length - 1)
            {
                rightSum += numbers[index + 1];
            }

            return rightSum;
        };

        Func<int[], int, int, bool> isLeftSumEqualToParameter = (numbers, index, parameter) =>
        {
            int leftSum = sumLeft(numbers, index);

            return leftSum == parameter;
        };

        Func<int[], int, int, bool> isRightSumEqualToParameter = (numbers, index, parameter) =>
        {
            int rightSum = sumRight(numbers, index);

            return rightSum == parameter;
        };

        Func<int[], int, int, bool> isSumOfNeighboursAndCurrentEqualToParameter = (numbers, index, parameter) =>
        {
            int leftSum = sumLeft(numbers, index);
            int rightSum = sumRight(numbers, index);

            return leftSum + rightSum - numbers[index] == parameter;
        };

        Func<string, Func<int[], int, int, bool>> getFunction = filterType =>
        {
            Func<int[], int, int, bool> func = null;

            switch (filterType)
            {
                case "Sum Left":
                    func = isLeftSumEqualToParameter;
                    break;
                case "Sum Right":
                    func = isRightSumEqualToParameter;
                    break;
                case "Sum Left Right":
                    func = isSumOfNeighboursAndCurrentEqualToParameter;
                    break;
            }

            return func;
        };

        int[] gemsPowers = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

        Dictionary<string, Dictionary<int, Func<int[], int, int, bool>>> filters =
            new Dictionary<string, Dictionary<int, Func<int[], int, int, bool>>>();
        string input = null;

        while ((input = Console.ReadLine()) != "Forge")
        {
            string[] commandArgs = input.Split(';');

            string command = commandArgs[0];
            string filterType = commandArgs[1];
            int filterParameter = int.Parse(commandArgs[2]);

            if (command == "Exclude")
            {
                if (!filters.ContainsKey(filterType))
                {
                    filters[filterType] = new Dictionary<int, Func<int[], int, int, bool>>();
                }

                Func<int[], int, int, bool> func = getFunction(filterType);
                filters[filterType][filterParameter] = func;
            }
            else if (command == "Reverse" && 
                filters.ContainsKey(filterType) &&
                filters[filterType].ContainsKey(filterParameter))
            {
                filters[filterType].Remove(filterParameter);
            }
        }

        bool[] mask = new bool[gemsPowers.Length]
            .Select(p => true)
            .ToArray();

        foreach (Dictionary<int, Func<int[], int, int, bool>> pairs in filters.Values)
        {
            foreach (KeyValuePair<int, Func<int[], int, int, bool>> pair in pairs)
            {
                int parameter = pair.Key;
                Func<int[], int, int, bool> func = pair.Value;

                for (int i = 0; i < gemsPowers.Length; i++)
                {
                    if (!mask[i])
                    {
                        continue;
                    }

                    if (func(gemsPowers, i, parameter))
                    {
                        mask[i] = false;
                    }
                }
            }
        }

        Console.WriteLine(string.Join(" ", gemsPowers.Where((x, i) => mask[i])));
    }
}