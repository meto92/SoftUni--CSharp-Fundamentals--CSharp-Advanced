using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class StringMatrixRotation
{
    static void JustPrint(List<string> lines)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            Console.WriteLine(lines[i]);
        }
    }

    static void Rotate90(List<string> lines, int length)
    {
        for (int i = 0; i < length; i++)
        {
            for (int j = lines.Count - 1; j >= 0; j--)
            {
                Console.Write(lines[j][i]);
            }

            Console.WriteLine();
        }
    }

    static void Rotate180(List<string> lines)
    {
        for (int i = lines.Count - 1; i >= 0; i--)
        {
            Console.WriteLine(new string(lines[i].Reverse().ToArray()));
        }
    }

    static void Rotate270(List<string> lines, int length)
    {
        for (int i = length - 1; i >= 0; i--)
        {
            for (int j = 0; j < lines.Count; j++)
            {
                Console.Write(lines[j][i]);
            }

            Console.WriteLine();
        }
    }

    static void Main(string[] args)
    {
        Regex pattern = new Regex(@"(?<=Rotate)(\((\d+)\))");

        int degrees = int.Parse(pattern.Match(Console.ReadLine()).Groups[2].Value) % 360;

        int length = 0;

        List<string> lines = new List<string>();

        string input;

        while ((input = Console.ReadLine()) != "END")
        {
            lines.Add(input);

            if (input.Length > length)
            {
                length = input.Length;
            }
        }

        for (int i = 0; i < lines.Count; i++)
        {
            lines[i] = lines[i].PadRight(length, ' ');
        }

        switch (degrees)
        {
            case 0:
                JustPrint(lines);
                break;
            case 90:
                Rotate90(lines, length);
                break;
            case 180:
                Rotate180(lines);
                break;
            case 270:
                Rotate270(lines, length);
                break;
            default:
                break;
        }
    }    
}