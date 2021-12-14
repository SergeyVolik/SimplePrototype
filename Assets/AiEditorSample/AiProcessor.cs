//using UnityEngine;
//using SerV112.UtilityAIRuntime;
//using UnityEngine.Events;

//[DisallowMultipleComponent]
//public class AiProcessor : AIGraphProcessor
//{
//    private bool HasChanges = true;
//    public Actions Action => m_Action;
//    [SerializeField]
//    private Actions m_Action;
//    [SerializeField]
//    private UnityEvent<Actions> m_ActionEvent = new UnityEvent<Actions>();
//    public UnityEvent<Actions> ActionEvent => m_ActionEvent;

//    private const string AmmoConst = "Ammo";
//    [SerializeField]
//    [Range(0, 20)]
//    private float m_Ammo;
//    public float Ammo {
//        set
//        {
//            SetProperty(AmmoConst, value);
//            HasChanges = true;
//            m_Ammo = value;
//        }
//        get => GetProperty(AmmoConst);
//    }
//    private const string HealthConst = "Health";
//    [Range(0, 100)]
//    [SerializeField]
//    private float m_Health;
//    public float Health
//    {
//        set
//        {
//            SetProperty(HealthConst, value);
//            m_Health = value;
//            HasChanges = true;
//        }
//        get => GetProperty(HealthConst);
//    }
//    private const string EnemyDistanceConst = "EnemyDistance";
//    [SerializeField]
//    [Range(0, 100)]
//    private float m_EnemyDistance;
//    public float EnemyDistance
//    {
//        set
//        {
//            SetProperty(EnemyDistanceConst, value);
//            m_EnemyDistance = value;
//            HasChanges = true;
//        }
//        get => GetProperty(EnemyDistanceConst);
//    }

//    bool Awaked = false;
//    protected override void Awake()
//    {
//        base.Awake();

//        Health = m_Health;
//        Ammo = m_Ammo;
//        EnemyDistance = m_EnemyDistance;
//        Awaked = true;
//    }


//#if UNITY_EDITOR
//    private void OnValidate()
//    {
//        if (Application.isPlaying && Awaked)
//        {
//            Health = m_Health;
//            Ammo = m_Ammo;
//            EnemyDistance = m_EnemyDistance;

//        }
//    }
//#endif

//    private void LateUpdate()
//    {
//        if (HasChanges)
//        {
//            var action1 = (Actions)Execute(0);
//            if (action1 != m_Action)
//            {
//                m_Action = action1;
//                ActionEvent.Invoke(m_Action);
               
//            }

//            HasChanges = false;

//        }
//    }
//}


