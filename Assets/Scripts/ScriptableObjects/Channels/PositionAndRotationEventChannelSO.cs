using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game.Channels
{
    [CreateAssetMenu(menuName = "Events/Position And Rotation Event Channel")]
    public class PositionAndRotationEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<Vector3, Quaternion> OnPosAndRotEventRaised;
        public UnityAction<Vector3> OnPositionEventRaised;
        public void RaiseEvent(Vector3 pos, Quaternion rot)
        {
            if (OnPosAndRotEventRaised != null)
                OnPosAndRotEventRaised.Invoke(pos, rot);
        }

        public void RaiseEvent(Vector3 pos)
        {
            if (OnPositionEventRaised != null)
                OnPositionEventRaised.Invoke(pos);
        }
    }
}
