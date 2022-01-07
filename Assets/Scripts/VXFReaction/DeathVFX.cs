using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(IDeathEffectEvent))]
    [DisallowMultipleComponent]
    public class DeathVFX : PlayEffectComponent<IDeathEffectEvent>
    {
        protected override void Play()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 10))
            {
                m_Channel.RaiseEvent(new Vector3(hitInfo.point.x, hitInfo.point.y + 0.05f, hitInfo.point.z));
            }
        }

    }

}

