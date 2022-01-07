using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game.Channels
{
    [CreateAssetMenu(menuName = "Events/Vector3 Event Channel")]
    public class Vector3EventChannelSO : DescriptionBaseSO
    {
        public UnityAction<Vector3> OnEventRaised;

        public void RaiseEvent(Vector3 pos)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(pos);
        }
    }
}
