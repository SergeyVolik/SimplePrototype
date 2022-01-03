using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{

    [DisallowMultipleComponent]
    [RequireComponent(typeof(IShootInpuData))]
    [RequireComponent(typeof(IGunData))]
    public class ShootSystem : MonoBehaviour, IShootEvent
    {
        IShootInpuData IShootInpuData;
        IGun Gun;
        IGunData Data;
        public UnityEvent OnShoot => m_OnShoot;

        [SerializeField]
        UnityEvent m_OnShoot;
        public void Shoot()
        {
            if (IShootInpuData.PressDown && Data.CurrentBullets > 0)
            {
                Gun.Shoot();
                OnShoot.Invoke();
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            Data = GetComponent<IGunData>();
            Gun = GetComponent<IGun>();
            IShootInpuData = GetComponent<IShootInpuData>();

        }

        // Update is called once per frame
        void Update()
        {
            Shoot();
        }
    }
}