using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

class Team
{
    private string teamName;
    private int wins;
    private SortedSet<string> opponents;

    public string TeamName
    {
        get { return teamName; }
        set { teamName = value; }
    }

    public int Wins
    {
        get { return wins; }
        set { wins = value; }
    }

    public SortedSet<string> Opponents
    {
        get { return opponents; }
        set { opponents = value; }
    }

    public Team(string teamName)
    {
        this.TeamName = teamName;
        wins = 0;
        this.Opponents = new SortedSet<string>();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append($"{this.TeamName}{Environment.NewLine}");
        sb.Append($"- Wins: {this.Wins}{Environment.NewLine}");
        sb.Append($"- Opponents: ");
        sb.Append(string.Join(", ", this.Opponents));

        return sb.ToString();
    }
}

class ChampionsLeague
{
    static string GetWinner(string firstTeam, string secondTeam, int[] firstMatchResult, int[] secondMatchResult)
    {
        int firstTeamGoals = firstMatchResult[0] + secondMatchResult[1],
            secondTeamGoals = firstMatchResult[1] + secondMatchResult[0];

        if (firstTeamGoals > secondTeamGoals)
        {
            return firstTeam;
        }

        if (secondTeamGoals > firstTeamGoals)
        {
            return secondTeam;
        }

        int firstTeamGoalsOnAwaySoil = secondMatchResult[1],
            secondTeamGoalsOnAwaySoil = firstMatchResult[1];

        if (firstTeamGoalsOnAwaySoil > secondTeamGoalsOnAwaySoil)
        {
            return firstTeam;
        }

        return secondTeam;
    }

    static void PrintResult(SortedDictionary<string, Team> teams)
    {
        foreach (KeyValuePair<string, Team> team 
            in teams.OrderByDescending(t => t.Value.Wins))
        {
            Console.WriteLine(team.Value);
        }
    }

    static void Main(string[] args)
    {
        SortedDictionary<string, Team> teams = new SortedDictionary<string, Team>();

        string input = null;

        while ((input = Console.ReadLine()) != "stop")
        {
            string[] inputParams = input.Split(new[] { " | " }, StringSplitOptions.None);

            string firstTeam = inputParams[0];
            string secondTeam = inputParams[1];
            int[] firstMatchResult = inputParams[2]
                .Split(':')
                .Select(int.Parse)
                .ToArray();
            int[] secondMatchResult = inputParams[3]
                .Split(':')
                .Select(int.Parse)
                .ToArray();

            if (!teams.ContainsKey(firstTeam))
            {
                teams[firstTeam] = new Team(firstTeam);
            }

            if (!teams.ContainsKey(secondTeam))
            {
                teams[secondTeam] = new Team(secondTeam);
            }

            teams[firstTeam].Opponents.Add(secondTeam);
            teams[secondTeam].Opponents.Add(firstTeam);

            teams[GetWinner(firstTeam, secondTeam, firstMatchResult, secondMatchResult)].Wins++;
        }

        PrintResult(teams);
    }    
}