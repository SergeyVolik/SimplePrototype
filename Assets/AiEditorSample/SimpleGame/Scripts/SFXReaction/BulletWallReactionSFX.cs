using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(INoTragetBulletSoundEvent))]
    public class BulletWallReactionSFX : PlaySoundComponent<INoTragetBulletSoundEvent> { }

}

