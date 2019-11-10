using System;
using System.IO;
using System.Collections.Generic;

class SlicingFile
{
    const int BufferSize = 4096;

    static int GetCurrentBufferSize(long sliceSize)
    {
        int currentBufferSize = BufferSize > sliceSize
            ? (int)sliceSize
            : BufferSize;

        return currentBufferSize;
    }

    static string GetExtension(string sourceFile)
    {
        int lastDotIndex = sourceFile.LastIndexOf('.');

        string extension = string.Empty;

        if (lastDotIndex != -1)
        {
            extension = sourceFile.Substring(lastDotIndex);
        }

        return extension;
    }

    static void Slice(string sourceFile, string destinationDirectory, int parts)
    {
        try
        {
            using (FileStream reader = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
            {
                long bytesCount = reader.Length;
                long sliceSize = (long)Math.Ceiling(bytesCount / (double)parts);

                int currentBufferSize = GetCurrentBufferSize(sliceSize);
                string extension = GetExtension(sourceFile);

                for (int i = 0; i < parts; i++)
                {
                    string destinationFilePath = $"{destinationDirectory}\\Part-{i}{extension}";

                    using (FileStream writer = new FileStream(destinationFilePath, FileMode.CreateNew))
                    {
                        long writtenBytes = 0;
                        byte[] buffer = new byte[currentBufferSize];

                        while (writtenBytes < sliceSize)
                        {
                            int readBytes = reader.Read(buffer, 0, currentBufferSize);

                            if (readBytes == 0)
                            {
                                break;
                            }

                            writer.Write(buffer, 0, readBytes);

                            writtenBytes += readBytes;
                        }
                    }
                }
            }
        }
        catch (FileNotFoundException fnf)
        {
            Console.WriteLine(fnf.Message);
        }
        catch (UnauthorizedAccessException uae)
        {
            Console.WriteLine(uae.Message);
        }
        catch (IOException ioex)
        {
            Console.WriteLine(ioex.Message);
        }
    }

    static void Assemble(List<string> files, string destinationDirectory)
    {
        try
        {
            int lastDotIndex = files[0].LastIndexOf('.');
            string extension = files[0].Substring(lastDotIndex);

            string destinationFilePath = $"{destinationDirectory}\\assembled{extension}";

            using (FileStream writer = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
            {
                foreach (string filePiecePath in files)
                {
                    using (FileStream reader = new FileStream(filePiecePath, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[BufferSize];

                        while (true)
                        {
                            int readBytes = reader.Read(buffer, 0, BufferSize);

                            if (readBytes == 0)
                            {
                                break;
                            }

                            writer.Write(buffer, 0, BufferSize);
                        }
                    }
                }
            }
        }
        catch (FileNotFoundException fnf)
        {
            Console.WriteLine(fnf.Message);
        }
        catch (UnauthorizedAccessException uae)
        {
            Console.WriteLine(uae.Message);
        }
        catch (IOException ioex)
        {
            Console.WriteLine(ioex.Message);
        }
    }

    static void Main(string[] args)
    {
        
    }
}