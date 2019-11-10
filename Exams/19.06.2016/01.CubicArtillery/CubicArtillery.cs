using System;
using System.Linq;
using System.Collections.Generic;

class Bunker
{
    private char identifier;
    private Queue<short> weapons;
    private int weaponsSum;
    
    public Bunker(char identifier)
    {
        this.Identifier = identifier;
        this.Weapons = new Queue<short>();
        this.weaponsSum = 0;
    }

    public char Identifier
    {
        get { return identifier; }
        set { identifier = value; }
    }

    public Queue<short> Weapons
    {
        get { return weapons; }
        set { weapons = value; }
    }

    public int WeaponsSum
    {
        get { return weaponsSum; }
        set { weaponsSum = value; }
    }

    public override string ToString()
    {
        return string.Format("{0} -> {1}",
        this.Identifier,
        this.Weapons.Any()
            ? string.Join(", ", this.Weapons)
            : "Empty");
    }
}

class CubicArtillery
{
    static void Main(string[] args)
    {
        int bunkersCapacity = int.Parse(Console.ReadLine());

        Queue<Bunker> bunkers = new Queue<Bunker>();
        string input = null;

        while ((input = Console.ReadLine()) != "Bunker Revision")
        {
            string[] tokens = input.Split(' ');

            foreach (string token in tokens)
            {
                if (!short.TryParse(token, out short weaponCapacity))
                {
                    bunkers.Enqueue(new Bunker(token[0]));
                    continue;                    
                }

                while (bunkers.Count > 1 && bunkers.Peek().WeaponsSum + weaponCapacity > bunkersCapacity)
                {
                    Console.WriteLine(bunkers.Dequeue());
                }

                if (weaponCapacity > 0 && weaponCapacity <= bunkersCapacity)
                {
                    while (bunkers.Peek().WeaponsSum + weaponCapacity > bunkersCapacity)
                    {
                        bunkers.Peek().WeaponsSum -= bunkers.Peek().Weapons.Dequeue();
                    }

                    bunkers.Peek().Weapons.Enqueue(weaponCapacity);
                    bunkers.Peek().WeaponsSum += weaponCapacity;
                }
            }
        }
    }
}