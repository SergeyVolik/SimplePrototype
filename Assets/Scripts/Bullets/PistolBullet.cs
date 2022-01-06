using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    public interface INoTragetBulletSoundEvent : ISoundEvent { }
    public interface INoTragetBulletEffectEvent : IEffectEvent { }
    public interface INoTragetBulletvent : INoTragetBulletSoundEvent, INoTragetBulletEffectEvent { }

    [RequireComponent(typeof(Rigidbody))]
    public class PistolBullet : MonoBehaviour, IBullet, INoTragetBulletvent
    {
        Rigidbody m_Rigidbody;
        [SerializeField]
        private UnityEvent m_OnHit;

        public UnityEvent OnEvent => m_OnHit;

        [SerializeField]
        private BulletPoolSO m_BulletPool;

        [SerializeField]
        private int m_Damage = 10;
        public int Damage => m_Damage;

        void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        public void Launch(float force)
        {
            m_Rigidbody.AddForce(m_Rigidbody.transform.forward * force);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var com = collision.gameObject.GetComponent<IDamageApplicator>();
            if (com != null)
            {
                com.DoDamage(m_Damage);

            }
            else {

                OnEvent.Invoke();


            }
            m_Rigidbody.angularVelocity = Vector3.zero;
            m_Rigidbody.velocity = Vector3.zero;
            m_BulletPool.Return(this);
           
            
        }



    }
}