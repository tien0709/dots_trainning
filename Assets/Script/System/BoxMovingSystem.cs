using Unity.Entities;
using Components;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

namespace System
{

    public partial struct BoxMovingSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (tf, moving, e) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BoxMovingComponent>, EnabledRefRO<EnemyComponent>>())
            {
                tf.ValueRW.Position.x += moving.ValueRW.Direction * moving.ValueRO.Speed * SystemAPI.Time.DeltaTime;
                if (tf.ValueRW.Position.x > moving.ValueRO.MaxRange)
                {
                    tf.ValueRW.Position.x = moving.ValueRO.MaxRange;
                    moving.ValueRW.Direction = -1;
                }
                else if (tf.ValueRW.Position.x < moving.ValueRO.MinRange)
                {
                    tf.ValueRW.Position.x = moving.ValueRO.MinRange;
                    moving.ValueRW.Direction = 1;
                }
            }
        }
    }
}
