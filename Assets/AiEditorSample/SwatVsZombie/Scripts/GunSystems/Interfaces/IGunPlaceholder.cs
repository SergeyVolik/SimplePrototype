

using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public interface IGunPlaceholder : IDropable, IProjectile
    {
        IGunData Data { get; }
        void SetPositionAndRot(Vector3 post, Quaternion rot);
    }
}
