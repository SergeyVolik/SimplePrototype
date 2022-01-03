

using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public interface IGunPlaceholder : IDropable
    {
        void SetPositionAndRot(Vector3 post, Quaternion rot);
    }
}
