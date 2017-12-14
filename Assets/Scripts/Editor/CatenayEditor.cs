using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(RopeTest1))]
public class CatenayEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUI.changed)
        {
            RopeTest1 catenary = (RopeTest1)target;

            catenary.Regenerate();
        }
    }
}
