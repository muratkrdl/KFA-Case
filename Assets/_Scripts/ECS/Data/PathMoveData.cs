using _Scripts.Extensions;
using Unity.Entities;
using UnityEngine;

namespace _Scripts.ECS.Data
{
    public struct PathMoveData : IComponentData
    {
        public byte CurrentPath;
        public float Timer;
        public float MoveTime;
        public bool ReachedEndOfThePath;
        public Vector3 CurrentPosition;
    }
}