using System;
using System.Text;
using System.Collections.Generic;

class NMS
{
    static List<StringBuilder> GetWords(StringBuilder text)
    {
        List<StringBuilder> words = new List<StringBuilder>();

        words.Add(new StringBuilder());
        words[0].Append(text[0]);

        int lastWordIndex = 0;
        
        for (int i = 1; i < text.Length; i++)
        {
            if (char.ToLower(text[i]) >= char.ToLower(text[i - 1]))
            {
                words[lastWordIndex].Append(text[i]);
            }
            else
            {
                words.Add(new StringBuilder());
                words[++lastWordIndex].Append(text[i]);
            }
        }
        
        return words;
    }

    static void Main(string[] args)
    {
        StringBuilder text = new StringBuilder();
        string input = null;

        while ((input = Console.ReadLine()) != "---NMS SEND---")
        {
            text.Append(input);   
        }

        string delimiter = Console.ReadLine();

        Console.WriteLine(string.Join(delimiter, GetWords(text)));
    }
}