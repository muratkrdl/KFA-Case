using System;
using _Scripts.Events;
using _Scripts.Extensions;
using _Scripts.Systems.ObjectPooling.Pools;
using UnityEngine;

namespace _Scripts.Managers.ServiceLocator
{
    public class GameBootstrapper : MonoBehaviour
    {
        private InputManager _inputManager;
        private InputEvents _inputEvents;
        
        private SFXManager _sfxManager;
        private SFXObjectPool _sfxObjectPool;

        private void Awake()
        {
            GetReferences();
            SetServices();
        }

        private void GetReferences()
        {
            this.GetReference(ref _inputManager);
            this.GetReference(ref _inputEvents);
            this.GetReference(ref _sfxManager);
            this.GetReference(ref _sfxObjectPool);
        }

        private void SetServices()
        {
            ServiceLocator.Register(_inputManager);
            ServiceLocator.Register(_inputEvents);
            ServiceLocator.Register(_sfxManager);
            ServiceLocator.Register(_sfxObjectPool);
        }
        
    }
}