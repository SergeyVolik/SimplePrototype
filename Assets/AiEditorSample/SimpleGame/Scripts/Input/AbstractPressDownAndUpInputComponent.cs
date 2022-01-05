using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    public class AbstractPressDownAndUpInputComponent : AbstractPressDownInputComponent
    {
        public UnityEvent PressUp => m_PressUp;
        [SerializeField]
        private UnityEvent m_PressUp;

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            if (Input.GetKeyUp(m_Key))
            {
                PressUp.Invoke();

            }
        }
    }

}
