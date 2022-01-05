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

    public interface IHealthData
    {
        int Health { get; set; }
        int MaxHealth { get; set; }

        UnityEvent OnHealthChanged { get; }
    }

    public interface IKillable
    {
        public UnityEvent OnDeadth { get; }

        void ForceDead();
    }
    public interface IHealable
    {
        void Heal(int value);

    }
    public interface IDamageApplicator
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