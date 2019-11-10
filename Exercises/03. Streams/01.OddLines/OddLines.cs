using System;
using System.IO;

class OddLines
{
    static void Main(string[] args)
    {
        string path = "../../../test.txt";

        if (!File.Exists(path))
        {
            Console.WriteLine(path);
            Console.WriteLine("File not found!");
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
            string line = reader.ReadLine();

            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);

                reader.ReadLine();
            }
        }
    }
}