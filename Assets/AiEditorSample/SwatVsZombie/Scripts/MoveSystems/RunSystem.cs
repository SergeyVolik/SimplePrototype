using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(MoveDataComponent))]
    [RequireComponent(typeof(IRunInputData))]
    public class RunSystem : MonoBehaviour
    {
        IMoveSettingsData data;
        IRunInputData m_Input;
        // Start is called before the first frame update
        void Start()
        {
            m_Input = GetComponent<IRunInputData>();
            data = GetComponent<MoveDataComponent>();
        }

        // Update is called once per frame
        void Update()
        {
            if (m_Input.PressDown)
            {
                data.CurrentSpeed = data.RunSpeed;
            }
            if (m_Input.PressUp)
            {
                data.CurrentSpeed = data.MoveSpeed;
            }
        }
    }

}
