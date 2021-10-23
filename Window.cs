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
    private List<string> newFileNames = new List<string>();
    private List<string> modifiedFileNames = new List<string>();
    
    [MenuItem("Window/UnityFolderSync")]
    public static void ShowWindow()
    {
        GetWindow<Window>("UnityFolderSync");
        Debug.Log("ShowWindow called");
    }

    private void Awake()
    {
        SyncConfig.LoadConfig(Application.dataPath + "/Scripts/UniteFileSync/config.json", out config);
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
        
        Rect row2 = EditorGUILayout.BeginHorizontal();
        config.destDir = EditorGUILayout.TextField("Destination path", config.destDir);
        if (GUILayout.Button("Browse"))
        {
            config.destDir = EditorUtility.OpenFolderPanel("Destination folder", Application.dataPath, "");
        }
        EditorGUILayout.EndHorizontal();

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
            config.SaveConfig(Application.dataPath + "/Scripts/UniteFileSync/config.json", config);
        }

        if (GUILayout.Button("LoadConfig"))
        {
            SyncConfig.LoadConfig(Application.dataPath + "/Scripts/UniteFileSync/config.json", out config);
        }
        if (GUILayout.Button("Sync Folders"))
        {
            if(config.srcDir == "" || config.destDir == "")
                return;
            FileCopy.CopyIfNewer(config.srcDir, config.destDir);
            newFileNames.Clear();
        }
    }

    private void Update()
    {
        if (EditorApplication.timeSinceStartup > timer)
        {
            timer = (float)EditorApplication.timeSinceStartup + timerInterval;
            if(config.srcDir == "" || config.destDir == "")
                return;
            CheckForChanges();
            Debug.Log("Timer was triggered");
        }
    }

    private void CheckForChanges()
    {
        (newFileNames, modifiedFileNames) = FileInfos.GetNewFiles(config.srcDir, config.destDir);
    }
}
