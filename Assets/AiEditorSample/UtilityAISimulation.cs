using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UtilityAISimulation : MonoBehaviour
{
    [SerializeField]
    private ComputeShader m_Shader;

    [SerializeField]
    public bool m_Update;
    private ComputeBuffer m_ResultBuffer1;
    private int[] m_ResultList1;

    private ComputeBuffer m_InputBuffer1;
    private float[] m_InputList1;

    [SerializeField]
    int m_InitialSize = 100;
    [SerializeField]
    int m_ExtentSizeIfOverflow = 100;
    int m_KernelIndex;
    uint m_threadGroupSize;
    uint _;

   
    void Awake()
    {
        m_KernelIndex = m_Shader.FindKernel("CSMain");

        m_Shader.GetKernelThreadGroupSizes(m_KernelIndex, out m_threadGroupSize, out _, out _);
        
    }
    StringBuilder m_Builder = new StringBuilder();


    void Update()
    {
       


        if (Agents.Count > 0 && m_Update)
        {
            m_Shader.SetBool("UpdateData", m_Update);

            m_InputBuffer1.SetData(m_InputList1);
            m_Shader.SetBuffer(m_KernelIndex, "Data", m_InputBuffer1);
            var threadGroups = (int)(m_InitialSize / m_threadGroupSize);
            if (threadGroups == 0)
                threadGroups = 1;
            m_Shader.Dispatch(m_KernelIndex, threadGroups, 1, 1);


            m_ResultBuffer1.GetData(m_ResultList1);


        }


    }

    private void LateUpdate()
    {
        if (NeedRecreateBuffers)
        {
            var ExtentSize = AgentsToAdd.Count / m_ExtentSizeIfOverflow;
            m_InitialSize += m_ExtentSizeIfOverflow * ExtentSize == 0 ? 1 : ExtentSize;

            if (m_InputBuffer1 != null)
            {

                m_InputBuffer1.Dispose();
                m_ResultBuffer1.Dispose();
            }

            m_InputBuffer1 = new ComputeBuffer(m_InitialSize, sizeof(float));
            m_ResultBuffer1 = new ComputeBuffer(m_InitialSize, sizeof(int));


            m_ResultList1 = new int[m_InitialSize];
            m_InputList1 = new float[m_InitialSize];

            m_Shader.SetBuffer(m_KernelIndex, "Result", m_ResultBuffer1);
            m_Shader.SetBuffer(m_KernelIndex, "Data", m_InputBuffer1);
        }

        if (AgentsToRemove.Count > 0)
        {
            int minIndex = int.MaxValue;
            for (int i = 0; i < AgentsToRemove.Count; i++)
            {
                Agents.Remove(AgentsToRemove[i]);
                var index = AgentsToRemove[i].Index;
                if (minIndex > index)
                    minIndex = index;


            }

            AgentsToRemove.Clear();
            for (int i = minIndex; i < Agents.Count; i++)
            {
                Agents[i].Index = i;
                m_InputList1[i] = Agents[i].GetEnergy();
            }


            AgentsSizeUpdated = true;
        }

        if (AgentsToAdd.Count > 0)
        {
            for (int i = 0; i < AgentsToAdd.Count; i++)
            {
                Agents.Add(AgentsToAdd[i]);
                AgentsToAdd[i].Index = Agents.Count - 1;
                m_InputList1[AgentsToAdd.Count - 1] = AgentsToAdd[i].GetEnergy();
            }

            AgentsToAdd.Clear();

            AgentsSizeUpdated = true;
        }

        if (NeedRecreateBuffers)
        {
            NeedRecreateBuffers = false;

            for (int i = 0; i < Agents.Count; i++)
            {
                m_InputList1[i] = Agents[i].GetEnergy();
            }

            Debug.Log($"NeedRecreateBuffers: {m_InitialSize}");
            m_InputBuffer1.SetData(m_InputList1);
        }



        if (AgentsSizeUpdated)
        {
            Debug.Log($"AgentsSizeUpdated: {Agents.Count}");
            AgentsSizeUpdated = false;
        }

        m_Builder.Clear();


        for (int i = 0; i < m_ResultList1.Length; i++)
        {
            m_Builder.Append(m_ResultList1[i]);
            m_Builder.Append(" ");

        }
        Debug.Log(m_Builder.ToString());
    }

    bool NeedRecreateBuffers;
    bool AgentsSizeUpdated;

    private void OnDestroy()
    {
        m_InputBuffer1.Dispose();
        m_ResultBuffer1.Dispose();
    }

    List<UtilityAIAgent> Agents = new List<UtilityAIAgent>();
    List<UtilityAIAgent> AgentsToAdd = new List<UtilityAIAgent>();
    List<UtilityAIAgent> AgentsToRemove = new List<UtilityAIAgent>();
    
    public void AddAgent(UtilityAIAgent agent)
    {
        AgentsToAdd.Add(agent);

        if (AgentsToAdd.Count + Agents.Count > m_InitialSize)
        {
            NeedRecreateBuffers = true;
        }
    }

    public void RemoveAgent(UtilityAIAgent agent)
    {
        AgentsToRemove.Add(agent);
    }

    public void ChangeEnergy(int index, float value)
    {
        m_InputList1[index] = value;
    }

    public int GetResult(int index)
    {
        return m_ResultList1[index];
    }
}
