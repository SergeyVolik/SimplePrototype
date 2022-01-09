using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game.Channels
{
    [CreateAssetMenu(menuName = "Events/RotateObjectRegister")]
    public class RotateObjectRegisterEvent : DescriptionBaseSO
    {
        public UnityAction<Transform, MoveData, IMoveInputData> OnEventRaised;

        public void RaiseEvent(Transform t, MoveData m, IMoveInputData input)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(t, m, input);
        }
    }
}
