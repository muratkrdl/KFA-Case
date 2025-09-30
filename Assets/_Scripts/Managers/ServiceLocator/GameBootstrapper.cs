using _Scripts.Events;
using _Scripts.Extensions;
using _Scripts.Systems.ObjectPooling.Pools;
using UnityEngine;

namespace _Scripts.Managers.ServiceLocator
{
    public class GameBootstrapper : MonoBehaviour
    {
        private InputManager _inputManager;
        private SFXManager _sfxManager;
        private WaveManager _waveManager;

        private InputEvents _inputEvents;
        private WaveEvents _waveEvents;

        private PathKeeper _pathKeeper;
        private EnemyDataKeeper _enemyDataKeeper;

        private SFXObjectPool _sfxObjectPool;
        private ProjectileObjectPool _projectileObjectPool;
        private EnemyObjectPool _enemyObjectPool;

        private void Awake()
        {
            GetReferences();
            RegisterServices();
        }

        private void GetReferences()
        {
            this.GetReference(ref _inputManager);
            this.GetReference(ref _sfxManager);
            this.GetReference(ref _waveManager);
            this.GetReference(ref _inputEvents);
            this.GetReference(ref _waveEvents);
            this.GetReference(ref _pathKeeper);
            this.GetReference(ref _enemyDataKeeper);
            this.GetReference(ref _sfxObjectPool);
            this.GetReference(ref _projectileObjectPool);
            this.GetReference(ref _enemyObjectPool);
        }

        private void RegisterServices()
        {
            ServiceLocator.Register(_inputManager);
            ServiceLocator.Register(_sfxManager);
            ServiceLocator.Register(_waveManager);
            ServiceLocator.Register(_inputEvents);
            ServiceLocator.Register(_waveEvents);
            ServiceLocator.Register(_pathKeeper);
            ServiceLocator.Register(_enemyDataKeeper);
            ServiceLocator.Register(_sfxObjectPool);
            ServiceLocator.Register(_projectileObjectPool);
            ServiceLocator.Register(_enemyObjectPool);
        }

        private void OnDestroy()
        {
            UnRegisterServices();
        }

        private void UnRegisterServices()
        {
            ServiceLocator.Unregister<InputManager>();
            ServiceLocator.Unregister<SFXManager>();
            ServiceLocator.Unregister<WaveManager>();
            ServiceLocator.Unregister<InputEvents>();
            ServiceLocator.Unregister<WaveEvents>();
            ServiceLocator.Unregister<PathKeeper>();
            ServiceLocator.Unregister<EnemyDataKeeper>();
            ServiceLocator.Unregister<SFXObjectPool>();
            ServiceLocator.Unregister<ProjectileObjectPool>();
            ServiceLocator.Unregister<EnemyObjectPool>();
        }

    }
}