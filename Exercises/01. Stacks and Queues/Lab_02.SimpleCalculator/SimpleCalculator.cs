using System;
using System.Linq;
using System.Collections.Generic;

class SimpleCalculator
{
    static void Main(string[] args)
    {
        Stack<string> tokens = 
            new Stack<string>(Console.ReadLine().Split(' ').Reverse());

        while (tokens.Count > 1)
        {
            int leftOperand = int.Parse(tokens.Pop());
            string operation = tokens.Pop();
            int rightOperand = int.Parse(tokens.Pop());

            int result = 0;

            if (operation == "+")
            {
                result = leftOperand + rightOperand;
            }
            else if (operation == "-")
            {
                result = leftOperand - rightOperand;
            }

            tokens.Push(result.ToString());
        }

        Console.WriteLine(tokens.Peek());
    }
}