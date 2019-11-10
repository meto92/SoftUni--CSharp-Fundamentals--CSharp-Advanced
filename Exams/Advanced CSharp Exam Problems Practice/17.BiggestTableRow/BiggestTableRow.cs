using System;
using System.Linq;
using System.Text.RegularExpressions;

class BiggestTableRow
{
    static void Main(string[] args)
    {
        double highestSum = double.NegativeInfinity;
        string[] highestSumValues = new string[0];
        Regex pattern = new Regex(@"(?<=<td>)-?((\d+(\.\d+)?)|(\.\d+))(?=</td>)");
        
        string input;

        while ((input = Console.ReadLine()) != "</table>")
        {
            string[] currentValues = pattern.Matches(input)
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            double currentSum = currentValues.Select(double.Parse).Sum();

            if (currentSum > highestSum  && currentValues.Length > 0)
            {
                highestSum = currentSum;
                highestSumValues = currentValues;
            }
        }

        if (!highestSumValues.Any())
        {
            Console.WriteLine("no data");
        }
        else
        {
            Console.WriteLine($"{highestSum} = {string.Join(" + ", highestSumValues)}");
        }
    }
}