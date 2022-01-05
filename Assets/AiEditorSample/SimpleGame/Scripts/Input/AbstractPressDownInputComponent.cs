using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    public abstract class AbstractPressDownInputComponent : MonoBehaviour
    {
        [SerializeField]
        protected KeyCode m_Key;
        public UnityEvent PressDown => m_PressDown;
        [SerializeField]
        private UnityEvent m_PressDown;


        // Update is called once per frame
        protected virtual void Update()
        {
            if (Input.GetKeyDown(m_Key))
            {
                PressDown.Invoke();

            }
        }
    }
}
