using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(IDeathSoundEvent))]
    public class DeathSFX : PlaySoundComponent<IDeathSoundEvent> { }

}
