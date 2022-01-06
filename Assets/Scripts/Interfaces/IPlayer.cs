using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
namespace SerV112.UtilityAI.Game
{
    public interface IPlayer : IOwner
    {

    }
    public interface IOwner
    {
    }
    public interface IEnemy : IMoveable, IOwner
    {

    }

    public interface IHealthChangedEvent : ISoundEvent
    {

    }
    public interface IHealthData : IHealthChangedEvent
    {
        int Health { get; set; }
        int MaxHealth { get; set; }

    }

    public interface IKillable : IDeathSoundEvent, IDeathEffectEvent
    {

        void ForceDead();
    }
    public interface IHealable
    {
        void Heal(int value);

    }

   


    public interface IDamageApplicator : IHitSoundEvent, IHitEffectEvent
    {

        void DoDamage(int value);
    }

    public interface IMoveable
    {
        bool IsMove { get; }
        float Horizontal { get; }
        float Vertical { get; }
    }

}