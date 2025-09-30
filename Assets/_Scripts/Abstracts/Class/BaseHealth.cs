using _Scripts.Abstracts.Interface;
using UnityEngine;

namespace _Scripts.Abstracts.Class
{
    public abstract class BaseHealth : MonoBehaviour, IDamageable
    {
        protected float MaxHealth = 100;
        protected float CurrentHealth;

        protected virtual void Start()
        {
            SetCurrentHealthToMax();
        }
        
        protected virtual void SetMaxHealth(float maxHealth)
        {
            MaxHealth = maxHealth;
        }
        
        public virtual void ResetHealthData()
        {
            SetCurrentHealthToMax();
        }
        
        protected virtual void SetCurrentHealthToMax()
        {
            CurrentHealth = MaxHealth;
        }
    
        public virtual void TakeDamage(float damage)
        {
            if (CurrentHealth <= 0) return;
            
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
    
            OnDamageTaken(damage);
            
            if (CurrentHealth <= 0)
                Die();
        }
    
        public float GetHealth() => CurrentHealth;
    
        protected abstract void OnDamageTaken(float damage);
        protected abstract void Die();
    }
}