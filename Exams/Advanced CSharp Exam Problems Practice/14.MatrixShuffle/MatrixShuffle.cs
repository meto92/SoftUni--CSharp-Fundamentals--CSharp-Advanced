using System;
using System.Text;
using System.Text.RegularExpressions;

class MatrixShuffle
{
    static void FillMatrix(char[,] matrix, string str)
    {
        int row = 0,
            col = 0,
            size = matrix.GetLength(0),
            index = 0,
            counter = 0;

        while (index < str.Length - 1)
        {
            switch (counter % 4)
            { 
                // R
                case 0:
                    while (col < size - 1 && matrix[row, col + 1] == '\0')
                    {
                        matrix[row, col] = str[index];
                        col++;
                        index++;
                    }
                    break;
                // D
                case 1:
                    while (row < size - 1 && matrix[row + 1, col] == '\0')
                    {
                        matrix[row, col] = str[index];
                        row++;
                        index++;
                    }
                    break;
                // L
                case 2: 
                    while (col > 0 && matrix[row, col - 1] == '\0')
                    {
                        matrix[row, col] = str[index];
                        col--;
                        index++;
                    }
                    break;
                // U
                case 3: 
                    while (row > 0 && matrix[row - 1, col] == '\0')
                    {
                        matrix[row, col] = str[index];
                        row--;
                        index++;
                    }
                    break;
            }

            counter++;
        }

        matrix[row, col] = str[index];
    }

    static string GetSentense(char[,] matrix)
    {
        StringBuilder sentence = new StringBuilder();

        int size = matrix.GetLength(0);

        for (int row = 0; row < size; row++)
        {
            for (int col = row % 2 == 0 ? 0 : 1; col < size; col += 2)
            {
                sentence.Append(matrix[row, col]);
            }
        }

        for (int row = 0; row < size; row++)
        {
            for (int col = row % 2 == 0 ? 1 : 0; col < size; col += 2)
            {
                sentence.Append(matrix[row, col]);
            }
        }

        return sentence.ToString();
    }

    static bool IsPalindrome(string sentence)
    {
        string str = Regex.Replace(sentence.ToLower(), @"[^a-z]+", "");

        for (int i = 0; i < str.Length / 2; i++)
        {
            if (str[i] != str[str.Length - i - 1])
            {
                return false;
            }
        }

        return true;
    }

    static void Main(string[] args)
    {
        int size = int.Parse(Console.ReadLine());
        string str = Console.ReadLine();

        char[,] matrix = new char[size, size];

        FillMatrix(matrix, str);

        string sentence = GetSentense(matrix);

        Console.WriteLine("<div style='background-color:{0}'>{1}</div>",
            IsPalindrome(sentence) ? "#4FE000" : "#E0000F", sentence);
    }    
}