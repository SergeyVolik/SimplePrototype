public interface IObjectPool<T> where T : UnityEngine.Object
{
    T Get();
    bool Release(T obj);

    int Capacity { get; }
    int MaxCapacity { get; }
}

