using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class ShmoogleCounter
{
    static void Main(string[] args)
    {
        StringBuilder code = new StringBuilder();

        string line = null;
        while ((line = Console.ReadLine()) != "//END_OF_CODE")
        {
            code.Append(line);
        }

        Regex pattern = new Regex(@"(int|double)\s+([a-z][a-zA-Z]{0,24})(?=\s*(;|,|=|\)))");
        List<string> ints = new List<string>();
        List<string> doubles = new List<string>();
        MatchCollection matches = pattern.Matches(code.ToString());

        foreach (Match match in matches)
        {
            string type = match.Groups[1].Value;
            string variableName = match.Groups[2].Value;

            if (type == "int")
            {
                ints.Add(variableName);
            }
            else if (type == "double")
            {
                doubles.Add(variableName);
            }
        }

        Console.WriteLine("Doubles: {0}",
            doubles.Count == 0 
            ? "None" 
            : string.Join(", ", doubles.OrderBy(p => p)));
        Console.WriteLine("Ints: {0}",
            ints.Count == 0
            ? "None"
            : string.Join(", ", ints.OrderBy(p => p)));
    }
}