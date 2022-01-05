using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(GunDataComponent))]
    public class PistolPlaceholder : MonoBehaviour, IGunPlaceholder, IProjectile
    {
        Rigidbody m_Rigidbody;
        GunDataComponent m_GunDataComponent;

        void Awake()
        {
            m_GunDataComponent = GetComponent<GunDataComponent>();
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        public GunDataComponent GunDataComponent => m_GunDataComponent;

        public IGunData Data => m_GunDataComponent;

        public int Damage => m_Damage;

        [SerializeField]
        private int m_Damage = 9999;
        [SerializeField]
        private float m_VelocitySpeedToDamage = 1;
        public void Drop()
        {
            gameObject.SetActive(true);
        }

        
        public void SetPositionAndRot(Vector3 post, Quaternion rot)
        {
            transform.SetPositionAndRotation(post, rot);
        }



        private void OnCollisionEnter(Collision collision)
        {

          
            var com = collision.gameObject.GetComponent<IDamageApplicator>();

            if (com != null && m_Rigidbody.velocity.magnitude > m_VelocitySpeedToDamage)
            {
              
                com.DoDamage(m_Damage);
                
            }

        }

        public void Launch(float force)
        {
            gameObject.SetActive(true);
            m_Rigidbody.AddForce(m_Rigidbody.transform.forward * force);
        }
    }

}