using System;

class TheHeiganDance
{
    static void SetZones(bool[,] infectedZones, int spellRow, int spellCol, bool isAffected)
    {
        if (spellRow < -1 || 
            spellRow > 15 || 
            spellCol < -1 || 
            spellCol > 15)
        {
            return;
        }

        for (int row = spellRow - 1; row <= spellRow + 1; row++)
        {
            if (row < 0 || row > 14)
            {
                continue;
            }

            for (int col = spellCol - 1; col <= spellCol + 1; col++)
            {
                if (col >= 0 && col < 15)
                {
                    infectedZones[row, col] = isAffected;
                }
            }
        }
    }

    static void Main(string[] args)
    {
        double playerDamage = double.Parse(Console.ReadLine());

        bool[,] dangerousZones = new bool[15, 15];

        int playerHitPoints = 18500;
        double heiganHitPoints = 3e6;
        int playerRow = 7,
            playerCol = 7;

        bool isHeiganDefeated = false;
        bool isPlayerKilled = false;
        bool hasPlayerTakenDamageFromCloudPrevTurn = false;
        string spellKilledPlayer = string.Empty;

        while (true)
        {
            string[] spellParams = Console.ReadLine().Split(' ');

            string spell = spellParams[0];
            int row = int.Parse(spellParams[1]);
            int col = int.Parse(spellParams[2]);

            if (hasPlayerTakenDamageFromCloudPrevTurn)
            {
                playerHitPoints -= 3500;
                hasPlayerTakenDamageFromCloudPrevTurn = false;

                if (playerHitPoints <= 0)
                {
                    isPlayerKilled = true;
                    spellKilledPlayer = "Plague Cloud";
                }
            }
            
            heiganHitPoints -= playerDamage;

            if (heiganHitPoints <= 0)
            {
                isHeiganDefeated = true;
            }

            if (isPlayerKilled || isHeiganDefeated)
            {
                break;
            }

            SetZones(dangerousZones, row, col, true);

            if (dangerousZones[playerRow, playerCol])
            {
                if (playerRow > 0 && !dangerousZones[playerRow - 1, playerCol])
                {
                    playerRow--;
                }
                else if (playerCol < 14 && !dangerousZones[playerRow, playerCol + 1])
                {
                    playerCol++;
                }
                else if (playerRow < 14 && !dangerousZones[playerRow + 1, playerCol])
                {
                    playerRow++;
                }
                else if (playerCol > 0 && !dangerousZones[playerRow, playerCol - 1])
                {
                    playerCol--;
                }
                else
                {
                    if (spell == "Cloud")
                    {
                        playerHitPoints -= 3500;
                        hasPlayerTakenDamageFromCloudPrevTurn = true;
                    }
                    else
                    {
                        playerHitPoints -= 6000;
                    }

                    if (playerHitPoints <= 0)
                    {
                        isPlayerKilled = true;
                        spellKilledPlayer = spell;
                        break;
                    }
                }
            }

            SetZones(dangerousZones, row, col, false);
        }

        if (isHeiganDefeated)
        {
            Console.WriteLine("Heigan: Defeated!");
        }
        else
        {
            Console.WriteLine($"Heigan: {heiganHitPoints:f2}");
        }

        if (isPlayerKilled)
        {
            Console.WriteLine("Player: Killed by {0}", spellKilledPlayer == "Cloud" ? "Plague Cloud" : spellKilledPlayer);
        }
        else
        {
            Console.WriteLine($"Player: {playerHitPoints}");
        }

        Console.WriteLine($"Final position: {playerRow}, {playerCol}");
    }
}