//-----------------------------------------------------------------------
// This file is AUTO-GENERATED.
// Changes for this script by hand might be lost when auto-generation is run.
// (Generated date: 2021.12.15 10:23:57)
//-----------------------------------------------------------------------
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(AIProcessor)) ]
[CanEditMultipleObjects]
public class  AIProcessorInspector  : Editor
{
    const string k_TargetGUID = "6c78a1f9afd6ccc40b9b0da381ef5b62";
    const string k_Error = "Please set up AI Graph asset";
    const string k_Error2 = "This script generated for specific AI Graph asset. Build guid: 6c78a1f9afd6ccc40b9b0da381ef5b62, asset path: Assets/AiEditorSample/AISample1.asset";
    SerializedProperty m_Asset;
    SerializedProperty m_Cheerfulness;
    SerializedProperty m_EventCheerfulness;
    SerializedProperty m_Energy;
	void OnEnable()
	{
		m_Asset = serializedObject.FindProperty("m_Asset");

		    m_Cheerfulness = serializedObject.FindProperty("m_Cheerfulness");
		    m_EventCheerfulness = serializedObject.FindProperty("m_EventCheerfulness");
		    m_Energy = serializedObject.FindProperty("m_Energy");
	}

	public override void OnInspectorGUI()
    {
        var t = (target as AIProcessor);

        serializedObject.Update();

        EditorGUILayout.PropertyField(m_Asset);

        if (m_Asset.objectReferenceValue != null)
        {
            if(t.CheckGuid(k_TargetGUID))
            {
		        EditorGUILayout.PropertyField(m_Cheerfulness);
		        EditorGUILayout.PropertyField(m_EventCheerfulness);
		        EditorGUILayout.PropertyField(m_Energy);
            }
            else{
                EditorGUILayout.HelpBox(k_Error2, MessageType.Error, true);
            }
        }
        else
        {
            EditorGUILayout.HelpBox(k_Error, MessageType.Error, true);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
	



