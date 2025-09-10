using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts.Systems.ObjectPooling
{
    public abstract class BaseObjectPool<T1> : MonoBehaviour where T1 : MonoBehaviour, IPoolObject<T1>
    {
        [SerializeField] protected T1 prefab;

        [SerializeField] private int defaultPoolSize;
        [SerializeField] private int maxPoolSize;

        private ObjectPool<T1> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<T1> 
            (
                OnCreate,
                OnGet,
                OnRelease,
                OnDestroyForPool,
                true,
                defaultPoolSize,
                maxPoolSize 
            );
        }

        protected virtual T1 OnCreate()
        {
            var obj = Instantiate(prefab, transform);
            obj.SetPool(_pool);
            return obj;
        }

        protected virtual void OnGet(T1 obj)
        {
            obj.gameObject.SetActive(true);
        }

        protected virtual void OnRelease(T1 obj)
        {
            obj.gameObject.SetActive(false);
        }

        protected virtual void OnDestroyForPool(T1 obj)
        {
            Destroy(obj);
        }

        public T1 GetFromPool()
        {
            return _pool.Get();
        }
        
    }
}