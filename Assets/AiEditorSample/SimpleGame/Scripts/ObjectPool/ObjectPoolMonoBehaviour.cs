using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public abstract class ObjectPoolMonoBehaviour<T> : MonoBehaviour where T : UnityEngine.Object
    {
        [SerializeField]
        protected T m_Prefab;

        [SerializeField]
        private int m_StartCapacity = 10;

        IObjectPool<T> m_Pool;

        public IObjectPool<T> Pool => m_Pool;

        protected virtual void Awake()
        {

            m_Pool = new ObjectPool<T>(CreateObject, TakeFromPool, ReturnToPool, DestroyObject, m_StartCapacity);
        }

        protected abstract T CreateObject();

        protected abstract void DestroyObject(T obj);

        protected abstract void TakeFromPool(T obj);

        protected abstract void ReturnToPool(T obj);
    }
}
