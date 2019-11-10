using System;
using System.Text;
using System.Text.RegularExpressions;

class ExtractHyperlinks
{
    static void Main(string[] args)
    {
        StringBuilder html = new StringBuilder();

        string line;

        while ((line = Console.ReadLine()) != "END")
        {
            html.Append(line);
        }
        
        MatchCollection aTags = Regex.Matches(html.ToString(), @"<a(.*?)>.*?<\/a>");

        foreach (Match aTag in aTags)
        {
            Match link = Regex.Match(aTag.Groups[1].Value, @"href\s*=\s*(((""|')(.*?)\3)|(.*?(?=\s+|$)))");

            if (link.Success)
            {
                if (link.Groups[5].Value != string.Empty)
                {
                    Console.WriteLine(link.Groups[5].Value);
                }
                else
                {
                    Console.WriteLine(link.Groups[4].Value);
                }
            }
        }
    }
}