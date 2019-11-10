using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class WordCount
{
    const string wordsFilePath = "../../../words.txt";
    const string textFilePath = "../../../text.txt";
    const string outputFilePath = "../../../wordsOccurrences.txt";

    static void VerifyPaths()
    {
        try
        {
            File.GetAccessControl(wordsFilePath);
            File.GetAccessControl(textFilePath);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found!");
            Environment.Exit(1);
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Access denied!");
            Environment.Exit(1);
        }
    }

    static List<string> ReadWords()
    {
        List<string> words = new List<string>();

        using (StreamReader reader = new StreamReader(wordsFilePath))
        {
            string line = null;

            while ((line = reader.ReadLine()) != null)
            {
                words.Add(line);
            }
        }

        return words;
    }

    static string ReadText()
    {
        StringBuilder text = new StringBuilder();

        using (StreamReader reader = new StreamReader(textFilePath))
        {
            string line = null;

            while ((line = reader.ReadLine()) != null)
            {
                text.Append(line);
            }
        }

        return text.ToString();
    }

    static List<Regex> GetPatterns(List<string> words)
    {
        List<Regex> patterns = new List<Regex>();

        foreach (string word in words)
        {
            patterns.Add(new Regex($@"\b{Regex.Escape(word)}\b", RegexOptions.IgnoreCase));
        }

        return patterns;
    }

    static Dictionary<string, int> GetWordsOccurrences(string text, List<string> words, List<Regex> patterns)
    {
        Dictionary<string, int> wordsOccurrences = new Dictionary<string, int>();

        for (int i = 0; i < words.Count; i++)
        {
            string word = words[i];
            Regex pattern = patterns[i];
            int occurrences = pattern.Matches(text).Count;

            wordsOccurrences[word] = occurrences;
        }

        return wordsOccurrences;
    }

    static void WriteWordsOccurrencesInDescendingOrder(Dictionary<string, int> wordsOccurrences)
    {
        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            foreach (KeyValuePair<string, int> pair
                in wordsOccurrences.OrderByDescending(p => p.Value))
            {
                string word = pair.Key;
                int occurrences = pair.Value;

                writer.WriteLine($"{word} - {occurrences}");
            }
        }
    }

    static void Main(string[] args)
    {
        VerifyPaths();

        List<string> words = ReadWords();
        string text = ReadText();
        List<Regex> patterns = GetPatterns(words);

        Dictionary<string, int> wordsOccurrences = GetWordsOccurrences(text, words, patterns);

        WriteWordsOccurrencesInDescendingOrder(wordsOccurrences);
    }
}