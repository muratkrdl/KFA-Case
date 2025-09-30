using _Scripts.Controllers.Player;
using _Scripts.Data.UnityObjects;
using _Scripts.Events;
using _Scripts.Extensions;
using _Scripts.Systems.ObjectPooling.Pools;
using Unity.Mathematics;
using UnityEngine;

namespace _Scripts.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        // References
        private PlayerMovementController _movementController;
        private PlayerHandleAnimator _handleAnimator;
        private PlayerEnemyDetectorController _enemyDetectorController;
        private PlayerPhysicsController _physicsController;
        private PlayerHealth _health;
        private PlayerProjectileShooter _projectileShooter;
        
        // Services
        private InputEvents _inputEvents;
        private SFXManager _sfxManager;
        private ProjectileObjectPool _projectilePool;

        // Data
        private CD_PLAYER _data;
        
        private void Awake()
        {
            transform.LookCamera();
            
            GetReferences();
            GetServices();
            SetData();
        }

        private void GetReferences()
        {
            this.GetReference(ref _movementController);
            this.GetReference(ref _handleAnimator);
            this.GetReference(ref _enemyDetectorController);
            this.GetReference(ref _physicsController);
            this.GetReference(ref _health);
            this.GetReferenceChildren(ref _projectileShooter);
        }

        private void GetServices()
        {
            _inputEvents = ServiceLocator.ServiceLocator.Get<InputEvents>();
            _projectilePool = ServiceLocator.ServiceLocator.Get<ProjectileObjectPool>();
            _sfxManager = ServiceLocator.ServiceLocator.Get<SFXManager>();
        }

        private void SetData()
        {
            _data = Resources.Load<CD_PLAYER>("Data/CD_PLAYER");
            
            _movementController.SetData(_data.MovementData);
            _handleAnimator.SetData(_sfxManager);
            _enemyDetectorController.SetData(_data.DetectionData);
            _physicsController.SetData(_data.KnockbackData, _health);
            _health.SetData(_data.IFrameData, _data.HealthData, _sfxManager);
            _projectileShooter.SetData(_data.ProjectileData, _projectilePool, _enemyDetectorController);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _inputEvents.OnMoveStart += OnMoveStart;
            _inputEvents.OnMoveStop += OnMoveStop;
        }
        
        private void OnMoveStart(float3 value)
        {
            _handleAnimator.HandleDirection(value);
            _movementController.HandleMoveInput(value);
        }

        private void OnMoveStop(float3 value)
        {
            _handleAnimator.HandleSpeed(value);
            _movementController.HandleMoveInput(value);
        }

        private void UnSubscribeEvents()
        {
            _inputEvents.OnMoveStart -= OnMoveStart;
            _inputEvents.OnMoveStop -= OnMoveStop;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
    }
}