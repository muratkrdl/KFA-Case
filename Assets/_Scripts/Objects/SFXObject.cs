using System;
using System.Threading;
using _Scripts.Data.UnityObjects.SO;
using _Scripts.Systems.ObjectPooling;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace _Scripts.Objects
{
    public class SFXObject : MonoBehaviour, IPoolObject<SFXObject>
    {
        [SerializeField] private AudioSource audioSource;

        private CancellationTokenSource _cts;

        private ObjectPool<SFXObject> _pool;

        private void Awake()
        {
            _cts = new CancellationTokenSource();
        }

        public void SetPool(ObjectPool<SFXObject> pool)
        {
            _pool = pool;
        }

        public void Play(SFXData data, AudioMixerGroup audioMixerGroupByType)
        {
            audioSource.clip = data.GetClip();
            audioSource.loop = data.Loop;
            audioSource.volume = data.Volume;
            audioSource.pitch = data.GetPitch();
            audioSource.spatialBlend = data.Is3D;
            audioSource.maxDistance = data.MaxDistance;
            audioSource.outputAudioMixerGroup = audioMixerGroupByType;
            audioSource.Play();
            Release().Forget();
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        private async UniTaskVoid Release()
        {
            await UniTask.WaitUntil(() => !audioSource.isPlaying, cancellationToken: _cts.Token);
            ReleasePool();
        }

        public void ReleasePool()
        {
            _pool.Release(this);
        }

        private void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

    }
}