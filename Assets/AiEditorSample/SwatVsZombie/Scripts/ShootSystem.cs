using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{

    [DisallowMultipleComponent]
    [RequireComponent(typeof(HandComponent))]
    [RequireComponent(typeof(IShootInpuData))]
    public class ShootSystem : MonoBehaviour
    {
        IShootInpuData IShootInpuData;
        HandComponent HandComponent;
        // Start is called before the first frame update
        void Start()
        {
            IShootInpuData = GetComponent<IShootInpuData>();
            HandComponent = GetComponent<HandComponent>();

        }

        // Update is called once per frame
        void Update()
        {
            if (IShootInpuData.NeedShoot && HandComponent.ActiveGun != null)
            {
                HandComponent.ActiveGun.Shoot();
            }
        }
    }
}