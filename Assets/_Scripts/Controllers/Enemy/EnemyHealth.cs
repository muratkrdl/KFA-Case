using System.Transactions;
using _Scripts.Abstracts.Class;
using _Scripts.Data.ValueObjects.Enemy;
using _Scripts.Events;
using _Scripts.Extensions;
using _Scripts.Keys;
using _Scripts.Managers;
using _Scripts.Managers.ServiceLocator;
using _Scripts.Utilities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Scripts.Controllers.Enemy
{
    public class EnemyHealth : BaseHealth
    {
        private EnemyTintData _data;
        
        private SpriteRenderer _spriteRenderer;
        private Material _baseMaterial;

        private WaveEvents _waveEvents;
        private SFXManager _sfxManager;
        
        private bool _isDead;
        
        private void Awake()
        {
            this.GetReference(ref _spriteRenderer);

            _waveEvents = ServiceLocator.Get<WaveEvents>();
            _sfxManager = ServiceLocator.Get<SFXManager>();
        }

        public override void TakeDamage(float damage)
        {
            _sfxManager.PlaySFX(SFXNames.HIT_ENEMY);
            base.TakeDamage(damage);
        }

        public override void ResetHealthData()
        {
            base.ResetHealthData();
            _isDead = true;
        }

        protected override void OnDamageTaken(float damage)
        {
            // Hit Anim
            _ = TintEnemy();
        }

        public void SetIncreaseMaxHealthData(float increment)
        {
            MaxHealth += increment;
            SetCurrentHealthToMax();
        }

        private async UniTaskVoid TintEnemy()
        {
            _spriteRenderer.material = _data.TintMaterial;
            await UniTask.WaitForSeconds(_data.TintDuration);
            _spriteRenderer.material = _baseMaterial;
        }

        protected override void Die()
        {
            _isDead = true;
            if (_data.DeathEffect)
                Instantiate(_data.DeathEffect, transform.position, Quaternion.identity);

            // Release Pool
            _waveEvents.RaiseEnemyDied(this.GetJustReference<EnemyController>());
            this.GetJustReference<EnemyController>().ReleasePool();
        }

        public void SetHealth(EnemyHealthKey healthKey)
        {
            MaxHealth = healthKey.Health + healthKey.IncrementByWave * healthKey.CurrentWaveIndex;
            SetCurrentHealthToMax();
        }

        public void SetData(EnemyTintData tintData)
        {
            _data = tintData;
        }

        public void SetBaseMaterial()
        {
            _baseMaterial = _spriteRenderer.material;
        }
        
    }
}