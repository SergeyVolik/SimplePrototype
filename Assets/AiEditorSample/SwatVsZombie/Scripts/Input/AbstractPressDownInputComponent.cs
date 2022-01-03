using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public abstract class AbstractPressDownInputComponent : MonoBehaviour
    {
        [SerializeField]
        protected KeyCode m_Key;
        [SerializeField]
        protected bool m_PressDown;

        public bool PressDown => m_PressDown;

      
        // Update is called once per frame
        protected virtual void Update()
        {
            m_PressDown = false;
            if (Input.GetKeyDown(m_Key))
            {
                m_PressDown = true;

            }
        }
    }
}
