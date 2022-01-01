using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ObjectPoolSingleton<T> : MonoBehaviour where T : UnityEngine.Object
{
    [SerializeField]
    protected T m_Prefab;

    [SerializeField]
    private int m_StartCapacity = 10;
    [SerializeField]
    private int m_MaxCapacity = 10;

    IObjectPool<T> m_PoolOfPistols;

    public IObjectPool<T> PoolOfPistols => m_PoolOfPistols;

    public static ObjectPoolSingleton<T> Intsance;
    private void Awake()
    {
        if (Intsance)
            Destroy(this);

        Intsance = this;

        m_PoolOfPistols = new ObjectPool<T>(CreateObject, TakeFromPool, ReturnToPool, DestroyObject, m_StartCapacity, m_MaxCapacity);
    }

    protected abstract T CreateObject();

    protected abstract void DestroyObject(T pistol);

    protected abstract void TakeFromPool(T pistol);

    protected abstract void ReturnToPool(T pistol);
}