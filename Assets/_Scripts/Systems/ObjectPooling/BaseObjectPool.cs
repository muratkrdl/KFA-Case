using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts.Systems.ObjectPooling
{
    public abstract class BaseObjectPool<T1> : MonoBehaviour where T1 : MonoBehaviour, IPoolObject<T1>
    {
        [SerializeField] protected T1 prefab;
        [SerializeField] protected Transform parent;

        [SerializeField] private int defaultPoolSize;
        [SerializeField] private int maxPoolSize;

        protected ObjectPool<T1> Pool;

        private void Awake()
        {
            Pool = new ObjectPool<T1> 
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
            var obj = Instantiate(prefab, parent);
            obj.SetPool(Pool);
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

        public virtual T1 GetFromPool()
        {
            return Pool.Get();
        }
        
    }
}