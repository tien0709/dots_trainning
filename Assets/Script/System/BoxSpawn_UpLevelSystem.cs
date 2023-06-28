using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;
using Unity.Burst;
using Unity.Transforms;

namespace System
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(SimulationSystemGroup))]
    public partial struct BoxSpawnSystem : ISystem
    {
      
        public partial struct SpawnLocation : IJobEntity
        {
        public NativeArray<Vector3> locationArray ;
        public float numBox;

            
            private void Execute()
            {
                        for(int i=0;i<3;i++){
                            for(int j=0;j<numBox;j++){
                                locationArray[i*(int)numBox+j] = new Vector3( j*3 + i  , i*3 + 2, j*2 - i );
                            }
                        }

            }
        
       }

        
        public partial struct SpawnBox : IJobEntity
        {
        public EntityCommandBuffer ecb;
        public float numBox;
        public float Level;
        public NativeArray<Entity> spawnArray ;
        public NativeArray<Vector3> locationArray ;
        

            
            private void Execute()
            {
                            for(int i = 0; i < numBox; i++ ){
                                if( Level >3  ) Level = 3;
                                Vector3 axis = locationArray[(int)numBox*(int)(Level-1) + i];
                                var newBullet = ecb.Instantiate(spawnArray[0]);
                                ecb.SetComponent(newBullet, new LocalTransform
                                {
                                    Position = axis, 
                                    Scale = 3f,
                                    Rotation =  Quaternion.identity
                                }) ;
                            }
            }
        
        }

        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.TempJob);
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
                        

                        NativeArray<Vector3> LocationArray = new NativeArray<Vector3>(3*(int)spawn.ValueRO.numBox, Allocator.TempJob);

                        state.Dependency = new SpawnLocation
                        {
                            locationArray = LocationArray,
                            numBox = spawn.ValueRO.numBox

                        }.Schedule(state.Dependency); 

                        state.Dependency.Complete();
                       
                        NativeArray<Entity> SpawnArray = new NativeArray<Entity>(1, Allocator.TempJob);
                        SpawnArray[0] = spawn.ValueRO.Prefab;
                        state.Dependency = new SpawnBox
                        {
                        ecb = entityCommandBuffer,
                        numBox = spawn.ValueRO.numBox,
                        Level = Val,
                        spawnArray = SpawnArray,
                        locationArray = LocationArray

                        }.Schedule(state.Dependency);

                        state.Dependency.Complete();
                        SpawnArray.Dispose();
                        LocationArray.Dispose(); 

                    }

            }
                        entityCommandBuffer.Playback(state.EntityManager);
                        entityCommandBuffer.Dispose();
        }
    }
}