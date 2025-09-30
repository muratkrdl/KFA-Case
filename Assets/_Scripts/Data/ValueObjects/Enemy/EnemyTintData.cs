using System;
using UnityEngine;

namespace _Scripts.Data.ValueObjects.Enemy
{
    [Serializable]
    public struct EnemyTintData
    {
        public GameObject DeathEffect;
        public Material TintMaterial;
        public float TintDuration;
    }
}