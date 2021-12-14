//-----------------------------------------------------------------------
// This file is AUTO-GENERATED.
// Changes for this script by hand might be lost when auto-generation is run.
// (Generated date: 2021.12.14 16:56:25)
//-----------------------------------------------------------------------
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AiProcessorNew)) ]
[CanEditMultipleObjects]
public class  AiProcessorNewInspector  : Editor
{
    SerializedProperty m_Asset;
    SerializedProperty m_Action1;
    SerializedProperty m_EventAction1;
    SerializedProperty m_Action2;
    SerializedProperty m_EventAction2;
    SerializedProperty m_Health;
    SerializedProperty m_Ammo;
    SerializedProperty m_EnemyDistance;
	void OnEnable()
	{
		m_Asset = serializedObject.FindProperty("m_Asset");

		    m_Action1 = serializedObject.FindProperty("m_Action1");
		    m_EventAction1 = serializedObject.FindProperty("m_EventAction1");
		    m_Action2 = serializedObject.FindProperty("m_Action2");
		    m_EventAction2 = serializedObject.FindProperty("m_EventAction2");
		    m_Health = serializedObject.FindProperty("m_Health");
		    m_Ammo = serializedObject.FindProperty("m_Ammo");
		    m_EnemyDistance = serializedObject.FindProperty("m_EnemyDistance");
	}

	public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(m_Asset);

        if (m_Asset.objectReferenceValue != null)
        {
		        EditorGUILayout.PropertyField(m_Action1);
		        EditorGUILayout.PropertyField(m_EventAction1);
		        EditorGUILayout.PropertyField(m_Action2);
		        EditorGUILayout.PropertyField(m_EventAction2);
		        EditorGUILayout.PropertyField(m_Health);
		        EditorGUILayout.PropertyField(m_Ammo);
		        EditorGUILayout.PropertyField(m_EnemyDistance);
        }
        else
        {
            EditorGUILayout.HelpBox("Asset not setted!", MessageType.Error, true);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
	



