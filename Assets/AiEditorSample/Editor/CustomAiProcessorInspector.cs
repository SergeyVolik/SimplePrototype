//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(AiProcessorCustom))]
//[CanEditMultipleObjects]
//public class LookAtPointEditor : Editor
//{
//    SerializedProperty m_Asset;
//    SerializedProperty m_EventAction1;
//    SerializedProperty m_Action1;

//    SerializedProperty m_Health;
//    void OnEnable()
//    {
//        m_Asset = serializedObject.FindProperty("m_Asset");


//        m_EventAction1 = serializedObject.FindProperty("m_EventAction1");
//        m_Action1 = serializedObject.FindProperty("m_Action1");

//        m_Health = serializedObject.FindProperty("m_Health");

//    }

//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();

//        EditorGUILayout.PropertyField(m_Asset);

//        if (m_Asset.objectReferenceValue != null)
//        {
//            EditorGUILayout.PropertyField(m_EventAction1);
//            EditorGUILayout.PropertyField(m_Action1);
//            EditorGUILayout.PropertyField(m_Health);
//        }
//        else
//        {
//            EditorGUILayout.HelpBox("Error", MessageType.Error, true);
//        }
//        serializedObject.ApplyModifiedProperties();
//    }
//}