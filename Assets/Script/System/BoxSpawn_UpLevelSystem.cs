using Unity.Entities;
using Components;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

namespace System
{

    public partial struct BoxSpawnSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (tf, spawn) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BoxSpawnComponent>>())
            {
                    float num = 0;
                    foreach (var box in SystemAPI.Query<RefRW<EnemyComponent>>())
                    {
                        num++;
                    }
                    if(num==0){
                        float Val = 0f;
                        foreach (var level
                              in SystemAPI.Query<RefRW<LevelComponent>>())
                        {
                                level.ValueRW.Level += 1;
                                Val = level.ValueRO.Level;
                        }
                        if(Val == 1) {
                            for(int i = 0; i < spawn.ValueRO.numBox; i++ ){
                                var newBullet = state.EntityManager.Instantiate(spawn.ValueRW.Prefab);
                                Vector3 axis = new Vector3((i+1), 3, 2*i );// Rotation 
                                state.EntityManager.SetComponentData(newBullet, new LocalTransform
                                {
                                    Position = axis, //tf.ValueRW.Position,
                                    Scale = 3f,
                                    Rotation =  Quaternion.identity
                                }) ;
                            }
                        }
                        
                        else if(Val == 2) {
                            for(int i = 0; i < spawn.ValueRO.numBox; i++ ){
                                var newBullet = state.EntityManager.Instantiate(spawn.ValueRW.Prefab);
                                Vector3 axis = new Vector3((i+1), 2*i, 0 );// Rotation 
                                state.EntityManager.SetComponentData(newBullet, new LocalTransform
                                {
                                    Position = axis, //tf.ValueRW.Position,
                                    Scale = 3f,
                                    Rotation =  Quaternion.identity
                                }) ;
                            }
                        }
                        
                        else {
                            for(int i = 0; i < spawn.ValueRO.numBox; i++ ){
                                var newBullet = state.EntityManager.Instantiate(spawn.ValueRW.Prefab);
                                Vector3 axis = new Vector3((3*i+1), 7-2*i, 0 );// Rotation 
                                state.EntityManager.SetComponentData(newBullet, new LocalTransform
                                {
                                    Position = axis, //tf.ValueRW.Position,
                                    Scale = 3f,
                                    Rotation =  Quaternion.identity
                                }) ;
                            }
                        }

                    }

            }
        }
    }
}