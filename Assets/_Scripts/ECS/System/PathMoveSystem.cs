using _Scripts.ECS.Buffer;
using _Scripts.ECS.Data;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using static Unity.Entities.SystemAPI;

namespace _Scripts.ECS.System
{
    [BurstCompile]
    public partial struct PathMoveSystem : ISystem
    {
        [BurstCompile]
        public partial struct MoveJob : IJobEntity
        {
            public float DeltaTime;
            
            public void Execute(ref PathMoveData anim, DynamicBuffer<PathPoint> pathPoints)
            {
                if (anim.CurrentPath >= pathPoints.Length -1)
                {
                    // Reached the end of the path
                    anim.ReachedEndOfThePath = true;
                    return;
                }
                
                float3 start = pathPoints[anim.CurrentPath].Position;
                float3 end   = pathPoints[anim.CurrentPath + 1].Position;

                anim.Timer += DeltaTime;
                float t = math.saturate(anim.Timer / anim.MoveTime);
                
                float3 newPos = math.lerp(start, end, t);
                
                anim.CurrentPosition = newPos;

                if (anim.Timer >= anim.MoveTime)
                {
                    anim.Timer = 0f;
                    anim.CurrentPath += 1;
                }
            }
        }
        
        public void OnUpdate(ref SystemState state)
        {
            var job = new MoveJob { DeltaTime = Time.DeltaTime };
            state.Dependency = job.ScheduleParallel(state.Dependency);
        }
    }
}