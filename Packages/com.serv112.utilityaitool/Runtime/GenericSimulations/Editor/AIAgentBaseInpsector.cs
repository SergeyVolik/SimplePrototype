#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public abstract class AIAgentBaseInpsector : Editor
{
    SerializedProperty Index;
    SerializedProperty InData;
    SerializedProperty OutData;


    void OnEnable()
    {
        Index = serializedObject.FindProperty("Index");
        InData = serializedObject.FindProperty("InData");
        OutData = serializedObject.FindProperty("OutData");


    }

    public override void OnInspectorGUI()
    {

        serializedObject.Update();
        GUI.enabled = false;
        if (Application.isPlaying)
        {
            EditorGUILayout.PropertyField(Index);
        }

        GUI.enabled = true;
        EditorGUILayout.PropertyField(InData);

        GUI.enabled = false;

        EditorGUILayout.PropertyField(OutData);

        GUI.enabled = true;

        serializedObject.ApplyModifiedProperties();
    }
}
#endif