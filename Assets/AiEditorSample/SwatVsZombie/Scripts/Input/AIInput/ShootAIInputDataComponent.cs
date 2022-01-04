using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{

    public interface IShootAIInpuData : IShootInpuData
    {

    }



    [DisallowMultipleComponent]
    public class ShootAIInputDataComponent : AbstractAIInput, IShootAIInpuData
    {
        public bool PressDown => m_Value;

        public void UpdateInput(bool value)
        {
            m_Value = value;
        }
        [SerializeField]
        private bool m_Value;
    }

}
