using _Scripts.Abstracts.Class;
using _Scripts.Data.ValueObjects.Player;
using _Scripts.Extensions;
using _Scripts.Managers;
using _Scripts.Utilities;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Controllers.Player
{
    public class PlayerHealth : BaseHealth
    {
        private PlayerIFrameData _data;

        private SpriteRenderer _playerRenderer;

        private SFXManager _sfxManager;

        private bool _isOnIFrame;

        private void Awake()
        {
            this.GetReference(ref _playerRenderer);
        }

        public override void TakeDamage(float damage)
        {
            if (_isOnIFrame) return;

            base.TakeDamage(damage);
            _sfxManager.PlaySFX(SFXNames.HIT_PLAYER);
            ActivateIFrame();
        }

        private void ActivateIFrame()
        {
            _isOnIFrame = true;
            _ = SetInvulnerableVisual();
        }

        private async UniTaskVoid SetInvulnerableVisual()
        {
            _playerRenderer.SetAlpha(0);
            _playerRenderer.DOFade(1, _data.IFrameDuration).SetEase(_data.Ease);
            await UniTask.WaitForSeconds(_data.IFrameDuration);
            _isOnIFrame = false;
        }

        protected override void OnDamageTaken(float damage)
        {
            Debug.Log($"Player took {damage} damage. HP: {CurrentHealth}/{MaxHealth}");
            // Camera shake
        }

        protected override void Die()
        {
            Debug.Log("Player died!");
            // GameOver
        }

        public void SetData(PlayerIFrameData data, PlayerHealthData healthData, SFXManager sfxManager)
        {
            _data = data;
            _sfxManager = sfxManager;
            SetMaxHealth(healthData.MaxHealth);
        }

    }
}