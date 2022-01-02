using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    public class ShootInpuDataComponent : MonoBehaviour, IShootInpuData
{
        public bool NeedShoot => m_NeedShoot;

        [SerializeField]
        private bool m_NeedShoot;

        [SerializeField]
        private KeyCode Key = KeyCode.Mouse0;
        // Update is called once per frame
        void Update()
        {
            
            if (Input.GetKeyDown(Key))
                m_NeedShoot = true;
        }

        void LateUpdate()
        {
            m_NeedShoot = false;
        }


    }

}
