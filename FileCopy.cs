using System;
using System.IO;

public class FileCopy
{
    public static void CopyIfNewer(String srcDir, String destDir)
    {
        string[] originalFiles = Directory.GetFiles(srcDir, "*", SearchOption.AllDirectories);

        Array.ForEach(originalFiles, (originalFileLocation) =>
        {
            FileInfo originalFile = new FileInfo(originalFileLocation);
            FileInfo destFile = new FileInfo(originalFileLocation.Replace(srcDir, destDir));

            if (destFile.Exists)
            {
                if (originalFile.LastWriteTime > destFile.LastWriteTime)
                {
                    originalFile.CopyTo(destFile.FullName, true);
                }
            }
            else
            {
                Directory.CreateDirectory(destFile.DirectoryName);
                originalFile.CopyTo(destFile.FullName, false);
            }
        });
    }
}