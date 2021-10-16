using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Window : EditorWindow
{
    private string source = "";
    private string destination = "";
    private float timerInterval = 10f;
    private float timer;
    private List<string> newFileNames;
    
    [MenuItem("Window/UnityFolderSync")]
    public static void ShowWindow()
    {
        GetWindow<Window>("UnityFolderSync");
        Debug.Log("ShowWindow called");
    }
    
    private void OnGUI()
    {
        GUILayout.Label($"{Time.time.ToString("N1")},{timer.ToString("N1")}", EditorStyles.boldLabel);

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

        EditorGUILayout.BeginVertical();
        /*foreach (var filename in newFileNames)
        {
            //GUILayout.Label(filename, EditorStyles.label);    
        }*/
        EditorGUILayout.EndVertical();
        
        if (GUILayout.Button("Sync Folders"))
        {
            FileCopy.CopyIfNewer(source, destination);    
        }
    }

    private void Update()
    {
        if (EditorApplication.timeSinceStartup > timer)
        {
            timer = (float)EditorApplication.timeSinceStartup + timerInterval;
            CheckForChanges();
            Debug.Log("Timer was triggered");
        }
    }

    private void CheckForChanges()
    {
        if(source == "" || destination == "")
            return;
        newFileNames = FileInfos.GetNewFiles(source, destination);
        if (newFileNames.Count == 0)
        {
            Debug.Log("No changes found...");
        }
        else
        {
            Debug.Log("Changes found:");
            foreach (string name in newFileNames)
            {
                Debug.Log(name);
            }
        }
    }
}
