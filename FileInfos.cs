using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileInfos
{
    public static bool DirPathExists(String path)
    {
        try
        {
            DirectoryInfo realPath = new DirectoryInfo(path);
            return realPath.Exists;
        }
        catch
        {
            return false;
        }
    }
    
    public static (List<String>, List<String>) GetNewFiles(String srcDir, String destDir)
    {
        if (!DirPathExists(srcDir))
        {
            Debug.LogError("Source Directory does not exist");
            return (new List<string>(), new List<string>());
        }
        if (!DirPathExists(destDir))
        {
            Debug.LogError("Destination Directory does not exist");
            return (new List<string>(), new List<string>());
        }
        
        List<String> newFiles = new List<string>();
        List<String> modifedFiles = new List<string>();
        string[] srcFiles;
        try
        {
            srcFiles = Directory.GetFiles(srcDir, "*", SearchOption.AllDirectories);
        }
        catch
        {
            Debug.LogError("Cannot list files");
            return (new List<string>(), new List<string>());
        }

        Array.ForEach(srcFiles, (srcFileLocation) =>
        {
            FileInfo srcFile = new FileInfo(srcFileLocation);
            FileInfo destFile = new FileInfo(srcFileLocation.Replace(srcDir, destDir));

            if (destFile.Exists)
            {
                if (srcFile.LastWriteTime > destFile.LastWriteTime)
                {
                    modifedFiles.Add(destFile.Name);
                }
            }
            else
            {
                newFiles.Add(destFile.Name);
            }
        });
        return (newFiles, modifedFiles);
    }
}
