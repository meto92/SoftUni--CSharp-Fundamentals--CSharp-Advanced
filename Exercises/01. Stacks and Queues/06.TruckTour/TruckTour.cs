using System;
using System.Linq;
using System.Collections.Generic;

class PetrolPump
{
    private int amountOfPetrol;
    private int distanceToNextPump;

    public PetrolPump(int amountOfPetrol, int distanceToNextPump)
    {
        this.AmountOfPetrol = amountOfPetrol;
        this.DistanceToNextPump = distanceToNextPump;
    }

    public int AmountOfPetrol
    {
        get { return this.amountOfPetrol; }
        set { this.amountOfPetrol = value; }
    }

    public int DistanceToNextPump
    {
        get { return this.distanceToNextPump; }
        set { this.distanceToNextPump = value; }
    }
}

class TruckTour
{
    static void Main(string[] args)
    {
        int petrolPumpsCount = int.Parse(Console.ReadLine());

        Queue<PetrolPump> petrolPumps = new Queue<PetrolPump>();

        for (int i = 0; i < petrolPumpsCount; i++)
        {
            int[] nums = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            int amountOfPetrol = nums[0],
                distanceToNextPump = nums[1];

            petrolPumps.Enqueue(new PetrolPump(amountOfPetrol, distanceToNextPump));
        }

        for (int i = 0; i < petrolPumpsCount; i++)
        {
            long tankFuel = 0;
            bool hasFuel = true;

            for (int j = 0; j < petrolPumpsCount; j++)
            {
                if (hasFuel)
                {
                    PetrolPump currentPump = petrolPumps.Peek();
                    tankFuel += currentPump.AmountOfPetrol - currentPump.DistanceToNextPump;

                    if (tankFuel < 0)
                    {
                        hasFuel = false;
                    }
                }

                petrolPumps.Enqueue(petrolPumps.Dequeue());
            }

            if (hasFuel)
            {
                Console.WriteLine(i);
                break;
            }

            petrolPumps.Enqueue(petrolPumps.Dequeue());
        }
    }
}