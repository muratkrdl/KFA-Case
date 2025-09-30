using Unity.Entities;
using Unity.Mathematics;

namespace _Scripts.ECS.Buffer
{
    public struct PathPoint : IBufferElementData
    {
        public float3 Position;
    }
}