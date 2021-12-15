//-----------------------------------------------------------------------
// This file is AUTO-GENERATED.
// Changes for this script by hand might be lost when auto-generation is run.
// (Generated date: 2021.12.15 10:23:57)
//-----------------------------------------------------------------------

using UnityEngine;
using SerV112.UtilityAIRuntime;
using UnityEngine.Events;


[DisallowMultipleComponent]
public class  AIProcessor  : AIGraphProcessor
{
	private bool HasChanges = true;

    public Cheerfulness Cheerfulness => m_Cheerfulness;
    [SerializeField]
    private Cheerfulness m_Cheerfulness;
    [SerializeField]
    private UnityEvent<Cheerfulness> m_EventCheerfulness = new UnityEvent<Cheerfulness>();
    public UnityEvent<Cheerfulness> EventCheerfulness => m_EventCheerfulness;

    private const string EnergyConst = "Energy";
    [SerializeField]
    [Range(0, 100)]
    private float m_Energy;
    public float Energy {
        set
        {
            SetProperty(EnergyConst, value);
            HasChanges = true;
            m_Energy = value;
        }
        get => GetProperty(EnergyConst);
    }

	bool Awaked = false;
    protected override void Awake()
    {
        base.Awake();

        Energy = m_Energy;

        Awaked = true;
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying && Awaked)
        {
            Energy = m_Energy;

        }
    }
#endif

    private void LateUpdate()
    {
        if (HasChanges)
        {
            var  Cheerfulness1 = (Cheerfulness)Execute(0);
            if (Cheerfulness1 != m_Cheerfulness)
            {
                m_Cheerfulness = Cheerfulness1;
                EventCheerfulness.Invoke(m_Cheerfulness);
               
            }
             HasChanges = false;

        }
    }
}

	



