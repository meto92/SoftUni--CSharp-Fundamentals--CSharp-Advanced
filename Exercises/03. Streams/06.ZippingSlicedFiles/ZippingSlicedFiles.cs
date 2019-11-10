using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

class ZippingSlicedFiles
{
    const int BufferSize = 4096;

    static int GetCurrentBufferSize(long sliceSize)
    {
        int currentBufferSize = BufferSize > sliceSize
            ? (int)sliceSize
            : BufferSize;

        return currentBufferSize;
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

                for (int i = 0; i < parts; i++)
                {
                    string destinationFilePath = $"{destinationDirectory}\\Part-{i}.gz";

                    using (FileStream writer = new FileStream(destinationFilePath, FileMode.CreateNew))
                    {
                        using (GZipStream zipStream = new GZipStream(writer, CompressionMode.Compress))
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

                                zipStream.Write(buffer, 0, readBytes);

                                writtenBytes += readBytes;
                            }
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
            string destinationFilePath = $"{destinationDirectory}\\assembled";

            using (FileStream writer = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
            {
                foreach (string filePiecePath in files)
                {
                    using (FileStream reader = new FileStream(filePiecePath, FileMode.Open, FileAccess.Read))
                    {
                        using (GZipStream zipStream = new GZipStream(reader, CompressionMode.Decompress))
                        {
                            byte[] buffer = new byte[BufferSize];

                            while (true)
                            {
                                int readBytes = zipStream.Read(buffer, 0, BufferSize);
                                
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