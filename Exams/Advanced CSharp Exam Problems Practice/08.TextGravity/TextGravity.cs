using System;
using System.Linq;
using System.Text;
using System.Security;
using System.Collections.Generic;

class TextGravity
{
    static void FillTable(List<char[]> table, string text, int lineLength)
    {
        int row = 0;

        while (row * lineLength < text.Length)
        {
            table.Add(new char[lineLength]);
            table[row] = text.Skip(row * lineLength).Take(lineLength).ToArray();

            row++;
        }

        table[table.Count - 1] = new string(table[table.Count - 1]).PadRight(lineLength, ' ').ToCharArray();
    }

    static void FallChars(List<char[]> table)
    {
        int lineLength = table[0].Length;

        for (int row = table.Count - 1; row > 0; row--)
        {
            for (int col = 0; col < lineLength; col++)
            {
                if (table[row][col] == ' ')
                {
                    int i = row - 1;

                    while (i >= 0 && table[i][col] == ' ')
                    {
                        i--;
                    }

                    if (i >= 0)
                    {
                        table[row][col] = table[i][col];
                        table[i][col] = ' ';
                    }
                }
            }
        }
    }

    static void PrintHTMLTable(List<char[]> table)
    {
        StringBuilder result = new StringBuilder("<table>");

        foreach (char[] row in table)
        {
            result.Append("<tr>");

            foreach (char c in row)
            {
                result.Append($"<td>{SecurityElement.Escape(c.ToString())}</td>");
            }

            result.Append("</tr>");
        }

        result.Append("</table>");

        Console.WriteLine(result);
    }

    static void Main(string[] args)
    {
        int lineLength = int.Parse(Console.ReadLine());
        string text = Console.ReadLine();

        List<char[]> table = new List<char[]>();

        FillTable(table, text, lineLength);
        FallChars(table);
        PrintHTMLTable(table);
    }    
}