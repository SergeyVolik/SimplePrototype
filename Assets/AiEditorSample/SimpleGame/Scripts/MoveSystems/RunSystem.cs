using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(MoveData))]
    [RequireComponent(typeof(IRunInputData))]
    public class RunSystem : MonoBehaviour
    {
        IMoveSettingsData data;
        IRunInputData m_Input;
        // Start is called before the first frame update
        void Awake()
        {
            m_Input = GetComponent<IRunInputData>();
            data = GetComponent<MoveData>();
        }

        private void OnDisable()
        {
            m_Input.PressDown.RemoveListener(SetRunSpeed);
            m_Input.PressUp.RemoveListener(SetWalkSpeed);
        }

        private void OnEnable()
        {
            m_Input.PressDown.AddListener(SetRunSpeed);
            m_Input.PressUp.AddListener(SetWalkSpeed);
        }


        void SetRunSpeed()
        {
            data.CurrentSpeed = data.RunSpeed;
        }

        void SetWalkSpeed()
        {
            data.CurrentSpeed = data.MoveSpeed;
        }
        // Update is called once per frame

    }

}
