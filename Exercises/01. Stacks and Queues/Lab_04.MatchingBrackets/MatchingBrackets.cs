using System;
using System.Collections.Generic;

class MatchingBrackets
{
    static void Main(string[] args)
    {
        string expression = Console.ReadLine();

        Stack<int> openingBracketsIndices = new Stack<int>();

        for (int i = 0; i < expression.Length; i++)
        {
            char character = expression[i];

            if (character == '(')
            {
                openingBracketsIndices.Push(i);
            }
            else if (character == ')')
            {
                int openingBracketIndex = openingBracketsIndices.Pop(),
                    subexpressionLength = i + 1 - openingBracketIndex;

                Console.WriteLine(expression.Substring(openingBracketIndex, subexpressionLength));
            }
        }
    }
}