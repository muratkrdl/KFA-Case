using Unity.Entities;

namespace _Scripts.ECS.Data
{
    public struct SpriteAnimData : IComponentData
    {
        public byte CurrentFrame;
        public float Timer;
        public float FrameTime;
        public byte FrameCount;
    }
}
