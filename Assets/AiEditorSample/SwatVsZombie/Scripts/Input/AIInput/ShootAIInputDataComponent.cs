using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{

    public interface IShootAIInpuData : IShootInpuData
    {
        void UpdateInput(bool value);
    }
    [DisallowMultipleComponent]
    public class ShootAIInputDataComponent : MonoBehaviour, IShootAIInpuData
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
