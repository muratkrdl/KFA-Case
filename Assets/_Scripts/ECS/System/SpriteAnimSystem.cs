using _Scripts.ECS.Data;
using Unity.Burst;
using Unity.Entities;

namespace _Scripts.ECS.System
{
    [BurstCompile]
    public partial struct SpriteAnimSystem : ISystem
    {
        [BurstCompile]
        public partial struct AnimJob : IJobEntity
        {
            public float DeltaTime;

            public void Execute(ref SpriteAnimData anim)
            {
                if (anim.FrameCount > 0)
                {
                    anim.Timer += DeltaTime;
                    if (anim.Timer >= anim.FrameTime)
                    {
                        anim.Timer -= anim.FrameTime;
                        anim.CurrentFrame = (byte)((anim.CurrentFrame + 1) % anim.FrameCount);
                    }
                }
                else
                {
                    anim.CurrentFrame = 0;
                }
                
            }
        }

        public void OnUpdate(ref SystemState state)
        {
            var job = new AnimJob
            {
                DeltaTime = SystemAPI.Time.DeltaTime,
            };

            job.ScheduleParallel();
        }
        
    }
}