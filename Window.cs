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
        destination = EditorGUILayout.TextField("destinationFilePath", destination);

        if (GUILayout.Button("Press Me pls"))
        {
            Debug.Log("Button pressed!");
        }
    }
}
