using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Window : EditorWindow
{
    private string myString = "Hello Person!";
    [MenuItem("Window/example")]
    public static void ShowWindow()
    {
        GetWindow<Window>("Example");
    }
    private void OnGUI()
    {
        GUILayout.Label("This is a Label.", EditorStyles.boldLabel);

        myString = EditorGUILayout.TextField("Name", myString);

        if (GUILayout.Button("Press Me pls"))
        {
            Debug.Log("Button pressed!");
        }
    }
}
