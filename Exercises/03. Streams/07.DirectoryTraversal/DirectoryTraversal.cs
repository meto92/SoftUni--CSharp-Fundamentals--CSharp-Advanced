using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class DirectoryTraversal
{
    static string GetDesktopPath()
    {
        DirectoryInfo usersFolder = new DirectoryInfo("c:/users");

        DirectoryInfo[] usersFolderSybdirs = usersFolder.GetDirectories();

        foreach (DirectoryInfo dir in usersFolderSybdirs)
        {
            DirectoryInfo[] subdirs = null;

            try
            {
                subdirs = dir.GetDirectories();
            }
            catch (UnauthorizedAccessException)
            {
                continue;
            }

            foreach (DirectoryInfo subdir in subdirs)
            {
                if (subdir.Name == "Desktop" &&
                    subdir.Parent.Name != "All Users" &&
                    subdir.Parent.Name != "Default" &&
                    subdir.Parent.Name != "Public"
                    )
                {
                    return subdir.FullName;
                }
            }
        }

        return "c:";
    }

    static void Main(string[] args)
    {
        string path = Console.ReadLine();

        DirectoryInfo currentDirectory = null;
        FileInfo[] files = null;

        try
        {
            currentDirectory = new DirectoryInfo(path);
            files = currentDirectory.GetFiles();
        }
        catch (DirectoryNotFoundException dnf)
        {
            Console.WriteLine(dnf.Message);
            return;
        }
        catch (UnauthorizedAccessException uae)
        {
            Console.WriteLine(uae.Message);
            return;
        }

        SortedDictionary<string, List<FileInfo>> filesByExtensions =
            new SortedDictionary<string, List<FileInfo>>();

        foreach (FileInfo file in files)
        {
            string extension = file.Extension;

            if (!filesByExtensions.ContainsKey(extension))
            {
                filesByExtensions[extension] = new List<FileInfo>();
            }

            filesByExtensions[extension].Add(file);
        }

        using (StreamWriter writer = new StreamWriter($"{GetDesktopPath()}/report.txt"))
        {
            foreach (KeyValuePair<string, List<FileInfo>> pair
                in filesByExtensions.OrderByDescending(p => p.Value.Count))
            {
                string extension = pair.Key;

                writer.WriteLine(extension);

                IEnumerable<FileInfo> orderedFilesBySize = pair.Value.OrderBy(file => file.Length);

                foreach (FileInfo file in orderedFilesBySize)
                {
                    double fileSizeInKb = Math.Round(file.Length / 1024.0, 3);

                    writer.WriteLine($"--{file.Name} - {fileSizeInKb}kb");
                }
            }
        }
    }
}