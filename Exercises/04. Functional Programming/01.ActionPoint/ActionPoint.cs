using System;
using System.Linq;

class ActionPoint
{
    static void Main(string[] args)
    {
        string[] strings = Console.ReadLine().Split(' ');

        Action<string> printOnNewLine = str => Console.WriteLine(str);

        strings.ToList().ForEach(printOnNewLine);
    }
}