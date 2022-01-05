using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AIAgentBase<SimT, InT, OutT> : MonoBehaviour where InT : struct where OutT : struct where SimT : MonoBehaviour
{
    [SerializeField]
    public int Index;
    [Header("Params")]
    [SerializeField]
    protected InT InData;
    [Header("Results")]
    [SerializeField]
    protected OutT OutData;

    public InT GetInData() => InData;
    public OutT GetOutData() => OutData;

    protected AISimulationBase<SimT, InT, OutT> m_Simulation;




    protected virtual void Awake()
    {
        m_Simulation = AISimulationBase<SimT, InT, OutT>.Instance;

        if (!m_Simulation)
        {
            GameObject obj = new GameObject(nameof(AIAgentBase<SimT, InT, OutT>));
            m_Simulation = obj.AddComponent<AISimulationBase<SimT, InT, OutT>>();
        }


    }

    private void OnEnable()
    {
        m_Simulation.AddAgent(this);
    }

    private void OnDisable()
    {
        m_Simulation.RemoveAgent(this);
    }

    public void SetAgentOutDataInternal(OutT data)
    {
        OutData = data;
    }


    public void ChangeAgentData(InT value)
    {
        InData = value;
        m_Simulation.ChangeAgentData(Index, InData);

    }
}