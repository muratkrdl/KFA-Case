using _Scripts.Controllers.Enemy;
using _Scripts.Data.UnityObjects;
using _Scripts.Data.ValueObjects;
using _Scripts.Data.ValueObjects.Player;
using _Scripts.Data.ValueObjects.Projectile;
using _Scripts.Extensions;
using _Scripts.Managers;
using _Scripts.Managers.ServiceLocator;
using _Scripts.Systems.ObjectPooling;
using _Scripts.Utilities;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts.Controllers
{
    public class ProjectileController : MonoBehaviour, IPoolObject<ProjectileController>
    {
        private ObjectPool<ProjectileController> _pool;

        private GameObject _enemy;
        private Tween _activeTween;
        private Animator _animator;

        private SFXManager _sfxManager;

        private CD_PROJECTILE _data;

        private void Awake()
        {
            _data = Resources.Load<CD_PROJECTILE>("Data/CD_PROJECTILE");
            this.GetReference(ref _animator);

            _sfxManager = ServiceLocator.Get<SFXManager>();
        }

        private void Start()
        {
            transform.LookCamera();
        }

        public void Initialize(GameObject enemy)
        {
            _enemy = enemy;
            transform.DOScale(ConstUtilities.One3, .1f);
            _ = MoveProjectile();
        }

        public void Reset()
        {
            _enemy = null;
            transform.localScale = ConstUtilities.Zero3;
        }
        
        private async UniTaskVoid MoveProjectile()
        {
            Vector3 startPos = transform.position;
            float t = 0f;

            _activeTween = DOTween.To(() => t, x => t = x, 1f, _data.ProjectileMoveData.Duration)
                .SetEase(_data.ProjectileMoveData.GetEase())
                .SetUpdate(true)
                .OnUpdate(() =>
                {
                    if (!_enemy) return;

                    Vector3 endPos = _enemy.transform.position;
                    Vector3 horizontal = Vector3.Lerp(startPos, endPos, t);
                    float yOffset = Mathf.Sin(t * Mathf.PI) * _data.ProjectileMoveData.YOffset;
                    transform.position = horizontal + Vector3.up * yOffset;
                });
            
            try
            {
                await _activeTween.AsyncWaitForCompletion();
                _sfxManager.PlaySFX(SFXNames.PROJECTILE_BOOM);
                _animator.SetTrigger(ConstUtilities.Anim);
            }
            catch (System.Exception)  { /* */ }
        }

        public void OnAnim_Damage()
        {
            // physic overlapSphere
            Collider[] hits = Physics.OverlapSphere(transform.position, _data.ProjectileDamageData.Radius, LayerMask.GetMask("Enemy"));
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<EnemyHealth>(out var health))
                {
                    health.TakeDamage(_data.ProjectileDamageData.Damage);
                }
            }
        }

        public void OnAnim_Death()
        {
            _activeTween?.Kill();
            ReleasePool();
        }

        public void SetPool(ObjectPool<ProjectileController> pool)
        {
            _pool = pool;
        }

        public void ReleasePool()
        {
            _pool.Release(this);
        }

        private void OnDisable()
        {
            _activeTween?.Kill();
        }
    }
}