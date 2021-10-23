using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GuiStyles
{
    public static readonly GUIStyle greenText;
    public static readonly GUIStyle yellowText;
    
    static GuiStyles()
    {
        greenText = new GUIStyle()
        {
            fontSize = 12, fontStyle = FontStyle.Bold,
            normal = new GUIStyleState() {textColor = new Color(0.3f, 0.9f, 0f)}
        };
        yellowText = new GUIStyle()
        {
            fontSize = 12, fontStyle = FontStyle.Bold,
            normal = new GUIStyleState() {textColor = new Color(0.8f, 0.67f, 0f)}
        };
    }
}
