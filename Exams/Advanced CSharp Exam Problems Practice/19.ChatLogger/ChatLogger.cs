using System;
using System.Linq;
using System.Security;
using System.Globalization;
using System.Collections.Generic;

class ChatLogger
{
    static string GetPeriodMessage(DateTime lastMessageDate, DateTime currentDate)
    {
        DateTime nextDay = lastMessageDate.AddDays(1);

        if (nextDay.Day == currentDate.Day &&
            nextDay.Month == currentDate.Month &&
            nextDay.Year == currentDate.Year)
        {
            return "yesterday";
        }

        TimeSpan span = currentDate - lastMessageDate;

        if (span.Days > 1)
        {
            return $"{lastMessageDate:dd-MM-yyyy}";
        }

        if (span.TotalMinutes < 1)
        {
            return "a few moments ago";
        }

        if (span.Hours < 1)
        {
            return $"{span.Minutes} minute(s) ago";
        }

        return $"{span.Hours} hour(s) ago";
    }

    static void Main(string[] args)
    {
        string dateFormat = "dd-MM-yyyy H:m:s";
        DateTime currentDate = DateTime.ParseExact(Console.ReadLine(), dateFormat, CultureInfo.InvariantCulture);
        
        SortedDictionary<DateTime, string> logs = new SortedDictionary<DateTime, string>();

        string input;

        while ((input = Console.ReadLine()) != "END")
        {
            string[] inputParams = input.Split(new[] { " / " }, StringSplitOptions.None);

            string message = SecurityElement.Escape(inputParams[0]);
            DateTime date = DateTime.ParseExact(inputParams[1], dateFormat, CultureInfo.InvariantCulture);

            logs[date] = message;
        }

        string periodMessage = GetPeriodMessage(logs.Last().Key, currentDate);

        Console.WriteLine(string.Join(Environment.NewLine, logs.Values.Select(message => $"<div>{message}</div>")));
        Console.WriteLine($"<p>Last active: <time>{periodMessage}</time></p>");
    }
}