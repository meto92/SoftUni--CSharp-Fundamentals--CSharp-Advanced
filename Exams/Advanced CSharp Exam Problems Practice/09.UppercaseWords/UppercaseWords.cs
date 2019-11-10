using System;
using System.Linq;
using System.Text;
using System.Security;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class UppercaseWords
{
    static bool IsPalindrom(string str)
    {
        for (int i = 0; i < str.Length / 2; i++)
        {
            if (str[i] != str[str.Length - i - 1])
            {
                return false;
            }
        }

        return true;
    }

    static string GetReplacement(Match match)
    {
        string word = match.Groups[1].Value;

        if (IsPalindrom(word))
        {
            StringBuilder doubledWord = new StringBuilder();

            for (int i = 0; i < word.Length; i++)
            {
                char letter = word[i];

                doubledWord.Append($"{letter}{letter}");
            }

            return doubledWord.ToString();
        }

        return new string(word.Reverse().ToArray());
    }

    static void ReplaceUppercaseWords(StringBuilder text, Dictionary<Match, string> replacementsByMatches)
    {
        foreach (KeyValuePair<Match, string> pair in replacementsByMatches.Reverse())
        {
            string upcaseWord = pair.Key.Value;
            int index = pair.Key.Index;
            string replacement = pair.Value;

            text.Remove(index, upcaseWord.Length);
            text.Insert(index, replacement);
        }
    }

    static void Main(string[] args)
    {
        StringBuilder text = new StringBuilder();

        string input;

        while ((input = Console.ReadLine()) != "END")
        {
            text.Append($"{input}{Environment.NewLine}");            
        }

        text.Length--;

        Regex pattern = new Regex(@"(?<![a-zA-Z])([A-Z]+)(?![a-zA-Z])");
        MatchCollection matches = pattern.Matches(text.ToString());
        Dictionary<Match, string> replacementsByMatches = new Dictionary<Match, string>();

        foreach (Match match in matches)
        {
            replacementsByMatches[match] = GetReplacement(match);
        }

        ReplaceUppercaseWords(text, replacementsByMatches);

        Console.WriteLine(SecurityElement.Escape(text.ToString()));
    }
}