using System;
using System.IO;

public class FileCopy
{
    public static void CopyIfNewer(String srcDir, String destDir)
    {
        string[] srcFiles = Directory.GetFiles(srcDir, "*", SearchOption.AllDirectories);

        Array.ForEach(srcFiles, (srcFileLocation) =>
        {
            FileInfo originalFile = new FileInfo(srcFileLocation);
            FileInfo destFile = new FileInfo(srcFileLocation.Replace(srcDir, destDir));

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