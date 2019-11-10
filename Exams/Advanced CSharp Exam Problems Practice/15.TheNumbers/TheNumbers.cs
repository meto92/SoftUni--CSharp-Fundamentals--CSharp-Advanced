using System;
using System.Linq;
using System.Text.RegularExpressions;

class TheNumbers
{
    static void Main(string[] args)
    {
        Console.WriteLine(string.Join("-",
            Regex.Matches(Console.ReadLine(), "\\d+")
				.Cast<Match>()
				.Select(m => ushort.Parse(m.Value))
				.Select(num => "0x" + Convert.ToString(num, 16).ToUpper().PadLeft(4, '0'))));
    }
}