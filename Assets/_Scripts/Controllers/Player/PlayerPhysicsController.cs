using _Scripts.Controllers.Enemy;
using _Scripts.Data.ValueObjects.Player;
using _Scripts.Extensions;
using _Scripts.Managers;
using _Scripts.Utilities;
using UnityEngine;

namespace _Scripts.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        private PlayerKnockbackData _data;
        
        private PlayerHealth _playerHealth;
        
        private Rigidbody _rb;
        
        private void Awake()
        {
            this.GetReference(ref _rb);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<EnemyController>(out var controller))
            {
                _playerHealth.TakeDamage(controller.GetRealTriggerDamage());
                Knockback(other.transform);
            }
        }

        private void Knockback(Transform other)
        {
            Vector3 direction = (transform.position - other.position).normalized;
            direction.y = 0;
            
            _rb.linearVelocity = ConstUtilities.Zero3;
            _rb.AddForce(direction * _data.KnockbackForce, ForceMode.Force);
        }

        public void SetData(PlayerKnockbackData data, PlayerHealth playerHealth)
        {
            _data = data;
            _playerHealth = playerHealth;
        }

    }
}
