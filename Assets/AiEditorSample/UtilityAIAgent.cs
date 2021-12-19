using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAIAgent : MonoBehaviour
{

    public int Index;

    [SerializeField]
    [Range(0, 100)]
    private float Energy;

    private static UtilityAIAgent Simulation;
    public void ChangeEnergy(float energy) {
        m_Simulation.ChangeEnergy(Index, energy);
        Energy = energy;
    }
    public float GetEnergy()
    {
        return Energy;
    }

    UtilityAISimulation m_Simulation;
#if UNITY_EDITOR
    private bool Awaked = false;
#endif
    private void Awake()
    {
        if (Simulation == null)
            Simulation = this;

        m_Simulation = FindObjectOfType<UtilityAISimulation>();
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

    public Cheerfulness GetCheerfulness => (Cheerfulness)m_Simulation.GetResult(Index);


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying && Awaked)
        {
            m_Simulation.ChangeEnergy(Index, Energy);
        }
    }
#endif
}
