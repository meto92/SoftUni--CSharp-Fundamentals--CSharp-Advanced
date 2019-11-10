using System;
using System.Text;
using System.Text.RegularExpressions;

class TextTransformer
{
    private const string SpecialChars = "$%&'";

    static void AppendText(StringBuilder result, Match match)
    {
        StringBuilder textToAdd = new StringBuilder(match.Groups[2].Value);

        int specialCharWeight = SpecialChars.IndexOf(match.Groups[1].Value[0]) + 1;

        for (int i = 0; i < textToAdd.Length; i++)
        {
            if (i % 2 == 0)
            {
                textToAdd[i] = (char)(textToAdd[i] + specialCharWeight);
            }
            else
            {
                textToAdd[i] = (char)(textToAdd[i] - specialCharWeight);
            }
        }

        result.Append($"{textToAdd} ");
    }

    static void Main(string[] args)
    {
        StringBuilder textSb = new StringBuilder();

        string input;

        while ((input = Console.ReadLine()) != "burp")
        {
            textSb.Append(input);
        }

        string text = Regex.Replace(textSb.ToString(), "\\s{2,}", " ");

        StringBuilder result = new StringBuilder();
        Match match = Regex.Match(text, @"([\$\%\&\'])([^\$%&']+)\1");

        while (match.Success)
        {
            AppendText(result, match);

            match = match.NextMatch();
        }

        Console.WriteLine(result);
    }
}