using System;
using System.Text;
using System.Text.RegularExpressions;

class Regeh
{
    static void Main(string[] args)
    {
        string input = Console.ReadLine();

        MatchCollection matches = Regex.Matches(input, @"\[[^\s\[\]]+<(\d+)REGEH(\d+)>[^\s\[\]]+\]");

        int indicesSum = 0;
        StringBuilder result = new StringBuilder();

        foreach (Match match in matches)
        {
            int firstNumber = int.Parse(match.Groups[1].Value);
            int secondNumber = int.Parse(match.Groups[2].Value);

            indicesSum += firstNumber;
            result.Append(input[indicesSum % input.Length]);

            indicesSum += secondNumber;
            result.Append(input[indicesSum % input.Length]);
        }

        Console.WriteLine(result);
    }
}