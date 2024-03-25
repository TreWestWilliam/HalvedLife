using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ComparisonCameraSystem))]
public class ComparisonCameraSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ComparisonCameraSystem ThisObj = (ComparisonCameraSystem)target;
        if (GUILayout.Button("Capture Screenshots"))
        {
            ThisObj.EditorCapture();
        }
    }
}
