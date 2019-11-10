using System;
using System.Linq;

class CountUppercaseWords
{
    static void Main(string[] args)
    {
        Console.WriteLine(
            string.Join(
                Environment.NewLine,
                Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(word => char.IsUpper(word[0]))));
    }
}