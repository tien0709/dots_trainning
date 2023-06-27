using Components;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public partial struct BulletMovingSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (tf, moving) in SystemAPI.Query<RefRW<LocalTransform>
                         , RefRW<BulletComponent>>())
            {
                moving.ValueRW.Direction = tf.ValueRW.Forward();
                tf.ValueRW.Position += moving.ValueRW.Direction * moving.ValueRW.Speed * SystemAPI.Time.DeltaTime;
            }
        }

    }
}
