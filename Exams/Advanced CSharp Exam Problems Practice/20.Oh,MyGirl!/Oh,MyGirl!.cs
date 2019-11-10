using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class OhMyGirl
{
    static Regex GetPattern(string key)
    {
        string middleChars = key.Substring(1, key.Length - 2);

        //string keyPattern = Regex.Replace(middleChars, "([^a-zA-Z0-9])", "\\$1");
        //keyPattern = Regex.Replace(keyPattern, "[a-z]", "[a-z]*");
        //keyPattern = Regex.Replace(keyPattern, "[A-Z]", "[A-Z]*");
        //keyPattern = Regex.Replace(keyPattern, "\\d", "\\d*");

        string keyPattern = Regex.Replace(
            Regex.Replace(
                Regex.Replace(
                    Regex.Replace(
                        middleChars, "([^a-zA-Z0-9])", "\\$1"),
                    "[a-z]", "[a-z]*"),
                "[A-Z]", "[A-Z]*"),
            "\\d", "\\d*");

        string firstKeyChar = key[0].ToString();

        if (!char.IsDigit(firstKeyChar[0]) && !char.IsLetter(firstKeyChar[0]))
        {
            firstKeyChar = $"\\{firstKeyChar}";
        }

        string lastKeyChar = key[key.Length - 1].ToString();

        if (!char.IsDigit(lastKeyChar[0]) && !char.IsLetter(lastKeyChar[0]))
        {
            lastKeyChar = $"\\{lastKeyChar}";
        }

        keyPattern = $"{firstKeyChar}{keyPattern}{lastKeyChar}";

        return new Regex($"{keyPattern}(.{{2,6}}){keyPattern}");
    }

    static void Main(string[] args)
    {
        Regex pattern = GetPattern(Console.ReadLine());

        StringBuilder text = new StringBuilder();

        string input;

        while ((input = Console.ReadLine()) != "END")
        {
            text.Append(input);
        }

        Console.WriteLine(string.Join("", pattern.Matches(text.ToString()).Cast<Match>().Select(match => match.Groups[1].Value)));
    }
}