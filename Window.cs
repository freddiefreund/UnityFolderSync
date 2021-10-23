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
    private string source = "";
    private string destination = "";
    private float timerInterval = 10f;
    private float timer;
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
        LoadConfig();
    }

    void LoadConfig()
    {
        string path = Application.dataPath + "/Scripts/UniteFileSync/config.json";
        try
        {
            var json = File.ReadAllText(path);
            SyncConfig config = JsonUtility.FromJson<SyncConfig>(json);
            destination = config.destDir;
            source = config.srcDir;
        }
        catch
        {
            Debug.Log("FILE NOT FOUND");
        }
    }

    void SaveConfig()
    {
        string path = Application.dataPath + "/Scripts/UniteFileSync/config.json";
        SyncConfig config = new SyncConfig();
        config.destDir = destination;
        config.srcDir = source;
        var json = JsonUtility.ToJson(config);
        File.WriteAllText(path,json);
    }
    
    private void OnGUI()
    {
        GUILayout.Label($"Enter the paths of the folders you want to sync", EditorStyles.boldLabel);

        Rect row1 = EditorGUILayout.BeginHorizontal();
        source = EditorGUILayout.TextField("Source path", source);
        if (GUILayout.Button("Browse"))
        {
            source = EditorUtility.OpenFolderPanel("Source folder", source, "");
        }
        EditorGUILayout.EndHorizontal();
        
        Rect row2 = EditorGUILayout.BeginHorizontal();
        destination = EditorGUILayout.TextField("Destination path", destination);
        if (GUILayout.Button("Browse"))
        {
            destination = EditorUtility.OpenFolderPanel("Destination folder", Application.dataPath, "");
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
            SaveConfig();
        }

        if (GUILayout.Button("LoadConfig"))
        {
            LoadConfig();
        }
        if (GUILayout.Button("Sync Folders"))
        {
            if(source == "" || destination == "")
                return;
            FileCopy.CopyIfNewer(source, destination);
            newFileNames.Clear();
        }
    }

    private void Update()
    {
        if (EditorApplication.timeSinceStartup > timer)
        {
            timer = (float)EditorApplication.timeSinceStartup + timerInterval;
            if(source == "" || destination == "")
                return;
            CheckForChanges();
            Debug.Log("Timer was triggered");
        }
    }

    private void CheckForChanges()
    {
        (newFileNames, modifiedFileNames) = FileInfos.GetNewFiles(source, destination);
    }
}
