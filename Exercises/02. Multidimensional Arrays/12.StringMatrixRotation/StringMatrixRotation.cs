using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class StringMatrixRotation
{
    static List<StringBuilder> RotateWords(List<string> words, int degrees)
    {
        List<StringBuilder> result = new List<StringBuilder>();

        int wordsLength = words[0].Length;

        switch (degrees)
        {
            case 90:
                for (int i = 0; i < wordsLength; i++)
                {
                    result.Add(new StringBuilder());

                    for (int j = words.Count - 1; j >= 0; j--)
                    {
                        result[i].Append(words[j][i]);
                    }
                }

                break;
            case 180:
                for (int i = words.Count - 1; i >= 0; i--)
                {
                    result.Add(new StringBuilder(new string(words[i].Reverse().ToArray())));
                }

                break;
            case 270:
                for (int i = wordsLength - 1; i >= 0; i--)
                {
                    result.Add(new StringBuilder());

                    for (int j = 0; j < words.Count; j++)
                    {
                        result[result.Count - 1].Append(words[j][i]);
                    }
                }

                break;
        }

        return result;
    }

    static void Main(string[] args)
    {
        int rotationDegrees = 
            int.Parse(
                Regex.Match(Console.ReadLine(), @"\((\d+)\)")
                .Groups[1].Value) % 360;
        
        List<string> words = new List<string>();
        int longestWordLength = 0;

        string word;

        while ((word = Console.ReadLine()) != "END")
        {
            words.Add(word);

            if (word.Length > longestWordLength)
            {
                longestWordLength = word.Length;
            }
        }

        words = words.Select(w => w.PadRight(longestWordLength, ' ')).ToList();

        if (rotationDegrees == 0)
        {
            Console.WriteLine(string.Join(Environment.NewLine, words));
        }
        else
        {
            List<StringBuilder> rotatedWords = RotateWords(words, rotationDegrees);

            Console.WriteLine(string.Join(Environment.NewLine, rotatedWords));
        }
    }
}