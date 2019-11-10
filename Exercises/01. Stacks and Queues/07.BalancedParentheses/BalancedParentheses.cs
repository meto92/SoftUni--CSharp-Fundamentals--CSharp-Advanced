using System;
using System.Collections.Generic;

class BalancedParentheses
{
    static void Main(string[] args)
    {
        string parentheses = Console.ReadLine();

        int length = parentheses.Length;
        Stack<int> brackets = new Stack<int>();
        Stack<int> squareBrackets = new Stack<int>();
        Stack<int> braces = new Stack<int>();

        for (int i = 0; i < length; i++)
        {
            char ch = parentheses[i];

            if (ch == '(')
            {
                brackets.Push(i);
            }
            else if (ch == ')')
            {
                if (brackets.Count == 0 || (i - brackets.Peek() - 1) % 2 != 0)
                {
                    Console.WriteLine("NO");
                    return;
                }

                brackets.Pop();
            }
            else if (ch == '[')
            {
                squareBrackets.Push(i);
            }
            else if (ch == ']')
            {
                if (squareBrackets.Count == 0 || (i - squareBrackets.Peek() - 1) % 2 != 0)
                {
                    Console.WriteLine("NO");
                    return;
                }

                squareBrackets.Pop();
            }
            else if (ch == '{')
            {
                braces.Push(i);
            }
            else if (ch == '}')
            {
                if (braces.Count == 0 || (i - braces.Peek() - 1) % 2 != 0)
                {
                    Console.WriteLine("NO");
                    return;
                }

                braces.Pop();
            }
        }

        if (brackets.Count == 0 && squareBrackets.Count == 0 && braces.Count == 0)
        {
            Console.WriteLine("YES");
        }
        else
        {
            Console.WriteLine("NO");
        }
    }
}