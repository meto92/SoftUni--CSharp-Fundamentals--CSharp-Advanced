using System;
using System.Linq;
using System.Collections.Generic;

class FilterByAge
{
    static Func<KeyValuePair<string, int>, bool> GetConditionFunc(string condition, int age)
    {
        if (condition == "younger")
        {
            return pair => pair.Value < age;
        }
        else if (condition == "older")
        {
            return pair => pair.Value >= age;
        }

        return null;
    }

    static Action<KeyValuePair<string, int>> GetPrintAction(string format)
    {
        switch (format)
        {
            case "name":
                return pair => Console.WriteLine(pair.Key);
            case "age":
                return pair => Console.WriteLine(pair.Value);
            case "name age":
                return pair => Console.WriteLine($"{pair.Key} - {pair.Value}");
        }

        return null;
    }

    static void Main(string[] args)
    {
        int peopleCount = int.Parse(Console.ReadLine());

        List<KeyValuePair<string, int>> pairs = new List<KeyValuePair<string, int>>();

        for (int i = 0; i < peopleCount; i++)
        {
            string[] inputParams = Console.ReadLine()
                .Split(new[] { ", " }, StringSplitOptions.None);

            string name = inputParams[0];
            int personAge = int.Parse(inputParams[1]);

            pairs.Add(new KeyValuePair<string, int>(name, personAge));
        }

        string condition = Console.ReadLine();
        int age = int.Parse(Console.ReadLine());
        string format = Console.ReadLine();

        Func<KeyValuePair<string, int>, bool> conditionFunc = GetConditionFunc(condition, age);    
        Action<KeyValuePair<string, int>> printAction = GetPrintAction(format);

        pairs.Where(conditionFunc).ToList().ForEach(printAction);
    }
}