using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class RageQuit
{
    static void Main(string[] args)
    {
        MatchCollection matches = Regex.Matches(Console.ReadLine(), @"(\D+)(\d+)");

        StringBuilder rageMessage = new StringBuilder();
        HashSet<char> charsUsed = new HashSet<char>();

        foreach (Match match in matches)
        {
            string str = match.Groups[1].Value.ToUpper();
            int count = int.Parse(match.Groups[2].Value);

            if (count == 0)
            {
                continue;
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                sb.Append(str);
            }

            rageMessage.Append(sb);
            str.ToList().ForEach(c => charsUsed.Add(c));
        }

        Console.WriteLine($"Unique symbols used: {charsUsed.Count}");
        Console.WriteLine(rageMessage);
    }
}