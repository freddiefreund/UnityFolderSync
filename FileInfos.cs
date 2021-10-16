using System;
using System.Collections.Generic;
using System.IO;

public class FileInfos
{
    public static List<String> GetNewFiles(String srcDir, String destDir)
    {
        List<String> newFiles = new List<string>();
        string[] srcFiles = Directory.GetFiles(srcDir, "*", SearchOption.AllDirectories);

        Array.ForEach(srcFiles, (srcFileLocation) =>
        {
            FileInfo srcFile = new FileInfo(srcFileLocation);
            FileInfo destFile = new FileInfo(srcFileLocation.Replace(srcDir, destDir));

            if (destFile.Exists)
            {
                if (srcFile.LastWriteTime > destFile.LastWriteTime)
                {
                    newFiles.Add(destFile.Name);
                }
            }
            else
            {
                newFiles.Add(destFile.Name);
            }
        });
        return newFiles;
    }
}
