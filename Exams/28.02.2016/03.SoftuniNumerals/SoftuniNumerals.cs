using System;
using System.Linq;
using System.Numerics;

class SoftuniNumerals
{
    static BigInteger ConvertFromBaseFiveToDecimal(string baseFiveNumberStr)
    {
        string reversed = new string(baseFiveNumberStr.Reverse().ToArray());

        BigInteger decimalNumber = 0;

        for (int i = 0; i < reversed.Length; i++)
        {
            decimalNumber += int.Parse(reversed[i].ToString()) * BigInteger.Pow(5, i);
        }

        return decimalNumber;
    }

    static void Main(string[] args)
    {
        string numeralString = Console.ReadLine();

        string baseFiveNumberStr = numeralString
            .Replace("aba", "1")
            .Replace("aa", "0")
            .Replace("bcc", "2")
            .Replace("cdc", "4")
            .Replace("cc", "3");
        
        BigInteger decimalNumber = ConvertFromBaseFiveToDecimal(baseFiveNumberStr);

        Console.WriteLine(decimalNumber);
    }
}