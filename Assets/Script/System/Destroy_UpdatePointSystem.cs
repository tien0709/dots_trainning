using System.Drawing;
using Components;
using Unity.Collections;
using Unity.Entities;

namespace System
{
    public partial struct DestroySystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            //  var isPressedSpace = Input.GetKey(KeyCode.Space);
            float count = 0;
            EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (tf, e)
                     in SystemAPI.Query<RefRO<DestroyComponent>>().WithEntityAccess())
            {
               entityCommandBuffer.DestroyEntity(e);
            }
            foreach (var box
                     in SystemAPI.Query<RefRO<DestroyComponent>,RefRO<EnemyComponent>>())
            {
                    count+=1;
            }
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
            foreach (var point
                     in SystemAPI.Query<RefRW<PointComponent>>())
               {
                    point.ValueRW.Point += count;
               }
        }
    }
}