using _Scripts.Extensions;
using _Scripts.Managers;
using _Scripts.Managers.ServiceLocator;
using _Scripts.Utilities;
using Unity.Mathematics;
using UnityEngine;

namespace _Scripts.Controllers.Player
{
    public class PlayerHandleAnimator : MonoBehaviour
    {
        private Animator _animator;

        private SFXManager _sfxManager;

        private void Awake()
        {
            this.GetReference(ref _animator);

            _sfxManager = ServiceLocator.Get<SFXManager>();
        }

        public void HandleDirection(float3 moveInput)
        {
            _animator.SetFloat(ConstUtilities.LAST_DIR_X, moveInput.x);
            _animator.SetFloat(ConstUtilities.LAST_DIR_Y, moveInput.z);
            HandleSpeed(moveInput);
        }

        public void HandleSpeed(float3 moveInput)
        {
            _animator.SetFloat(ConstUtilities.SPEED, moveInput.SqrMagnitude());
        }

        public void AnimEvent_PlayWalkSFX()
        {
            _sfxManager.PlaySFX(SFXNames.PLAYER_WALK);
        }

        public void SetData(SFXManager sfxManager)
        {
            _sfxManager = sfxManager;
        }

    }
}