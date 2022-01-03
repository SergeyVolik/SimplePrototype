using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public class AimInputDataComponent : AbstractPressDownInputComponent, IAimInputData
    {
        protected override void Update()
        {
            if (Input.GetKeyDown(m_Key))
            {
                m_PressDown = true;

            }
            else if (Input.GetKeyUp(m_Key))
            {
                m_PressDown = false;
            }
        }
    }

}
