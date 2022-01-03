using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{

    public interface IRimAIInpuData : IShootInpuData
    {
        void UpdateInput(bool value);
    }
    [DisallowMultipleComponent]
    public class RunInputDataComponent : MonoBehaviour, IRimAIInpuData
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
