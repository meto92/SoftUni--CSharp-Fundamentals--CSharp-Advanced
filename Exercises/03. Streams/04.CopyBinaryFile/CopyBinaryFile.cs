using System;
using System.IO;

class CopyBinaryFile
{
    const string originalFilePath = "../../../image.png";
    const string copyFilePath = "../../../copy.png";

    static void Main(string[] args)
    {
        int bufferSize = 4096;

        try
        {
            using (FileStream inputFileStream = new FileStream(originalFilePath, FileMode.Open, FileAccess.Read))
            {
                using (FileStream outputFileStream = new FileStream(copyFilePath, FileMode.CreateNew))
                {
                    int readBytes = 0;
                    byte[] buffer = new byte[bufferSize];

                    while ((readBytes = inputFileStream.Read(buffer, 0, bufferSize)) != 0)
                    {
                        outputFileStream.Write(buffer, 0, readBytes);
                    }

                    //using (BinaryReader binaryReader = new BinaryReader(inputFileStream))
                    //{
                    //    using (BinaryWriter binaryWriter = new BinaryWriter(outputFileStream))
                    //    {
                    //        byte[] buffer = null;

                    //        while ((buffer = binaryReader.ReadBytes(bufferSize)).Length > 0)
                    //        {
                    //            binaryWriter.Write(buffer);
                    //        }
                    //    }
                    //}
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
        catch (IOException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}