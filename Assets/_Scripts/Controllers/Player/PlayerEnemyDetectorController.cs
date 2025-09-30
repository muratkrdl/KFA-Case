using _Scripts.Data.ValueObjects.Player;
using UnityEngine;

namespace _Scripts.Controllers.Player
{
    public class PlayerEnemyDetectorController : MonoBehaviour
    {
        private PlayerDetectionData _data;

        public GameObject GetClosestEnemy(float range)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, range, _data.EnemyLayer);

            if (hits.Length == 0)
            {
                return null;
            }
            
            GameObject closest = null;
            float minDistanceSqr = Mathf.Infinity;
            Vector3 playerPos = transform.position;

            foreach (var enemy in hits)
            {
                Vector3 dir = enemy.transform.position - playerPos;
                float dSqr = dir.sqrMagnitude;
                
                if (!(dSqr < minDistanceSqr)) continue;
                
                minDistanceSqr = dSqr;
                closest = enemy.gameObject;
            }

            return closest;
        }

        public void SetData(PlayerDetectionData data)
        {
            _data = data;
        }
        
    }
}