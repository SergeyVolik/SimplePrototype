//-----------------------------------------------------------------------
// This file is AUTO-GENERATED.
// Changes for this script by hand might be lost when auto-generation is run.
// (Generated date: 2021.12.14 12:51:41)
//-----------------------------------------------------------------------

using UnityEngine;
using SerV112.UtilityAIRuntime;
using UnityEngine.Events;


[DisallowMultipleComponent]
public class  AiProcessorCustom  : AIGraphProcessor
{
	private bool HasChanges = true;

    public Actions Action1 => m_Action1;
    [SerializeField]
    private Actions m_Action1;
    [SerializeField]
    private UnityEvent<Actions> m_EventAction1 = new UnityEvent<Actions>();
    public UnityEvent<Actions> EventAction1 => m_EventAction1;
    public Actions1 Action2 => m_Action2;
    [SerializeField]
    private Actions1 m_Action2;
    [SerializeField]
    private UnityEvent<Actions1> m_EventAction2 = new UnityEvent<Actions1>();
    public UnityEvent<Actions1> EventAction2 => m_EventAction2;

    private const string HealthConst = "Health";
    [SerializeField]
    [Range(0, 100)]
    private float m_Health;
    public float Health {
        set
        {
            SetProperty(HealthConst, value);
            HasChanges = true;
            m_Health = value;
        }
        get => GetProperty(HealthConst);
    }
    private const string AmmoConst = "Ammo";
    [SerializeField]
    [Range(0, 100)]
    private float m_Ammo;
    public float Ammo {
        set
        {
            SetProperty(AmmoConst, value);
            HasChanges = true;
            m_Ammo = value;
        }
        get => GetProperty(AmmoConst);
    }
    private const string EnemyDistanceConst = "EnemyDistance";
    [SerializeField]
    [Range(0, 100)]
    private float m_EnemyDistance;
    public float EnemyDistance {
        set
        {
            SetProperty(EnemyDistanceConst, value);
            HasChanges = true;
            m_EnemyDistance = value;
        }
        get => GetProperty(EnemyDistanceConst);
    }

	bool Awaked = false;
    protected override void Awake()
    {
        base.Awake();

        Health = m_Health;
        Ammo = m_Ammo;
        EnemyDistance = m_EnemyDistance;

        Awaked = true;
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying && Awaked)
        {
            Health = m_Health;
            Ammo = m_Ammo;
            EnemyDistance = m_EnemyDistance;

        }
    }
#endif

    private void LateUpdate()
    {
        if (HasChanges)
        {
            var  Action11 = (Actions)Execute(0);
            if (Action11 != m_Action1)
            {
                m_Action1 = Action11;
                EventAction1.Invoke(m_Action1);
               
            }
             var  Action21 = (Actions1)Execute(1);
            if (Action21 != m_Action2)
            {
                m_Action2 = Action21;
                EventAction2.Invoke(m_Action2);
               
            }
             HasChanges = false;

        }
    }
}

	



