//-----------------------------------------------------------------------
// This file is AUTO-GENERATED.
// Changes for this script by hand might be lost when auto-generation is run.
// (Generated date: 2022-01-03)
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



[DisallowMultipleComponent]
[DefaultExecutionOrder(-1)]
public class AISimulationSimpleAI : MonoBehaviour
{
    [Serializable]
    public struct AgentInData
    {
       
        [SerializeField]
        [Range(0, 100)]
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
    [SerializeField]
    private ComputeShader m_SimulationShader;

    [SerializeField]
    public bool m_UpdateSimulation = true;

    [SerializeField]
    private int m_CurrentSimulationSize;

    public int CurrentSimulationSize => m_CurrentSimulationSize;

    [SerializeField]
    private int m_NumberOfSimulationAgents;

    public int NumberOfSimulationAgents => m_SimulationAgents.Count;

    System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
    private string m_LastGPUAsycReadback;
    public string LastGPUAsycReadback => m_LastGPUAsycReadback;


    [SerializeField]
    private int m_ExtentSizeIfOverflow = 1024;
    //Results
    private ComputeBuffer m_AgentInBuffer;
    private NativeArray<AgentInData> m_AgentInArray;
    private int m_AgentInID;

    private ComputeBuffer m_AgentOutBuffer;
    private NativeArray<AgentOutData> m_AgentOutArray;
    private int m_AgentOutID;

    private int m_KernelIndex;
    private uint m_ThreadGroupSize;

    [SerializeField]
    public float m_SimulationTime;
    [SerializeField]
    public float m_DeltaTime;

    private List<AIAgentSimpleAI> m_SimulationAgents = new List<AIAgentSimpleAI>();
    private List<AIAgentSimpleAI> m_SimulationAgentsToAdd = new List<AIAgentSimpleAI>();
    private List<AIAgentSimpleAI> m_SimulationAgentsToRemove = new List<AIAgentSimpleAI>();

    private bool m_NeedRecreateBuffers;
    private bool m_AgentsSizeUpdated;


    public static AISimulationSimpleAI Instance { get; private set; }

    private int m_SimulationTimeID;
    private int m_DeltaTimeID;
    
    [SerializeField]
    private int m_AgentInDataSize;
    [SerializeField]
    private int m_AgentOutDataSize;

    private void Awake()
    {
        

        if (Instance)
        {
            Destroy(this);
            Debug.LogWarning("AISimulationSimpleAI is a singleton. A repeaded Component with a AISimulationSimpleAI has been deleted!");
            return;
        }

        unsafe
        {
            m_AgentInDataSize = sizeof(AgentInData);
            m_AgentOutDataSize = sizeof(AgentOutData);

        }

        m_SimulationTimeID = Shader.PropertyToID("SimulationTime");
        m_DeltaTimeID = Shader.PropertyToID("DeltaTime");
        m_AgentInID = Shader.PropertyToID("InAgentBuffer");
        m_AgentOutID = Shader.PropertyToID("OutAgentBuffer");

        Instance = this;

        m_SimulationShader = Resources.Load<ComputeShader>("SimpleAI");
        m_KernelIndex = m_SimulationShader.FindKernel("CSMain");
        m_SimulationShader.GetKernelThreadGroupSizes(m_KernelIndex, out m_ThreadGroupSize, out _, out _);
        
    }
   


    private void ResizeBuffers()
    {
        var ExtentSize = Mathf.CeilToInt(m_SimulationAgentsToAdd.Count / (float)m_ExtentSizeIfOverflow);
        m_CurrentSimulationSize += m_ExtentSizeIfOverflow * ExtentSize;

        m_AgentInBuffer?.Dispose();
        m_AgentInBuffer = new ComputeBuffer(m_CurrentSimulationSize, m_AgentInDataSize);
        if (m_AgentInArray.IsCreated)
            m_AgentInArray.Dispose();
        m_AgentInArray = new NativeArray<AgentInData>(m_CurrentSimulationSize, Allocator.Persistent);
        m_SimulationShader.SetBuffer(m_KernelIndex, m_AgentInID, m_AgentInBuffer);

        m_AgentOutBuffer?.Dispose();
        m_AgentOutBuffer = new ComputeBuffer(m_CurrentSimulationSize, m_AgentOutDataSize);
        m_AgentOutArray = new NativeArray<AgentOutData>(m_CurrentSimulationSize, Allocator.Persistent);
        m_AgentOutBuffer.SetData(m_AgentOutArray);
        m_SimulationShader.SetBuffer(m_KernelIndex, m_AgentOutID, m_AgentOutBuffer);

      
    }

    private void RemoveAgentsAndUpdateBuffers()
    {
        int minIndex = int.MaxValue;
        for (int i = 0; i < m_SimulationAgentsToRemove.Count; i++)
        {
            m_SimulationAgents.Remove(m_SimulationAgentsToRemove[i]);
            var index = m_SimulationAgentsToRemove[i].Index;
            if (minIndex > index)
                minIndex = index;


        }

        m_SimulationAgentsToRemove.Clear();
        for (int i = minIndex; i < m_SimulationAgents.Count; i++)
        {
            m_SimulationAgents[i].Index = i;

            m_AgentInArray[i] = m_SimulationAgents[i].GetAgentData();
          
        }


        m_AgentsSizeUpdated = true;

        m_NumberOfSimulationAgents = m_SimulationAgents.Count;
    }

