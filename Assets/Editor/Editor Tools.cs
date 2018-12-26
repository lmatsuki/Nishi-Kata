using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ClearScreenScript))]
public class EditorTools : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ClearScreenScript clearScreen = (ClearScreenScript)target;

        if (GUILayout.Button("Clear Screen"))
        {
            clearScreen.ClearScreen();
        }
    }
}
