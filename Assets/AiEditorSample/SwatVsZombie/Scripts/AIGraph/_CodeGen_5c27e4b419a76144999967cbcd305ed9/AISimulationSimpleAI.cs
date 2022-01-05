//-----------------------------------------------------------------------
// This file is AUTO-GENERATED.
// Changes for this script by hand might be lost when auto-generation is run.
// (Generated date: 2022-01-04)
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;
using Unity.Collections;
using UnityEngine.Rendering;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AISimulationSimpleAI : AISimulationBase<AISimulationSimpleAI, AISimulationSimpleAI.AgentInData, AISimulationSimpleAI.AgentOutData>
{
    [Serializable]
    public struct AgentInData
    {
       
        [SerializeField]
        [Range(0, 7)]
        public float Helath;
        [SerializeField]
        [Range(0, 1)]
        public float SeeEnemy;
        [SerializeField]
        [Range(0, 7)]
        public float Ammo;
        [SerializeField]
        [Range(0, 1)]
        public float HasGun;
    }

    [Serializable]
    public struct AgentOutData
    {
        public SimpleAiActions SimpleAiActions;
        public float Age;
    }

    protected override void Awake()
    {
        base.Awake();

        unsafe
        {
            m_AgentInDataSize = sizeof(AgentInData);
            m_AgentOutDataSize = sizeof(AgentOutData);

        }

    }

    protected override ComputeShader FindComputeShader()
    {
        return Resources.Load<ComputeShader>("SimpleAI");;
    }

}

#if UNITY_EDITOR

[CustomEditor(typeof(AISimulationSimpleAI))]
[CanEditMultipleObjects]
public class  AISimulationSimpleAIInspector: AISimulationBaseInspector
{
    const string m_Warning0 = "Debug Mode. Inspector results fields will be updating in a play mode.";

	public override void OnInspectorGUI()
    {    
        base.OnInspectorGUI();
        EditorGUILayout.HelpBox(m_Warning0, MessageType.Info, true);
    }
}

#endif

