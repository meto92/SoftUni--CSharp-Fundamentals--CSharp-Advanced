using System;
using System.Linq;

class AddVAT
{
    static void Main(string[] args)
    {
        Console.WriteLine(
            string.Join(
                Environment.NewLine,
                Console.ReadLine()
                    .Split(new[] { ", " }, StringSplitOptions.None)
                    .Select(p => $"{double.Parse(p)*1.2:f2}")));
    }
}