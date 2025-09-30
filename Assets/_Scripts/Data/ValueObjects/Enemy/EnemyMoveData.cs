using System;
using UnityEngine;

namespace _Scripts.Data.ValueObjects.Enemy
{
    [Serializable]
    public struct EnemyMoveData
    {
        [SerializeField] private float moveSpeed;
        
        public float MoveSpeed => 1/moveSpeed;
    }
}