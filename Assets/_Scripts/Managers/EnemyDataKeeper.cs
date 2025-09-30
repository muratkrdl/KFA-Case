using System;
using System.Collections.Generic;
using _Scripts.Data.UnityObjects.SO;
using _Scripts.Enums;
using _Scripts.Keys;
using UnityEngine;

namespace _Scripts.Managers
{
    public class EnemyDataKeeper : MonoBehaviour
    {
        private const float HealthIncrement = 25f;
        private const float PowerIncrement = 2.5f;
        private const float SpeedIncrement = .25f;
        
        private readonly Color _blueColor = new(.14f, .62f, .82f, 1);
        private readonly Color _redColor = new(.82f, .14f, .2f, 1);
        private readonly Color _greenColor = new(.54f, .82f, .14f, 1);
        
        private readonly Dictionary<EnemyType, EnemyAnimFrames> _animFramesDict = new();
        
        private readonly Dictionary<EnemyType, Material> _materialDict = new();

        private void Awake()
        {
            var animFrames = Resources.LoadAll<EnemyAnimFrames>("Data/Enemy/AnimFrames");
            foreach (var frame in animFrames)
            {
                if (Enum.TryParse<EnemyType>(frame.name, out var type))
                {
                    _animFramesDict[type] = frame;
                }
            }
            
            var materials = Resources.LoadAll<Material>("Data/Enemy/EnemyMaterials");
            foreach (var mat in materials)
            {
                if (Enum.TryParse<EnemyType>(mat.name, out var type))
                {
                    _materialDict[type] = mat;
                }
            }
        }

        public EnemyAnimFrames GetAnimFramesByType(EnemyType enemyType)
        {
            return _animFramesDict.GetValueOrDefault(enemyType);
        }

        public Material GetMaterialByType(EnemyType enemyType)
        {
            return _materialDict.GetValueOrDefault(enemyType);
        }

        public Color GetColorByAbilityType(EntryData entryData)
        {
            return entryData.AbilityType switch
            {
                EnemyAbilityType.Blue => _blueColor,
                EnemyAbilityType.Green => _greenColor,
                EnemyAbilityType.Red => _redColor,
                _ => GetNormalColorByEnemyType()
            };
        }

        private Color GetNormalColorByEnemyType()
        {
            return Color.white;
        }

        public float GetFloatValueByAbilityType(EnemyAbilityType abilityType)
        {
            return abilityType switch
            {
                EnemyAbilityType.Blue => SpeedIncrement,
                EnemyAbilityType.Green => PowerIncrement,
                EnemyAbilityType.Red => HealthIncrement,
                _ => 0
            };
        }
        
    }
}