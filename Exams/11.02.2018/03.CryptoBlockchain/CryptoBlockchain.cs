using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class CryptoBlockchain
{
    static string ReadBlockChains()
    {
        int rows = int.Parse(Console.ReadLine());

        StringBuilder blockChains = new StringBuilder();

        for (int i = 0; i < rows; i++)
        {
            blockChains.Append(Console.ReadLine());
        }

        return blockChains.ToString();
    }

    static string GetDigits(string value)
    {
        StringBuilder digits = new StringBuilder();
        MatchCollection numbersMatches = Regex.Matches(value, "\\d+");

        foreach (Match numbersMatch in numbersMatches)
        {
            digits.Append(numbersMatch.Value);
        }

        return digits.ToString();
    }

    static List<int> GetNumbers(string digits)
    {
        List<int> numbers = new List<int>();

        for (int i = 0; i < digits.Length; i += 3)
        {
            string threeDigits = digits.Substring(i, 3);

            numbers.Add(int.Parse(threeDigits));
        }

        return numbers;
    }

    static void Main(string[] args)
    {
        string blockChains = ReadBlockChains();        
        Regex regex = new Regex(@"(\[[^\[\]\{\}]+\])|(\{[^\[\]\{\}]+\})");

        MatchCollection matches = regex.Matches(blockChains.ToString());
        StringBuilder decryptedText = new StringBuilder();

        foreach (Match match in matches)
        {
            string digits = GetDigits(match.Value);

            if (digits.Length == 0 || digits.Length % 3 != 0)
            {
                continue;
            }

            List<int> numbers = GetNumbers(digits);

            foreach (int num in numbers)
            {
                decryptedText.Append((char)(num - match.Length));
            }
        }

        Console.WriteLine(decryptedText);
    }
}