using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Window : EditorWindow
{
    private float timerInterval = 10f;
    private float timer;
    private SyncConfig config = new SyncConfig();
    private string configPath;
    private List<string> newFileNames = new List<string>();
    private List<string> modifiedFileNames = new List<string>();
    private bool srcPathWarning = false;
    private bool destPathWarning = false;
    
    [MenuItem("Window/UnityFolderSync")]
    public static void ShowWindow()
    {
        GetWindow<Window>("UnityFolderSync");
    }

    private void Awake()
    {
<<<<<<< Updated upstream
        SyncConfig.LoadConfig(Application.dataPath + "/Scripts/UnityFolderSync/config.json", out config);
=======
        configPath = Application.dataPath + "/Scripts/UnityFolderSync/config.json";
        SyncConfig.LoadConfig(configPath, out config);
        Debug.Log(Directory.GetCurrentDirectory());
>>>>>>> Stashed changes
    }

    private void OnGUI()
    {
        GUILayout.Label($"Enter the paths of the folders you want to sync", EditorStyles.boldLabel);

        Rect row1 = EditorGUILayout.BeginHorizontal();
        config.srcDir = EditorGUILayout.TextField("Source path", config.srcDir);
        if (GUILayout.Button("Browse"))
        {
            config.srcDir = EditorUtility.OpenFolderPanel("Source folder", config.srcDir, "");
        }
        EditorGUILayout.EndHorizontal();
        if (GUI.changed)
        {
            if (config.srcDir != "" && !FileInfos.DirPathExists(config.srcDir))
            {
                srcPathWarning = true;
            }
            else
            {
                srcPathWarning = false;
            }
        }
        if (srcPathWarning)
        {
            GUILayout.Label("Source folder path does not exist.", EditorStyles.boldLabel);
        }
            
        Rect row2 = EditorGUILayout.BeginHorizontal();
        config.destDir = EditorGUILayout.TextField("Destination path", config.destDir);
        if (GUILayout.Button("Browse"))
        {
            config.destDir = EditorUtility.OpenFolderPanel("Destination folder", Application.dataPath, "");
        }
        EditorGUILayout.EndHorizontal();
        if (GUI.changed)
        {
            if (config.destDir != "" && !FileInfos.DirPathExists(config.srcDir))
            {
                destPathWarning = true;
            }
            else
            {
                destPathWarning = false;
            }
        }
        if (destPathWarning)
        {
            GUILayout.Label("Destination folder path does not exist.", EditorStyles.boldLabel);
        }
        
        EditorGUILayout.Space();
        if (newFileNames.Count > 0)
        {
            GUILayout.Label("new files:", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical();
            foreach (var filename in newFileNames)
            {
                GUILayout.Label(filename, EditorStyles.label);    
            }
            EditorGUILayout.EndVertical();    
        }
        if (modifiedFileNames.Count > 0)
        {
            GUILayout.Label("modified files:", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical();
            foreach (var filename in modifiedFileNames)
            {
                GUILayout.Label(filename, EditorStyles.label);    
            }
            EditorGUILayout.EndVertical();    
        }
        else if (modifiedFileNames.Count == 0 && newFileNames.Count == 0)
        {
            GUILayout.Label("Everything up to date", EditorStyles.boldLabel);
        }

        if (GUILayout.Button("Save Config"))
        {
<<<<<<< Updated upstream
            config.SaveConfig(Application.dataPath + "/Scripts/UnityFolderSync/config.json", config);
=======
            config.SaveConfig(configPath, config);
>>>>>>> Stashed changes
        }

        if (GUILayout.Button("LoadConfig"))
        {
<<<<<<< Updated upstream
            SyncConfig.LoadConfig(Application.dataPath + "/Scripts/UnityFolderSync/config.json", out config);
=======
            SyncConfig.LoadConfig(configPath, out config);
>>>>>>> Stashed changes
        }
        if (GUILayout.Button("Sync Folders"))
        {
            if(config.srcDir == "" || config.destDir == "")
                return;
            FileCopy.CopyIfNewer(config.srcDir, config.destDir);
            AssetDatabase.Refresh();
            newFileNames.Clear();
        }
    }

    private void Update()
    {
        if (EditorApplication.timeSinceStartup > timer)
        {
            timer = (float)EditorApplication.timeSinceStartup + timerInterval;
            if (config.srcDir == "" || config.destDir == "")
                return;
            CheckForChanges();
        }
    }

    private void CheckForChanges()
    {
        (newFileNames, modifiedFileNames) = FileInfos.GetNewFiles(config.srcDir, config.destDir);
    }
}
