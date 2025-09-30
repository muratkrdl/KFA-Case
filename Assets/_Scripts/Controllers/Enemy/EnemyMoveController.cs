using _Scripts.Data.UnityObjects;
using _Scripts.Data.ValueObjects.Enemy;
using _Scripts.ECS.Buffer;
using _Scripts.ECS.Data;
using _Scripts.Events;
using _Scripts.Extensions;
using _Scripts.Managers.ServiceLocator;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace _Scripts.Controllers.Enemy
{
    public class EnemyMoveController : MonoBehaviour
    {
        private Entity _entity;
        private EntityManager _entityManager;

        private EnemyMoveData _data;
        
        private Transform _transform;
        
        private static WaveEvents _waveEvents;

        private float _realSpeed;
        
        private PathMoveData _pathMoveData;
        
        private void Awake()
        {
            _waveEvents = ServiceLocator.Get<WaveEvents>();
            
            _transform = transform;

            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            EntityArchetype archetype = _entityManager.CreateArchetype(typeof(PathMoveData), typeof(LocalTransform));
            _entity = _entityManager.CreateEntity(archetype);

            _entityManager.AddBuffer<PathPoint>(_entity);
        }

        public void InitializeValues(EnemyMoveData data)
        {
            _data = data;
            _realSpeed = _data.MoveSpeed;
            _entityManager.SetComponentData(_entity, new PathMoveData
            {
                CurrentPath = 0,
                Timer = 0,
                MoveTime = _realSpeed,
                CurrentPosition = transform.position,
            });
        }

        public void SetIncreaseMoveSpeedData(float increment)
        {
            _realSpeed -= increment;
            PathMoveData data = _entityManager.GetComponentData<PathMoveData>(_entity);
            _entityManager.SetComponentData(_entity, new PathMoveData
            {
                CurrentPath = data.CurrentPath,
                Timer = data.Timer,
                MoveTime = _realSpeed,
                CurrentPosition = data.CurrentPosition,
            });
        }

        public void InitializePath(float3[] randomPath)
        {
            var buffer = _entityManager.GetBuffer<PathPoint>(_entity);
            foreach (var position in randomPath)
            {
                buffer.Add(new PathPoint { Position = position });
            }
            PathMoveData data = _entityManager.GetComponentData<PathMoveData>(_entity);
            _entityManager.SetComponentData(_entity, new PathMoveData
            {
                CurrentPath = 0,
                Timer = data.Timer,
                MoveTime = _realSpeed,
                CurrentPosition = data.CurrentPosition,
            });
        }

        public void Reset()
        {
            _entityManager.GetBuffer<PathPoint>(_entity).Clear();
            _realSpeed = _data.MoveSpeed;
        }

        private void Update()
        {
            if (!_entityManager.Exists(_entity)) return;
            
            var animData = _entityManager.GetComponentData<PathMoveData>(_entity);
            
            if (animData.ReachedEndOfThePath)
            {
                // Damage base
                // Release pool
                _waveEvents.RaiseEnemyDied(this.GetJustReference<EnemyController>());
                this.GetJustReference<EnemyController>().ReleasePool();
                return;
            }
            
            _transform.position = animData.CurrentPosition;
        }

    }
}