using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Data.ValueObjects.Projectile
{
    [Serializable]
    public struct ProjectileMoveData
    {
        public float YOffset;
        public float Duration;
        
        // DGTween Ease
        [SerializeField] private AnimationCurve[] animationCurves;

        public AnimationCurve GetEase()
        {
            return animationCurves[Random.Range(0, animationCurves.Length)];
        }
        
    }
}