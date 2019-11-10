using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class BasicMarkupLanguage
{
    static string Inverse(string content)
    {
        StringBuilder inversed = new StringBuilder(content);

        for (int i = 0; i < inversed.Length; i++)
        {
            if (char.IsUpper(inversed[i]))
            {
                inversed[i] = char.ToLower(inversed[i]);
            }
            else
            {
                inversed[i] = char.ToUpper(inversed[i]);
            }
        }

        return inversed.ToString();
    }

    static string Reverse(string content)
    {
        return new string(content.Reverse().ToArray());
    }

    static void Print(int lineNumber, string content)
    {
        Console.WriteLine($"{lineNumber}. {content}");
    }

    static void Main(string[] args)
    {
        Regex[] patterns =
        {
            new Regex(@"<\s*(inverse|reverse)\s+content\s*=\s*""([^""]+)""\s*\/\s*>"),
            new Regex(@"<\s*repeat\s+value\s*=\s*""(\d+)""\s+content\s*=\s*""([^""]+)""\s*\/\s*>")
        };

        int lineNumber = 0;
        string input = null;

        while ((input = Console.ReadLine().Trim()) != "<stop/>")
        {
            Match match = null;
            string content = null;

            if (patterns[0].IsMatch(input))
            {
                match = patterns[0].Match(input);

                string command = match.Groups[1].Value;
                content = match.Groups[2].Value;

                string result = command == "inverse"
                    ? Inverse(content)
                    : Reverse(content);

                Print(++lineNumber, result);
            }
            else if (patterns[1].IsMatch(input))
            {
                match = patterns[1].Match(input);

                int repetitions = int.Parse(match.Groups[1].Value);
                content = match.Groups[2].Value;

                for (int i = 0; i < repetitions; i++)
                {
                    Print(++lineNumber, content);
                }
            }
        }
    }
}