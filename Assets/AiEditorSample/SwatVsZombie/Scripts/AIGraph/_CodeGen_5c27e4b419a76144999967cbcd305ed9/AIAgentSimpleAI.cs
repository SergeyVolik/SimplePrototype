//-----------------------------------------------------------------------
// This file is AUTO-GENERATED.
// Changes for this script by hand might be lost when auto-generation is run.
// (Generated date: 2022-01-03)
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class AIAgentSimpleAI : AIAgentBase<AISimulationSimpleAI, AISimulationSimpleAI.AgentInData, AISimulationSimpleAI.AgentOutData>
{

#if UNITY_EDITOR
    private bool Awaked = false;


    protected override void Awake()
    {
        base.Awake();

        Awaked = true;


    }

    private void OnValidate()
    {
        if (Application.isPlaying && Awaked)
        {
            m_Simulation.ChangeAgentData(Index, InData);
        }
    }
#endif

}

#if UNITY_EDITOR

[CustomEditor(typeof(AIAgentSimpleAI))]
[CanEditMultipleObjects]
public class  InspectorAIAgentSimpleAI : AIAgentBaseInpsector
{
    const string m_Warning0 = "Debug Mode. Inspector results fields will be updating in a play mode.";

	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.HelpBox(m_Warning0, MessageType.Info, true);

    }
}

#endif