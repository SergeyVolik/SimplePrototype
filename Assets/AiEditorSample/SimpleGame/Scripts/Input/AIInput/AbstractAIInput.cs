using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    public abstract class AbstractAIInput : MonoBehaviour
    {
        public UnityEvent PressDown => m_PressDown;

        [SerializeField]
        private UnityEvent m_PressDown;
    }

    public abstract class AbstractAIInputStartEnd : AbstractAIInput
    {
        public UnityEvent PressUp => m_PressUp;

        [SerializeField]
        private UnityEvent m_PressUp;
    }
}
