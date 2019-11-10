using System;
using System.Linq;
using System.Text.RegularExpressions;

class CubicMessages
{
    static string GetVerificationMessage(Match match)
    {
        string message = match.Groups[2].Value;
        int[] indices = Regex.Matches(match.Value, "\\d")
            .Cast<Match>()
            .Select(m => int.Parse(m.Value))
            .ToArray();

        return new string(indices.Select(i => i < message.Length ? message[i] : ' ').ToArray());
    }

    static void Main(string[] args)
    {
        string input = null;

        while ((input = Console.ReadLine()) != "Over!")
        {
            int length = int.Parse(Console.ReadLine());

            Regex pattern = new Regex($@"^(\d+)([a-zA-Z]{{{length}}})([^a-zA-Z]*)$");
            Match match = pattern.Match(input);

            if (!match.Success)
            {
                continue;
            }

            string message = match.Groups[2].Value;
            string verificationMessage = GetVerificationMessage(match);

            Console.WriteLine($"{message} == {verificationMessage}");
        }
    }
}