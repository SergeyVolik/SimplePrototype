using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{

    public interface IThrowItemAIInpuData : IThrowInput
    {
    }

    [DisallowMultipleComponent]
    public class ThrowItemAIInputDataComponent : MonoBehaviour, IThrowItemAIInpuData
    {

        public UnityEvent PressDown => m_PressDown;

        [SerializeField]
        private UnityEvent m_PressDown;
    }

}
