//-----------------------------------------------------------------------
// This file is AUTO-GENERATED.
// Changes for this script by hand might be lost when auto-generation is run.
// (Generated date: 2021-12-30)
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif



public class AIAgentHuman : MonoBehaviour
{
	[SerializeField]
    public int Index;
    [Header("Params")]
    [SerializeField]
    private AISimulationHuman.AgentInData InData;
    [Header("Results")]
    [SerializeField]
    private AISimulationHuman.AgentOutData OutData;

    public AISimulationHuman.AgentInData GetAgentData() => InData;


    AISimulationHuman m_Simulation;

#if UNITY_EDITOR
    private bool Awaked = false;
#endif


    private void Awake()
    {
        m_Simulation = AISimulationHuman.Instance;

        if (!m_Simulation)
        {
            GameObject obj = new GameObject(nameof(AISimulationHuman));
            m_Simulation = obj.AddComponent<AISimulationHuman>();
        }

#if UNITY_EDITOR
        Awaked = true;
#endif

    }

    private void OnEnable()
    {
        m_Simulation.AddAgent(this);
    }

    private void OnDisable()
    {
        m_Simulation.RemoveAgent(this);
    }
#if UNITY_EDITOR
    public void SetAgentOutDataInternal(AISimulationHuman.AgentOutData data)
    {
        OutData = data;
    }
#endif

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying && Awaked)
        {
            m_Simulation.ChangeAgentData(Index, InData);
        }
    }
#endif

    public void ChangeAgentData(AISimulationHuman.AgentInData value) 
    {
        InData = value;
        m_Simulation.ChangeAgentData(Index, InData);
        
    }

}

#if UNITY_EDITOR

[CustomEditor(typeof(AIAgentHuman))]
[CanEditMultipleObjects]
public class  InspectorAIAgentHuman : Editor
{
    const string m_Warning0 = "Debug Mode. Inspector results fields will be updating in a play mode.";
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
        if(Application.isPlaying)
        {          
            EditorGUILayout.PropertyField(Index);  
        }

        GUI.enabled = true;
        EditorGUILayout.PropertyField(InData);

        GUI.enabled = false;

        EditorGUILayout.PropertyField(OutData);

        GUI.enabled = true;
        EditorGUILayout.HelpBox(m_Warning0, MessageType.Info, true);
        serializedObject.ApplyModifiedProperties();
    }
}

#endif

