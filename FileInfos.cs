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
    
    public static List<String> GetNewFiles(String srcDir, String destDir)
    {

        if (!DirPathExists(srcDir))
        {
            Debug.Log("src dir does not exist");
            return new List<string>();
        }
        if (!DirPathExists(destDir))
        {
            Debug.Log("dest dir does not exist");
            return new List<string>();
        }
        
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
