using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{

    [DisallowMultipleComponent]
    [RequireComponent(typeof(HandComponent))]
    public class ShootSystem : MonoBehaviour
    {
        HandComponent HandComponent;
        // Start is called before the first frame update
        void Start()
        {
            HandComponent = GetComponent<HandComponent>();

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && HandComponent.ActiveGun != null)
            {
                HandComponent.ActiveGun.Shoot();
            }
        }
    }
}