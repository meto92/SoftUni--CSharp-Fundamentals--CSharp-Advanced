using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class QueryMess
{
    static void PrintPairs(string input)
    {
        Dictionary<string, List<string>> pairs = new Dictionary<string, List<string>>();
        string[] keyValues = input.Split('&');

        for (int i = 0; i < keyValues.Length; i++)
        {
            string[] keyValue = keyValues[i].Split('=');

            string key = keyValue[0].Trim();
            string value = keyValue[1].Trim();

            if (!pairs.ContainsKey(key))
            {
                pairs[key] = new List<string>();
            }

            pairs[key].Add(value);
        }

        Console.WriteLine(string.Join("", pairs.Select(pair => $"{pair.Key}=[{string.Join(", ", pairs[pair.Key])}]")));
    }

    static void Main(string[] args)
    {
        string input;

        while ((input = Console.ReadLine()) != "END")
        {
            string[] tokens = input.Split('?');

            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i].Contains('='))
                {
                    tokens[i] = Regex.Replace(Regex.Replace(tokens[i], @"\+|%20", " "), @"\s{2,}", " ");

                    PrintPairs(tokens[i]);
                }
            }
        }
    }
}