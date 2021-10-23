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
            fontSize = 12, 
            fontStyle = FontStyle.Bold,
            normal = new GUIStyleState() {textColor = new Color(0.3f, 0.55f, 0f)},
            margin = new RectOffset(10, 0, 0, 10),
            padding = new RectOffset(0,0,0,0)
        };
        yellowText = new GUIStyle()
        {
            fontSize = 12, fontStyle = FontStyle.Bold,
            normal = new GUIStyleState() {textColor = new Color(0.8f, 0.67f, 0f)},
            margin = new RectOffset(10, 0, 0, 10),
            padding = new RectOffset(0,0,0,0)
        };
    }
}