    private void AddAgentsAndUpdateBuffers()
    {
        m_SimulationAgentsToAdd.Reverse();

        for (int i = 0; i < m_SimulationAgentsToAdd.Count; i++)
        {
            m_SimulationAgents.Add(m_SimulationAgentsToAdd[i]);
            m_SimulationAgentsToAdd[i].Index = m_SimulationAgents.Count - 1;
            m_AgentInArray[m_SimulationAgents.Count - 1] = m_SimulationAgents[i].GetAgentData();
        }

        m_SimulationAgentsToAdd.Clear();

        m_AgentsSizeUpdated = true;
        m_NumberOfSimulationAgents = m_SimulationAgents.Count;
    }

    private void UpdateBuffers()
    {
        m_NeedRecreateBuffers = false;

        for (int i = 0; i < m_SimulationAgents.Count; i++)
        {
            m_AgentInArray[i] = m_SimulationAgents[i].GetAgentData();
        }

        Debug.Log($"NeedRecreateBuffers: {m_CurrentSimulationSize}");

        m_AgentInBuffer.SetData(m_AgentInArray);
       

    }

    bool requestDone = true;

    private void StartRequest()
    {
        if (requestDone)
        {
            requestDone = false;
            AsyncGPUReadback.RequestIntoNativeArray(ref m_AgentOutArray, m_AgentOutBuffer, m_SimulationAgents.Count * m_AgentOutDataSize, 0, AsyncRead);
           
        }
    }
    private void AsyncRead(AsyncGPUReadbackRequest req)
    {
        if (req.done)
        {

            requestDone = true;
            stopWatch.Stop();
            System.TimeSpan ts = stopWatch.Elapsed;
            m_LastGPUAsycReadback = ts.TotalMilliseconds.ToString() + "ms";

            stopWatch.Restart();
            stopWatch.Start();
            for (int i = 0; i < m_SimulationAgents.Count; i++)
            {
           
                m_SimulationAgents[i].SetAgentOutDataInternal(m_AgentOutArray[i]);
            }
        }

        if (req.hasError)
        {
            Debug.LogError("AISimulationSimpleAI: AsyncRead from gpu has an error");
        }
    }

    private void Dispatch()
    {
        m_AgentInBuffer.SetData(m_AgentInArray, 0, 0, m_SimulationAgents.Count);
        m_SimulationShader.SetFloat(m_SimulationTimeID, m_SimulationTime);
         m_SimulationShader.SetFloat(m_DeltaTimeID, m_DeltaTime);
        var threadGroups = Mathf.CeilToInt((m_SimulationAgents.Count / (float)m_ThreadGroupSize));
        if (threadGroups == 0)
            threadGroups = 1;

        m_SimulationShader.Dispatch(m_KernelIndex, threadGroups, 1, 1);

        StartRequest();
        
        m_SimulationTime += Time.deltaTime;
        m_DeltaTime = Time.deltaTime;

    }

    public void UpdateSimulation()
    {
        if (m_NeedRecreateBuffers)
        {
            ResizeBuffers();
        }

        if (m_SimulationAgentsToRemove.Count > 0)
        {
            RemoveAgentsAndUpdateBuffers();

        }

        if (m_SimulationAgentsToAdd.Count > 0)
        {
            AddAgentsAndUpdateBuffers();

        }

        if (m_NeedRecreateBuffers)
        {
            UpdateBuffers();

        }

        if (m_SimulationAgents.Count > 0)
        {
            Dispatch();
        }
    }

    private void Update()
    {
        if (m_UpdateSimulation)
        {
            UpdateSimulation();
        }

     
    }


    private void OnDestroy()
    {
        if(m_AgentInArray.IsCreated)
            m_AgentInArray.Dispose();
        m_AgentOutBuffer?.Dispose();
        m_AgentInBuffer?.Dispose();
    }
    
    public void AddAgent(AIAgentSimpleAI agent)
    {
        m_SimulationAgentsToAdd.Add(agent);

        if (m_SimulationAgentsToAdd.Count + m_SimulationAgents.Count > m_CurrentSimulationSize)
        {
            m_NeedRecreateBuffers = true;
        }


    }

    public void RemoveAgent(AIAgentSimpleAI agent)
    {

        m_SimulationAgentsToRemove.Add(agent);
    }


    public void ChangeAgentData(int index, AgentInData value)
    {
        m_AgentInArray[index] = value;
    }  
   
}

#if UNITY_EDITOR

[CustomEditor(typeof(AISimulationSimpleAI))]
[CanEditMultipleObjects]
public class  InspectorAISimulationSimpleAI : Editor
{
    const string m_Warning0 = "Debug Mode. Inspector results fields will be updating in a play mode.";
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
        if(Application.isPlaying)
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

        EditorGUILayout.HelpBox(m_Warning0, MessageType.Info, true);

        serializedObject.ApplyModifiedProperties();
    }
}

#endif

