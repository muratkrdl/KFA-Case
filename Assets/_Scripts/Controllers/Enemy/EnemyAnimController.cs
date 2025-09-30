using System;
using _Scripts.Data.UnityObjects.SO;
using _Scripts.ECS.Data;
using _Scripts.Enums;
using Unity.Entities;
using UnityEngine;

namespace _Scripts.Controllers.Enemy
{
    public class EnemyAnimController : MonoBehaviour
    {
        [SerializeField] private float frameTime = 0.1f;

        private EnemyAnimFrames _enemyAnimFrames;

        private Entity _entity;
        private EntityManager _entityManager;
        private SpriteRenderer _spriteRenderer;

        private Sprite[] _currentSprites;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            EntityArchetype archetype = _entityManager.CreateArchetype(typeof(SpriteAnimData));
            _entity = _entityManager.CreateEntity(archetype);
        }

        private void LateUpdate()
        {
            if (!_entityManager.Exists(_entity) || !_enemyAnimFrames) return;

            var animData = _entityManager.GetComponentData<SpriteAnimData>(_entity);
            _spriteRenderer.sprite = _currentSprites[animData.CurrentFrame];
        }

        public void SetAnimFrames(EnemyAnimFrames enemyFrames)
        {
            _enemyAnimFrames = enemyFrames;
        }

        public void TriggerAnim(EnemyAnimType type)
        {
            SetNewAnimData(type switch
            {
                EnemyAnimType.Walk => _enemyAnimFrames.WalkFrames,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            });
        }

        private void SetNewAnimData(Sprite[] newSprite)
        {
            _currentSprites = newSprite;
            _entityManager.SetComponentData(_entity, new SpriteAnimData
            {
                CurrentFrame = 0,
                Timer = 0,
                FrameTime = frameTime,
                FrameCount = (byte)_currentSprites.Length,
            });
        }

    }
}