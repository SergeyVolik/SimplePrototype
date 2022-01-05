using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(IHitEffectEvent))]
    [DisallowMultipleComponent]
    public class HitVFX : PlayEffectComponent<IHitEffectEvent>
    {
        protected override void Play()
        {
            HitBloodParticlePool.Instance.PlayParticleAtPosition(transform.position);
        }

    }

}
