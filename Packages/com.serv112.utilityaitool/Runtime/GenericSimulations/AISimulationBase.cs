using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[DisallowMultipleComponent]
[DefaultExecutionOrder(-1)]
public abstract class AISimulationBase<SimT, InT, OutT> : MonoBehaviour where InT : struct where OutT : struct where SimT : MonoBehaviour
{
    [SerializeField]
    protected ComputeShader m_SimulationShader;

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
    private NativeArray<InT> m_AgentInArray;
    private int m_AgentInID;

    private ComputeBuffer m_AgentOutBuffer;
    private NativeArray<OutT> m_AgentOutArray;
    private int m_AgentOutID;

    private int m_KernelIndex;
    private uint m_ThreadGroupSize;

    [SerializeField]
    public float m_SimulationTime;
    [SerializeField]
    public float m_DeltaTime;

    private List<AIAgentBase<SimT, InT, OutT>> m_SimulationAgents = new List<AIAgentBase<SimT, InT, OutT>>();
    private List<AIAgentBase<SimT, InT, OutT>> m_SimulationAgentsToAdd = new List<AIAgentBase<SimT, InT, OutT>>();
    private List<AIAgentBase<SimT, InT, OutT>> m_SimulationAgentsToRemove = new List<AIAgentBase<SimT, InT, OutT>>();

    private bool m_NeedRecreateBuffers;
    private bool m_AgentsSizeUpdated;


    public static AISimulationBase<SimT, InT, OutT> Instance { get; private set; }

    private int m_SimulationTimeID;
    private int m_DeltaTimeID;

    [SerializeField]
    protected int m_AgentInDataSize;
    [SerializeField]
    protected int m_AgentOutDataSize;

    protected abstract ComputeShader FindComputeShader();
    virtual protected void Awake()
    {


        if (Instance)
        {
            Destroy(this);
            Debug.LogWarning("AISimulationSimpleAI is a singleton. A repeaded Component with a AISimulationSimpleAI has been deleted!");
            return;
        }

        Instance = this;

        m_SimulationTimeID = Shader.PropertyToID("SimulationTime");
        m_DeltaTimeID = Shader.PropertyToID("DeltaTime");
        m_AgentInID = Shader.PropertyToID("InAgentBuffer");
        m_AgentOutID = Shader.PropertyToID("OutAgentBuffer");

        

        m_SimulationShader = FindComputeShader();
        m_KernelIndex = m_SimulationShader.FindKernel("CSMain");
        m_SimulationShader.GetKernelThreadGroupSizes(m_KernelIndex, out m_ThreadGroupSize, out _, out _);

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
        if (m_AgentInArray.IsCreated)
            m_AgentInArray.Dispose();
        m_AgentOutBuffer?.Dispose();
        m_AgentInBuffer?.Dispose();
    }

    private void ResizeBuffers()
    {
        var ExtentSize = Mathf.CeilToInt(m_SimulationAgentsToAdd.Count / (float)m_ExtentSizeIfOverflow);
        m_CurrentSimulationSize += m_ExtentSizeIfOverflow * ExtentSize;

        m_AgentInBuffer?.Dispose();
        m_AgentInBuffer = new ComputeBuffer(m_CurrentSimulationSize, m_AgentInDataSize);
        if (m_AgentInArray.IsCreated)
            m_AgentInArray.Dispose();
        m_AgentInArray = new NativeArray<InT>(m_CurrentSimulationSize, Allocator.Persistent);
        m_SimulationShader.SetBuffer(m_KernelIndex, m_AgentInID, m_AgentInBuffer);

        m_AgentOutBuffer?.Dispose();
        m_AgentOutBuffer = new ComputeBuffer(m_CurrentSimulationSize, m_AgentOutDataSize);
        m_AgentOutArray = new NativeArray<OutT>(m_CurrentSimulationSize, Allocator.Persistent);
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

            m_AgentInArray[i] = m_SimulationAgents[i].GetInData();

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
            m_AgentInArray[m_SimulationAgents.Count - 1] = m_SimulationAgents[i].GetInData();
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
            m_AgentInArray[i] = m_SimulationAgents[i].GetInData();
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


    public OutT GetAgentOutData(int index)
    {
        return m_AgentOutArray[index];
    }

    public void AddAgent(AIAgentBase<SimT, InT, OutT> agent)
    {
        m_SimulationAgentsToAdd.Add(agent);

        if (m_SimulationAgentsToAdd.Count + m_SimulationAgents.Count > m_CurrentSimulationSize)
        {
            m_NeedRecreateBuffers = true;
        }


    }

    public void RemoveAgent(AIAgentBase<SimT, InT, OutT> agent)
    {

        m_SimulationAgentsToRemove.Add(agent);
    }



    public void ChangeAgentData(int index, InT value)
    {
        m_AgentInArray[index] = value;
    }
}