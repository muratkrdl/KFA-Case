using _Scripts.Controllers.Enemy;
using _Scripts.Utilities;
using UnityEngine;

namespace _Scripts.Systems.ObjectPooling.Pools
{
    public class EnemyObjectPool : BaseObjectPool<EnemyController>
    {
        protected override EnemyController OnCreate()
        {
            var obj = Instantiate(prefab, ConstUtilities.OutOfWorld3, Quaternion.identity, parent);
            obj.SetPool(Pool);
            return obj;
        }

        protected override void OnRelease(EnemyController obj)
        {
            obj.Reset();
            base.OnRelease(obj);
        }
    }
}