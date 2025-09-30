using _Scripts.Data.UnityObjects;
using _Scripts.Data.UnityObjects.SO;
using _Scripts.Enums;
using _Scripts.Extensions;
using _Scripts.Keys;
using _Scripts.Managers;
using _Scripts.Managers.ServiceLocator;
using _Scripts.Systems.ObjectPooling;
using _Scripts.Utilities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts.Controllers.Enemy
{
    public class EnemyController : MonoBehaviour, IPoolObject<EnemyController>
    {
        private EnemyType _enemyType;
        private EnemyAbilityType _abilityType;
        
        private SpriteRenderer _spriteRenderer;

        private EnemyMoveController _moveController;
        private EnemyAnimController _animController;
        private EnemyHealth _enemyHealth;

        private ObjectPool<EnemyController> _pool;
        
        private static EnemyDataKeeper _dataKeeper;

        private CD_ENEMY _data;
        
        private float _realTriggerDamage;

        private void Awake()
        {
            _data = Resources.Load<CD_ENEMY>("Data/CD_ENEMY");
            
            transform.LookCamera();
            
            this.GetReference(ref _spriteRenderer);
            this.GetReference(ref _moveController);
            this.GetReference(ref _animController);
            this.GetReference(ref _enemyHealth);
            
            _moveController.InitializeValues(_data.MoveData);
            _enemyHealth.SetData(_data.TintData);

            _realTriggerDamage = _data.TriggerData.Damage;
            
            _dataKeeper = ServiceLocator.Get<EnemyDataKeeper>();
        }

        public void Initialize(float3[] randomPath, EntryData entryData)
        {
            _enemyType = entryData.EnemyType;
            _abilityType = entryData.AbilityType;
            _spriteRenderer.material = _dataKeeper.GetMaterialByType(_enemyType);
            _spriteRenderer.material.SetColor(ConstUtilities.COLOUR_REF, _dataKeeper.GetColorByAbilityType(entryData));
            _animController.SetAnimFrames(_dataKeeper.GetAnimFramesByType(entryData.EnemyType));
            _animController.TriggerAnim(EnemyAnimType.Walk);
            _moveController.InitializePath(randomPath);
            _enemyHealth.SetBaseMaterial();
            SetAbilityValues();
        }
        
        private void SetAbilityValues()
        {
            float value = _dataKeeper.GetFloatValueByAbilityType(_abilityType);
            if (_enemyType == EnemyType.Boss)
            {
                value *= 2;
            }
            switch (_abilityType)
            {
                case EnemyAbilityType.Normal:
                    break;
                case EnemyAbilityType.Blue:
                    _moveController.SetIncreaseMoveSpeedData(value);
                    break;
                case EnemyAbilityType.Green:
                    _realTriggerDamage += value;
                    break;
                case EnemyAbilityType.Red:
                    _enemyHealth.SetIncreaseMaxHealthData(value);
                    break;
            }
        }

        public void Reset()
        {
            _moveController.Reset();
            _enemyHealth.ResetHealthData();
            _realTriggerDamage = _data.TriggerData.Damage;
        }

        public void SetHealthDataValue(EnemyHealthKey healthKey)
        {
            _enemyHealth.SetHealth(healthKey);
        }

        public void SetPool(ObjectPool<EnemyController> pool)
        {
            _pool = pool;
        }

        public void ReleasePool()
        {
            _pool.Release(this);
        }

        public CD_ENEMY GetEnemyData()
        {
            return _data;
        }

        public float GetRealTriggerDamage()
        {
            return _realTriggerDamage;
        }
        
    }
}