using System;
using System.Linq;
using System.Collections.Generic;

class KeyRevolver
{
    static void Main(string[] args)
    {
        int bulletPrice = int.Parse(Console.ReadLine());
        int gunBarrelSize = int.Parse(Console.ReadLine());
        Stack<int> bullets = new Stack<int>(Console.ReadLine().
            Split()
            .Select(int.Parse));
        int[] locks = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();
        int intelligenceValue = int.Parse(Console.ReadLine());

        int lockIndex = 0,
            lostMoney = 0,
            shotBullets = 0;

        while (bullets.Count > 0 && lockIndex < locks.Length)
        {
            int bullet = bullets.Pop();
            int currentLock = locks[lockIndex];

            if (bullet <= currentLock)
            {
                Console.WriteLine("Bang!");

                lockIndex++;
            }
            else
            {
                Console.WriteLine("Ping!");
            }

            lostMoney += bulletPrice;
            shotBullets++;

            if (shotBullets % gunBarrelSize == 0 && bullets.Count > 0)
            {
                Console.WriteLine("Reloading!");
            }
        }
        
        if (lockIndex == locks.Length)
        {
            int leftBullets = bullets.Count;
            int earned = intelligenceValue - lostMoney;

            Console.WriteLine($"{leftBullets} bullets left. Earned ${earned}");
        }
        else if (bullets.Count == 0)
        {
            Console.WriteLine($"Couldn't get through. Locks left: {locks.Length - lockIndex}");
        }
    }
}