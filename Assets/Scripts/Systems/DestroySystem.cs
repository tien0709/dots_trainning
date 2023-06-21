using Components;
using Unity.Collections;
using Unity.Entities;

namespace Systems
{
    public partial struct DestroySystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            //  var isPressedSpace = Input.GetKey(KeyCode.Space);
            EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (tf, e)
                     in SystemAPI.Query<RefRO<DestroyComponent>>().WithEntityAccess()
                    )
            {
                entityCommandBuffer.DestroyEntity(e);
            }
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}