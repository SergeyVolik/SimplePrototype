using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    public interface IShootable : IActionable
    {


    }

    public interface IShootEvent : IShootSoundEvent, IShootEffectEvent
    {

    }
}