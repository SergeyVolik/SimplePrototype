using System;
using System.Collections.Generic;
using UnityEngine;
namespace SerV112.UtilityAI.Game
{
    public class ObjectPool<T> : IObjectPool<T> where T : UnityEngine.Object
    {
        private Func<T> m_OnCreate;
        private Action<T> m_OnTakeFromPool;
        private Action<T> m_OnReturnedToPool;
        private Action<T> m_OnObjectDestroy;
        private int m_MaxCapacity;

        Stack<T> m_PooledObjects = new Stack<T>();
        public ObjectPool(Func<T> onCreate, Action<T> onTakeFromPool, Action<T> onReturnedToPool, Action<T> onObjectDestroy, int startCapacity, int maxCapacity)
        {
            m_OnCreate = onCreate;
            m_OnTakeFromPool = onTakeFromPool;
            m_OnReturnedToPool = onReturnedToPool;
            m_OnObjectDestroy = onObjectDestroy;
            m_MaxCapacity = maxCapacity;

            PrepareInternal(startCapacity);
        }


        T CreateObjectInternal()
        {

            return m_OnCreate();
        }

        void PrepareInternal(int startCapacity)
        {

            for (int i = 0; i < startCapacity; i++)
            {
                m_PooledObjects.Push(CreateObjectInternal());

            }
        }

        public int Capacity => m_PooledObjects.Count;

        public int MaxCapacity => m_MaxCapacity;

        public T Get()
        {
            T obj;

            if (m_PooledObjects.Count == 0)
            {

                obj = CreateObjectInternal();
            }
            else
            {
                obj = m_PooledObjects.Pop();
            }

            m_OnTakeFromPool(obj);

            return obj;
        }

        public bool Release(T obj)
        {

            if (obj != null)
            {
                if (Capacity == MaxCapacity)
                {
                    m_OnObjectDestroy(obj);
                }
                else
                {
                    m_OnReturnedToPool(obj);
                    m_PooledObjects.Push(obj);

                }

                return true;
            }
            else
            {
                Debug.LogError($"You are trying to return null object of type {typeof(T)} ");
            }
            return false;

        }
    }
}