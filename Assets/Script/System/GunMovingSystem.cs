using Unity.Entities;
using Components;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

namespace System
{

    public partial struct GunMovingSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            foreach (var (tf, moving) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<GunMovingComponent>>())
            {
                tf.ValueRW.Position.x -= horizontalInput * moving.ValueRO.Speed * SystemAPI.Time.DeltaTime;
                tf.ValueRW.Position.y += verticalInput * moving.ValueRO.Speed * SystemAPI.Time.DeltaTime;
                if (tf.ValueRW.Position.x > moving.ValueRO.MaxRange)
                {
                    tf.ValueRW.Position.x = moving.ValueRO.MaxRange;
                }
                else if (tf.ValueRW.Position.x < moving.ValueRO.MinRange)
                {
                    tf.ValueRW.Position.x = moving.ValueRO.MinRange;
                }
                if (tf.ValueRW.Position.y > moving.ValueRO.MaxRange)
                {
                    tf.ValueRW.Position.y = moving.ValueRO.MaxRange;
                }
                else if (tf.ValueRW.Position.y < moving.ValueRO.MinRange)
                {
                    tf.ValueRW.Position.y = moving.ValueRO.MinRange;
                }
            }
        }
    }
}

