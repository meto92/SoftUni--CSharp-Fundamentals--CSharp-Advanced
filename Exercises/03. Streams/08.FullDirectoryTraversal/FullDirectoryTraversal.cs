using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class FullDirectoryTraversal
{
    static SortedDictionary<string, List<FileInfo>> filesByExtensions = 
        new SortedDictionary<string, List<FileInfo>>();

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

    static void ReportFiles()
    {
        using (StreamWriter writer = new StreamWriter($"{GetDesktopPath()}/report.txt"))
        {
            foreach (KeyValuePair<string, List<FileInfo>> pair
                in filesByExtensions.OrderByDescending(p => p.Value.Count))
            {
                string extension = pair.Key;

                writer.WriteLine(extension);

                IEnumerable<FileInfo> sortedFilesBySize = pair.Value.OrderBy(file => file.Length);

                foreach (FileInfo file in sortedFilesBySize)
                {
                    double fileSizeInKb = Math.Round(file.Length / 1024.0, 3);

                    writer.WriteLine($"--{file.Name} - {fileSizeInKb}kb");
                }
            }
        }
    }

    static void AddFiles(FileInfo[] files)
    {
        foreach (FileInfo file in files)
        {
            string extension = file.Extension;

            if (!filesByExtensions.ContainsKey(extension))
            {
                filesByExtensions[extension] = new List<FileInfo>();
            }

            filesByExtensions[extension].Add(file);
        }
    }

    static void DirDFS(string path)
    {
        DirectoryInfo currentDirectory = null;
        DirectoryInfo[] subdirs = null;
        FileInfo[] files = null;

        try
        {
            currentDirectory = new DirectoryInfo(path);
            subdirs = currentDirectory.GetDirectories();
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

        foreach (DirectoryInfo dir in subdirs)
        {
            DirDFS(dir.FullName);
        }

        AddFiles(files);
    }

    static void Main(string[] args)
    {
        string path = Console.ReadLine();

        DirDFS(path);
        ReportFiles();
    }
}