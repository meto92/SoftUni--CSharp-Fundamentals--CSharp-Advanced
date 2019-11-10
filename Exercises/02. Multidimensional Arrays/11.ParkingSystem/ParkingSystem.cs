using System;
using System.Linq;
using System.Collections.Generic;

class ParkingSystem
{
    static int GetParkingCol(Dictionary<int, bool[]> parking, int entryRow, int desiredParkingRow, int desiredParkingCol, int cols)
    {
        int parkingCol = -1;

        for (int left = desiredParkingCol, right = desiredParkingCol; left > 0 || right < cols; left--, right++)
        {
            if (left > 0 && !parking[desiredParkingRow][left])
            {
                parkingCol = left;
                break;
            }

            if (right < cols && !parking[desiredParkingRow][right])
            {
                parkingCol = right;
                break;
            }
        }

        return parkingCol;
    }

    static void Main(string[] args)
    {
        int[] dimensions = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        int rows = dimensions[0],
            cols = dimensions[1];

        Dictionary<int, bool[]> parking = new Dictionary<int, bool[]>();

        string input;

        while ((input = Console.ReadLine()) != "stop")
        {
            int[] inputParams = input.Split(' ')
                .Select(int.Parse)
                .ToArray();

            int entryRow = inputParams[0],
                desiredParkingRow = inputParams[1],
                desiredParkingCol = inputParams[2];

            if (!parking.ContainsKey(desiredParkingRow))
            {
                parking[desiredParkingRow] = new bool[cols];
            }

            int parkingCol = GetParkingCol(parking, entryRow, desiredParkingRow, desiredParkingCol, cols);

            if (parkingCol != -1)
            {
                int travelledDistance = Math.Abs(entryRow - desiredParkingRow) + parkingCol + 1;

                parking[desiredParkingRow][parkingCol] = true;
                Console.WriteLine(travelledDistance);
            }
            else
            {
                Console.WriteLine($"Row {desiredParkingRow} full");
            }
        }
    }    
}