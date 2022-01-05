using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(IDeathEffectEvent))]
    public class DeathVFX : PlayEffectComponent<IDeathEffectEvent>
    {
        protected override void Play()
        {
            HitBloodParticlePool.Instance.PlayParticleAtPosition(transform.position);
        }

    }

}

