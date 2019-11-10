using System;
using System.Text;
using System.Text.RegularExpressions;

class SemanticHTML
{
    static void Main(string[] args)
    {
        Regex openingDivPattern = new Regex(@"\s*(id|class)\s*=\s*""(.*?)""\s*");
        Regex closingDivPattern = new Regex(@"^(\s*)(<\/div>\s+<!--\s*(.*?)\s*-->)");

        StringBuilder html = new StringBuilder();

        string line;

        while ((line = Console.ReadLine()) != "END")
        {
            if (Regex.IsMatch(line, @"\s*<\/?div"))
            {
                Match closingDiv = closingDivPattern.Match(line);

                if (closingDiv.Success) // closing div
                {
                    string leadingSpaces = closingDiv.Groups[1].Value;
                    string value = closingDiv.Groups[3].Value;

                    line = $"{leadingSpaces}</{value}>";
                }
                else // opening div
                {
                    string leadingSpaces = Regex.Match(line, @"^\s*").Value;                    

                    line = Regex.Replace(line, @"\s{2,}", " ");
                    line = string.Format("{0}{1}",
                        leadingSpaces, 
                        line[0] == ' ' ? line.Substring(1) : line);

                    Match openingDiv = openingDivPattern.Match(line);

                    string value = openingDiv.Groups[2].Value;

                    line = Regex.Replace(line.Replace(openingDiv.Value, " ").Replace
                        ("div", value), @"\s+>", ">");
                }
            }

            html.Append($"{line}{Environment.NewLine}");
        }

        html.Length--;
        Console.WriteLine(html);
    }
}