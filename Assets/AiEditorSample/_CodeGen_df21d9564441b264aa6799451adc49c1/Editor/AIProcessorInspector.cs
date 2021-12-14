//-----------------------------------------------------------------------
// This file is AUTO-GENERATED.
// Changes for this script by hand might be lost when auto-generation is run.
// (Generated date: 2021.12.14 20:56:24)
//-----------------------------------------------------------------------
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(AIProcessor)) ]
[CanEditMultipleObjects]
public class  AIProcessorInspector  : Editor
{
    const string k_TargetGUID = "c166e1d76a10edf40ba64ff68afc7f06";
    const string k_Error = "Please set up AI Graph asset";
    const string k_Error2 = "This script generated for specific AI Graph asset. Build guid: c166e1d76a10edf40ba64ff68afc7f06, asset path: AISample1.asset";
    SerializedProperty m_Asset;
    SerializedProperty m_Сheerfulness;
    SerializedProperty m_EventСheerfulness;
    SerializedProperty m_Energy;
	void OnEnable()
	{
		m_Asset = serializedObject.FindProperty("m_Asset");

		    m_Сheerfulness = serializedObject.FindProperty("m_Сheerfulness");
		    m_EventСheerfulness = serializedObject.FindProperty("m_EventСheerfulness");
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
		        EditorGUILayout.PropertyField(m_Сheerfulness);
		        EditorGUILayout.PropertyField(m_EventСheerfulness);
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
	



