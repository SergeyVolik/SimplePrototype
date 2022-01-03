

using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public interface IGunPlaceholder : IDropable
    {
        IGunData Data { get; }
        void SetPositionAndRot(Vector3 post, Quaternion rot);
    }
}
