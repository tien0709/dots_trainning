using Unity.Entities;
using Components;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

namespace System
{

    public partial struct BulletSpawnSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (tf, spawn) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BulletSpawnComponent>>())
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    var newBullet = state.EntityManager.Instantiate(spawn.ValueRW.Prefab);
                    Vector3 axis = new Vector3(0, 5, 0);// Rotation 

                    state.EntityManager.SetComponentData(newBullet, new LocalTransform
                    {
                        Position = tf.ValueRW.Position + spawn.ValueRO.Offset,
                        Scale = 30f,
                        Rotation =  Quaternion.identity * Quaternion.Euler(axis * spawn.ValueRO.Angle)
                    }) ;
                }
            }
        }
    }
}
