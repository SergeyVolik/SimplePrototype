using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(MoveDataComponent))]
    public class RunSystem : MonoBehaviour
    {
        IMoveSettingsData data;
        // Start is called before the first frame update
        void Start()
        {
            data = GetComponent<MoveDataComponent>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                data.CurrentSpeed = data.RunSpeed;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                data.CurrentSpeed = data.MoveSpeed;
            }
        }
    }

}
