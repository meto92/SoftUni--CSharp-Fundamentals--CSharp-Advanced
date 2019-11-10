using System;
using System.Linq;

class ITVillage
{
    static void Move(ref int row, ref int col, int moves)
    {
        while (moves-- > 0)
        {
            // R
            if (row == 0 && col < 3)
            {
                col++;
            }
            // L
            else if (row == 3 && col > 0)
            {
                col--;
            }
            // U
            else if (col == 0 && row > 0)
            {
                row--;
            }
            // D
            else if (col == 3 && row < 3)
            {
                row++;
            }
        }
    }
        
    static void Main(string[] args)
    {
        string rowsStr = Console.ReadLine().Trim();
        int[] enteringPosition = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();
        int[] diceNumbers = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        string[] rows = rowsStr.Split(new[] { " | " }, StringSplitOptions.None);
        char[][] field = new char[4][];

        for (int i = 0; i < 4; i++)
        {
            field[i] = rows[i].Split(' ').Select(c => c[0]).ToArray();
        }

        int row = enteringPosition[0] - 1,
            col = enteringPosition[1] - 1,
            innsCount = rowsStr.Count(c => c == 'I'),
            boughtInns = 0,
            coins = 50;

        for (int i = 0; i < diceNumbers.Length; i++)
        {
            coins += boughtInns * 20;
            Move(ref row, ref col, diceNumbers[i] % 12);
            
            switch (field[row][col])
            {
                case 'P':
                    coins -= 5;
                    break;
                case 'I':
                    if (coins >= 100)
                    {
                        boughtInns++;
                        coins -= 100;
                    }
                    else
                    {
                        coins -= 10;
                    }
                    break;
                case 'F':
                    coins += 20;
                    break;
                case 'S':
                    i += 2;
                    break;
                case 'V':
                    coins *= 10;
                    break;
                case 'N':
                    Console.WriteLine($"<p>You won! Nakov's force was with you!<p>");
                    return;
            }

            if (coins < 0)
            {
                Console.WriteLine($"<p>You lost! You ran out of money!<p>");
                return;
            }
            else if (boughtInns == innsCount)
            {
                Console.WriteLine($"<p>You won! You own the village now! You have {coins} coins!<p>");
                return;
            }
        }

        Console.WriteLine($"<p>You lost! No more moves! You have {coins} coins!<p>");
    }
}