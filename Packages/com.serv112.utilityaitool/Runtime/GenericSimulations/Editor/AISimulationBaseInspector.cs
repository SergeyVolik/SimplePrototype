#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CanEditMultipleObjects]
public class AISimulationBaseInspector : Editor
{
    private SerializedProperty m_SimulationShader;
    private SerializedProperty m_UpdateSimulation;
    private SerializedProperty m_CurrentSimulationSize;
    private SerializedProperty m_ExtentSizeIfOverflow;
    private SerializedProperty m_SimulationTime;
    private SerializedProperty m_DeltaTime;
    private SerializedProperty m_NumberOfSimulationAgents;
    private SerializedProperty m_AgentInDataSize;
    private SerializedProperty m_AgentOutDataSize;


    void OnEnable()
    {
        m_SimulationShader = serializedObject.FindProperty("m_SimulationShader");
        m_UpdateSimulation = serializedObject.FindProperty("m_UpdateSimulation");
        m_CurrentSimulationSize = serializedObject.FindProperty("m_CurrentSimulationSize");
        m_ExtentSizeIfOverflow = serializedObject.FindProperty("m_ExtentSizeIfOverflow");
        m_SimulationTime = serializedObject.FindProperty("m_SimulationTime");
        m_DeltaTime = serializedObject.FindProperty("m_DeltaTime");
        m_NumberOfSimulationAgents = serializedObject.FindProperty("m_NumberOfSimulationAgents");
        m_AgentInDataSize = serializedObject.FindProperty("m_AgentInDataSize");
        m_AgentOutDataSize = serializedObject.FindProperty("m_AgentOutDataSize");

    }

    public override void OnInspectorGUI()
    {

        serializedObject.Update();

        GUI.enabled = false;
        if (Application.isPlaying)
        {
            EditorGUILayout.PropertyField(m_SimulationShader);
        }

        EditorGUILayout.PropertyField(m_SimulationTime);
        EditorGUILayout.PropertyField(m_DeltaTime);
        EditorGUILayout.PropertyField(m_CurrentSimulationSize);
        EditorGUILayout.PropertyField(m_NumberOfSimulationAgents);
        EditorGUILayout.PropertyField(m_AgentInDataSize);
        EditorGUILayout.PropertyField(m_AgentOutDataSize);

        GUI.enabled = true;
        EditorGUILayout.PropertyField(m_UpdateSimulation);
        EditorGUILayout.PropertyField(m_ExtentSizeIfOverflow);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif