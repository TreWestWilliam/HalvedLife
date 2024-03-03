using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RightHandManager))]
public class RightHandManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Label("Editor Help");
        RightHandManager m_hand = (RightHandManager)target;
        if (GUILayout.Button("Refresh Hand State")) 
        {
            m_hand.RefreshState();
        }
    }
}
