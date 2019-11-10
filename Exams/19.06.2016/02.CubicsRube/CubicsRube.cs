using System;
using System.Linq;

class CubicsRube
{
    static void Main(string[] args)
    {
        int dimensionOfRube = int.Parse(Console.ReadLine());

        long sum = 0;
        int changedCellsCount = 0;
        string input = null;

        while ((input = Console.ReadLine()) != "Analyze")
        {
            int[] inputParams = input.Split(' ')
                .Select(int.Parse)
                .ToArray();

            int firstDimension = inputParams[0],
                secondDimension = inputParams[1],
                thirdDimension = inputParams[2],
                particles = inputParams[3];

            if (particles != 0 &&
                firstDimension >= 0 &&
                firstDimension < dimensionOfRube &&
                secondDimension >= 0 &&
                secondDimension < dimensionOfRube &&
                thirdDimension >= 0 &&
                thirdDimension < dimensionOfRube)
            {
                sum += particles;
                changedCellsCount++;
            }
        }

        Console.WriteLine(sum);
        Console.WriteLine(Math.Pow(dimensionOfRube, 3) - changedCellsCount);
    }
}