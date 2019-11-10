using System;
using System.Text;
using System.Text.RegularExpressions;

class PhoneNumbers
{
    static void Main(string[] args)
    {        
        Regex pattern = new Regex(@"(?<name>[A-Z][a-zA-Z]*)[^a-zA-Z+]*?(?<number>\+?\d[\d\(\)\/\.\s-]*\d)");

        StringBuilder str = new StringBuilder();

        string input;

        while ((input = Console.ReadLine()) != "END")
        {
            str.Append(input);
        }

        MatchCollection matches = pattern.Matches(str.ToString());

        if (matches.Count == 0)
        {
            Console.WriteLine("<p>No matches!</p>");
        }
        else
        {
            StringBuilder html = new StringBuilder("<ol>");

            foreach (Match match in matches)
            {
                string name = match.Groups["name"].Value;
                string number = Regex.Replace(match.Groups["number"].Value, @"[\(\)\/\.\s-]", "");
                
                html.Append($"<li><b>{name}:</b> {number}</li>");
            }

            html.Append("</ol>");

            Console.WriteLine(html);
        }
    }
}