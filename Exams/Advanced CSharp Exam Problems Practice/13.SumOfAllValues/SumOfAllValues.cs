using System;
using System.Linq;
using System.Text.RegularExpressions;

class SumOfAllValues
{
    static void Main(string[] args)
    {
        string keysString = Console.ReadLine();
        string text = Console.ReadLine();

        string startKeyPattern = @"^[a-zA-eq2Z_]+(?=\d)";
        string endKeyPattern = @"(?<=\d)[a-zA-Z_]+$";

        Match startKeyMatch = Regex.Match(keysString, startKeyPattern);
        Match endKeyMatch = Regex.Match(keysString, endKeyPattern);        

        if (!startKeyMatch.Success || !endKeyMatch.Success)
        {
            Console.WriteLine("<p>A key is missing</p>");
            return;
        }

        Regex pattern = new Regex($@"{startKeyMatch.Value}(-?(\d+(\.\d+)?|\.\d+)){endKeyMatch.Value}");

        double sum = pattern.Matches(text)
            .Cast<Match>()
            .Select(m => double.Parse(m.Groups[1].Value))
            .Sum();

        Console.WriteLine("<p>The total value is: <em>{0}</em></p>", 
            sum == 0 ? "nothing" : sum.ToString());
    }
}