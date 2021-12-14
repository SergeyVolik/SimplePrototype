//-----------------------------------------------------------------------
// This file is AUTO-GENERATED.
// Changes for this script by hand might be lost when auto-generation is run.
// (Generated date: 2021.12.14 20:56:23)
//-----------------------------------------------------------------------

using UnityEngine;
using SerV112.UtilityAIRuntime;
using UnityEngine.Events;


[DisallowMultipleComponent]
public class  AIProcessor  : AIGraphProcessor
{
	private bool HasChanges = true;

    public Сheerfulness Сheerfulness => m_Сheerfulness;
    [SerializeField]
    private Сheerfulness m_Сheerfulness;
    [SerializeField]
    private UnityEvent<Сheerfulness> m_EventСheerfulness = new UnityEvent<Сheerfulness>();
    public UnityEvent<Сheerfulness> EventСheerfulness => m_EventСheerfulness;

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
            var  Сheerfulness1 = (Сheerfulness)Execute(0);
            if (Сheerfulness1 != m_Сheerfulness)
            {
                m_Сheerfulness = Сheerfulness1;
                EventСheerfulness.Invoke(m_Сheerfulness);
               
            }
             HasChanges = false;

        }
    }
}

	



