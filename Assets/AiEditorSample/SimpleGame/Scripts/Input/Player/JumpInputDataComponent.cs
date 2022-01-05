using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{

    [DisallowMultipleComponent]
    public class JumpInputDataComponent : AbstractPressDownInputComponent, IJumpInputData
    {

        public float JumpForce => m_JumpForce;

        [SerializeField]
        private float m_JumpForce = 10;


    }

}
