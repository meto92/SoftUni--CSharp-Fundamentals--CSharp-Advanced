using System;
using System.Text;
using System.Collections.Generic;

class SimpleTextEditor
{
    static void AddText(StringBuilder text, string[] tokens, Stack<string> textLastStates)
    {
        textLastStates.Push(text.ToString());

        string textToAppend = tokens[1];

        for (int i = 0; i < textToAppend.Length; i++)
        {
            text.Append(textToAppend[i]);
        }
    }

    static void Erase(StringBuilder text, string[] tokens, Stack<string> textLastStates)
    {
        textLastStates.Push(text.ToString());

        int count = int.Parse(tokens[1]);

        text.Length -= count;
    }

    static void UndoLastUpdate(ref StringBuilder text, Stack<string> textLastStates)
    {
        text = new StringBuilder(textLastStates.Pop());
    }

    static void Main(string[] args)
    {
        int operationsCount = int.Parse(Console.ReadLine());

        StringBuilder text = new StringBuilder();
        Stack<string> textLastStates = new Stack<string>();
        
        for (int i = 0; i < operationsCount; i++)
        {
            string[] tokens = Console.ReadLine().Split(' ');
            string operation = tokens[0];

            switch (operation)
            {
                case "1":
                    AddText(text, tokens, textLastStates);
                    break;
                case "2":
                    Erase(text, tokens, textLastStates);
                    break;
                case "3":
                    int index = int.Parse(tokens[1]);
                    
                    Console.WriteLine(text[index - 1]);
                    break;
                case "4":
                    UndoLastUpdate(ref text, textLastStates);
                    break;
            }
        }
    }
}