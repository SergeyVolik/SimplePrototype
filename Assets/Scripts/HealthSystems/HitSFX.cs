using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(IHitSoundEvent))]
    public class HitSFX : PlaySoundComponent<IHitSoundEvent> { }

}
