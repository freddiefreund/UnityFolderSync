using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Window : EditorWindow
{
    private string myString = "Hello Person!";
    private string source = "enter src path here...";
    private string destination = "enter dest path here...";
    private float timerInterval = 10f;
    private float timer;
    
    [MenuItem("Window/example")]
    public static void ShowWindow()
    {
        GetWindow<Window>("Example");
        
    }

    private void OnGUI()
    {
        GUILayout.Label("This is a Label.", EditorStyles.boldLabel);

        myString = EditorGUILayout.TextField("Name", myString);
        source = EditorGUILayout.TextField("sourceFilePath", source);
        if (GUILayout.Button("Browse"))
        {
            source = EditorUtility.OpenFolderPanel("Source folder", "", "");
        }
        destination = EditorGUILayout.TextField("destinationFilePath", destination);
        if (GUILayout.Button("Browse"))
        {
            destination = EditorUtility.OpenFolderPanel("Destination folder", "", "");
        }

        if (EditorApplication.timeSinceStartup > timer)
        {
            timer += timerInterval;
            Debug.Log("10 Seconds passed");
            // Call Bastis function
        }
        
        if (GUILayout.Button("Press Me pls"))
        {
            Debug.Log("Button pressed!");
            myString = $"{EditorApplication.timeSinceStartup.ToString("N1")} Timer:{timer.ToString("N1")}";
        }
    }
}
