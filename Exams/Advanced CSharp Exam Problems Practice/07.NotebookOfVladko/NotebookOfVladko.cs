using System;
using System.Linq;
using System.Collections.Generic;

class TennisPlayer
{
    private string name;
    private int age;
    private int wins;
    private int losses;
    private List<string> opponents;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Age
    {
        get { return age; }
        set { age = value; }
    }

    public int Wins
    {
        get { return wins; }
        set { wins = value; }
    }

    public int Losses
    {
        get { return losses; }
        set { losses = value; }
    }

    public List<string> Opponents
    {
        get { return opponents; }
        set { opponents = value; }
    }

    public TennisPlayer()
    {
        this.Name = string.Empty;
        this.Age = -1;
        this.Wins = 0;
        this.Losses = 0;
        this.Opponents = new List<string>();
    }

    public double Rank => ((double)this.wins + 1) / (this.losses + 1);

    public override string ToString()
    {
        string opponents = this.opponents.Count == 0
            ? "(empty)"
            : string.Join(", ", this.opponents.OrderBy(p => p, StringComparer.Ordinal));

        return $"-age: {this.age}{Environment.NewLine}-name: {this.name}{Environment.NewLine}-opponents: {opponents}{Environment.NewLine}-rank: {this.Rank:f2}";
    }
}

class NotebookOfVladko
{
    static void Main(string[] args)
    {
        SortedDictionary<string, TennisPlayer> tennisPlayersByColors = new SortedDictionary<string, TennisPlayer>();
        
        string input;

        while ((input = Console.ReadLine()) != "END")
        {
            string[] inputParams = input.Split('|');

            string color = inputParams[0];

            if (!tennisPlayersByColors.ContainsKey(color))
            {
                tennisPlayersByColors[color] = new TennisPlayer();
            }

            if (inputParams[1] == "name")
            {
                string playerName = inputParams[2];

                tennisPlayersByColors[color].Name = playerName;
            }
            else if (inputParams[1] == "age")
            {
                int age = int.Parse(inputParams[2]);

                tennisPlayersByColors[color].Age = age;
            }
            else
            {
                bool isWin = inputParams[1] == "win";
                string opponentName = inputParams[2];

                tennisPlayersByColors[color].Opponents.Add(opponentName);

                if (isWin)
                {
                    tennisPlayersByColors[color].Wins++;
                }
                else
                {
                    tennisPlayersByColors[color].Losses++;
                }
            }
        }

        int colorsWithoutNeededInfoCount = 0;

        foreach (KeyValuePair<string, TennisPlayer> pair in tennisPlayersByColors)
        {
            string color = pair.Key;
            TennisPlayer tennisPlayer = pair.Value;

            if (tennisPlayer.Age == -1 || tennisPlayer.Name == string.Empty)
            {
                colorsWithoutNeededInfoCount++;
            }
            else
            {
                Console.WriteLine($"Color: {color}");
                Console.WriteLine(tennisPlayer);
            }
        }

        if (colorsWithoutNeededInfoCount == tennisPlayersByColors.Count)
        {
            Console.WriteLine("No data recovered.");
        }
    }
}