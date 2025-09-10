using UnityEngine.Pool;

namespace _Scripts.Systems.ObjectPooling
{
    public interface IPoolObject<T> where T : class
    {
        void SetPool(ObjectPool<T> pool);
        void ReleasePool();
    }
}