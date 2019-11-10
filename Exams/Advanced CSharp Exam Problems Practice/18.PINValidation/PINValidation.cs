using System;
using System.Linq;
using System.Text.RegularExpressions;

class PINValidation
{
    static bool IsCheckSumValid(int[] PIN)
    {
        int sum = 0;
        int[] multipliers = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };

        for (int i = 0; i < 9; i++)
        {
            sum += PIN[i] * multipliers[i];
        }

        return PIN[9] == sum % 11 % 10;
    }

    static bool IsDateValid(int[] PIN)
    {
        int month = int.Parse($"{PIN[2]}{PIN[3]}");
        int day = int.Parse($"{PIN[4]}{PIN[5]}");

        if (day > 31 || month > 52)
        {
            return false;
        }

        while (month > 12)
        {
            month -= 20;
        }

        int[] daysInMonths = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        if (day > daysInMonths[month - 1])
        {
            return false;
        }

        return true;
    }

    static bool IsPINValid(int[] PIN)
    {
        return 
            PIN.Length == 10 &&
            IsCheckSumValid(PIN) &&
            IsDateValid(PIN);
    }

    static bool IsGenderValid(int[] PIN, bool isMale)
    {
        return PIN[8] % 2 == 0 && isMale ||
            PIN[8] % 2 == 1 && !isMale;
    }

    static bool IsNameValid(string name)
    {
        return Regex.IsMatch(name, @"^[A-Z][a-z]+\s*[A-Z][a-z]+$");
    }

    static void Main(string[] args)
    {
        string name = Console.ReadLine().Trim();
        string gender = Console.ReadLine();
        int[] PIN = Console.ReadLine()
            .ToCharArray()
            .Select(c => int.Parse(c.ToString()))
            .ToArray();

        bool isMale = gender == "male";

        bool isValid =
            IsPINValid(PIN) &&
            IsGenderValid(PIN, isMale) &&
            IsNameValid(name);

        if (isValid)
        {
            Console.Write("{\"");
            Console.Write(@"name"":""{0}"",""gender"":""{1}"",""pin"":""{2}",
                name, gender, string.Join("", PIN));
            Console.WriteLine("\"}");
        }
        else
        {
            Console.WriteLine("<h2>Incorrect data</h2>");
        }
    }    
}