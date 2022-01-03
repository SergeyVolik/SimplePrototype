using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public class AbstractPressDownAndUpInputComponent : AbstractPressDownInputComponent
    {
        public bool PressUp => m_PressUp;

        [SerializeField]
        protected bool m_PressUp;

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            m_PressUp = false;
            if (Input.GetKeyUp(m_Key))
            {
                m_PressUp = true;

            }
        }
    }

}
