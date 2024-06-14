using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TriggerEnterRelay))]
public class TriggerEnterRelayEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Label("Manual Buttons");
        TriggerEnterRelay TER = (TriggerEnterRelay)target;
        if (GUILayout.Button("Any Entered"))
        {
            TER.AnyObjectEntered.Invoke();
        }
        if (GUILayout.Button("Target Entered"))
        {
            TER.OnCorrectObjectEntered.Invoke();
        }
        if (GUILayout.Button("Any Exit"))
        {
            TER.OnAnyObjectExit.Invoke();
        }
        if (GUILayout.Button("Target Exit"))
        {
            TER.OnCorrectObjectExit.Invoke();
        }
    }

}
