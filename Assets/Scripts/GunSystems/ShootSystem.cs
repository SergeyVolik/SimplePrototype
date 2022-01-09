using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{

    [DisallowMultipleComponent]
    [RequireComponent(typeof(IShootInpuData))]
    [RequireComponent(typeof(IGunData))]
    public class ShootSystem : MonoBehaviour
    {
        IShootInpuData IShootInpuData;
        IGun Gun;


        public void Shoot()
        {
            Gun.DoAction();
        }

        // Start is called before the first frame update
        void Awake()
        {
            Gun = GetComponent<IGun>();
            IShootInpuData = GetComponent<IShootInpuData>();

        }

        private void OnEnable()
        {
            IShootInpuData.PressDown.AddListener(Shoot);
        }

        private void OnDisable()
        {
            IShootInpuData.PressDown.RemoveListener(Shoot);
        }


    }
}