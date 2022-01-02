using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class PistolBullet : MonoBehaviour, IBullet
    {
        Rigidbody m_Rigidbody;

        void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        public void Push(int force)
        {
            m_Rigidbody.AddForce(m_Rigidbody.transform.forward * force);
        }

        private void OnCollisionEnter(Collision collision)
        {
            PistolBulletPoolSingleton.Instance.Pool.Release(this);
            m_Rigidbody.velocity = Vector3.zero;
            m_Rigidbody.angularVelocity = Vector3.zero;
            Debug.Log(collision.gameObject.name);
        }

        [SerializeField]
        private int m_Damage = 10;
        public int Damage => m_Damage;
    }
}