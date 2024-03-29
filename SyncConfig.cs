﻿using System;
using System.IO;
using UnityEngine;

[Serializable]
public class SyncConfig 
{
    public string srcDir;
    public string destDir;

    public static void LoadConfig(string configPath, out SyncConfig config)
    {
        try
        {
            var json = File.ReadAllText(configPath);
            config = JsonUtility.FromJson<SyncConfig>(json);
        }
        catch
        {
            config = new SyncConfig();
            config.srcDir = "";
            config.destDir = "";
        }
    }
    
    public void SaveConfig(string configPath, SyncConfig config)
    {
        var json = JsonUtility.ToJson(config);
        File.WriteAllText(configPath,json);
    }
}
