using _Scripts.Data.ValueObjects.Player;
using _Scripts.Systems.ObjectPooling.Pools;
using UnityEngine;

namespace _Scripts.Controllers.Player
{
    public class PlayerProjectileShooter : MonoBehaviour
    {
        private PlayerProjectileData _data;
        
        private ProjectileObjectPool _projectileObjectPool;
        private PlayerEnemyDetectorController _detectorController;
        
        private void Start()
        {
            InvokeRepeating(nameof(Shoot),0, _data.AttackCoolDown);
        }

        public void Shoot()
        {
            var closestEnemy = _detectorController.GetClosestEnemy(_data.Range);
            if (!closestEnemy) return;
            
            var obj = _projectileObjectPool.GetFromPool();
            obj.transform.position = transform.position;
            obj.Initialize(closestEnemy);
        }

        public void SetData(PlayerProjectileData data, ProjectileObjectPool pool, PlayerEnemyDetectorController detector)
        {
            _data = data;
            _projectileObjectPool = pool;
            _detectorController = detector;
        }

    }
}