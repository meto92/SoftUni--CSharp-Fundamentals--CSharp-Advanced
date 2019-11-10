using System;
using System.IO;

class LineNumbers
{
    static void Main(string[] args)
    {
        string path = "../../../test.txt";

        if (!File.Exists(path))
        {
            Console.WriteLine("Non-existing file!");
            return;
        }

        try
        {
            File.GetAccessControl(path);
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Access denied!");
            return;
        }

        using (StreamReader reader = new StreamReader(path))
        {
            using (StreamWriter writer = new StreamWriter("../../../result.txt"))
            {
                int lineNumber = 0;
                string line = null;

                while ((line = reader.ReadLine()) != null)
                {
                    writer.WriteLine($"Line {++lineNumber}: {line}");
                }
            }
        }
    }
}