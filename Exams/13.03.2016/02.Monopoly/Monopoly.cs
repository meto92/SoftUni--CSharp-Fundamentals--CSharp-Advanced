using System;
using System.Linq;

class Monopoly
{
    static void Main(string[] args)
    {
        int[] dimension = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        int rows = dimension[0],
            cols = dimension[1];

        char[,] field = new char[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            string fieldRow = Console.ReadLine();

            for (int col = 0; col < cols; col++)
            {
                field[row, col] = fieldRow[col];
            }
        }

        int money = 50,
            turns = 0,
            boughtHotels = 0,
            playerRow = 0,
            playerCol = 0,
            step = 1;

        while (playerRow < rows)
        {
            while (playerCol >= 0 && playerCol < cols)
            {
                switch (field[playerRow, playerCol])
                {
                    case 'H':
                        boughtHotels++;
                        Console.WriteLine($"Bought a hotel for {money}. Total hotels: {boughtHotels}.");
                        money = 0;
                        break;
                    case 'J':
                        Console.WriteLine($"Gone to jail at turn {turns}.");
                        turns += 2;
                        money += 20 * boughtHotels;
                        break;
                    case 'F':
                        break;
                    case 'S':
                        int moneySpentAtShop = (playerRow + 1) * (playerCol + 1) <= money
                            ? (playerRow + 1) * (playerCol + 1)
                            : money;

                        money -= moneySpentAtShop;

                        Console.WriteLine($"Spent {moneySpentAtShop} money at the shop.");
                        break;
                }

                turns++;
                money += 10 * boughtHotels;
                playerCol += step;
            }

            playerCol -= step;
            step *= -1;
            playerRow++;
        }

        Console.WriteLine($"Turns {turns}");
        Console.WriteLine($"Money {money}");
    }
}