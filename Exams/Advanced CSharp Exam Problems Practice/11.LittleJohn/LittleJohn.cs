using System;
using System.Linq;
using System.Text.RegularExpressions;

class LittleJohn
{
    static void Main(string[] args)
    {
        string smallArrow = ">----->";
        string mediumArrow = ">>----->";
        string largeArrow = ">>>----->>";

        int smallArrowsCount = 0,
            mediumArrowsCount = 0,
            largeArrowsCount = 0;

        for (int i = 0; i < 4; i++)
        {
            string hay = Console.ReadLine();

            largeArrowsCount += Regex.Matches(hay, largeArrow).Count;

            hay = hay.Replace(largeArrow, "^");
            mediumArrowsCount += Regex.Matches(hay, mediumArrow).Count;

            hay = hay.Replace(mediumArrow, "^");
            smallArrowsCount += Regex.Matches(hay, smallArrow).Count;
        }

        string binary = Convert.ToString(int.Parse($"{smallArrowsCount}{mediumArrowsCount}{largeArrowsCount}"), 2);
        string binaryReversed = new string(binary.Reverse().ToArray());

        Console.WriteLine(Convert.ToInt32($"{binary}{binaryReversed}", 2));
    }
}