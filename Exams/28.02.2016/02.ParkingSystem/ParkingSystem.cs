using System;
using System.Linq;
using System.Collections.Generic;

class ParkingSystem
{
    static int GetParkingCol(Dictionary<int, bool[]> parking, int desiredRow, int desiredCol, int cols)
    {
        int left = desiredCol,
            right = desiredCol,
            parkingCol = -1;

        while (left > 0 || right < cols)
        {
            if (left > 0 && parking[desiredRow][left])
            {
                parkingCol = left;
                break;
            }

            if (right < cols && parking[desiredRow][right])
            {
                parkingCol = right;
                break;
            }

            left--;
            right++;
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

        string input = null;

        while ((input = Console.ReadLine()) != "stop")
        {
            int[] parkingParams = input.Split(' ')
            .Select(int.Parse)
            .ToArray();

            int entryRow = parkingParams[0],
                desiredRow = parkingParams[1],
                desiredCol = parkingParams[2];

            if (!parking.ContainsKey(desiredRow))
            {
                // all desiredRow parking spots are free
                parking[desiredRow] = new bool[cols].Select(x => true).ToArray();
            }

            int parkingCol = GetParkingCol(parking, desiredRow, desiredCol, cols);

            if (parkingCol != -1)
            {
                int travelledDistance = Math.Abs(entryRow - desiredRow) + parkingCol + 1;

                Console.WriteLine(travelledDistance);
                parking[desiredRow][parkingCol] = false;
            }
            else
            {
                Console.WriteLine($"Row {desiredRow} full");
            }
        }
    }
}