using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;

namespace System
{

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(SimulationSystemGroup))]
    public partial struct BulletCollideSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EnemyComponent>();
            state.RequireForUpdate<BulletComponent>();
            state.RequireForUpdate<SimulationSingleton>();
        }

        public partial struct JobCheckCollision : ITriggerEventsJob
        {
            public ComponentLookup<EnemyComponent> enemyLookup;
            public ComponentLookup<BulletComponent> bulletLookup;
            public EntityCommandBuffer ecb;

            private bool IsEnemy(Entity e)
            {
                return enemyLookup.HasComponent(e);
            }

            private bool IsBullet(Entity e)
            {
                return bulletLookup.HasComponent(e);
            }

            public void Execute(TriggerEvent triggerEvent)
            {

                var isEnemyA = IsEnemy(triggerEvent.EntityA);
                var isBulletA = IsBullet(triggerEvent.EntityA);

                var isEnemyB = IsEnemy(triggerEvent.EntityB);
                var isBulletB = IsBullet(triggerEvent.EntityB);

               /* var validA = (isEnemyA! = isBulletA);
                if (!validA)
                {
                    return;
                }

                var validB = (isEnemyB != isBulletB);
                if (!validB)
                {
                    return;
                }

                var v = (isEnemyA == isBulletB) || (isBulletA == isEnemyB);
                if (!v)
                {
                    return;
                }

                
                //addtag(hitted

                var destroyableA = false;
                var destroyableB = false;
                if (enemyLookup.HasComponent(triggerEvent.EntityA))
                {
                    if (enemyLookup.IsComponentEnabled(triggerEvent.EntityA))
                    {
                        ecb.SetComponentEnabled<EnemyComponent>(triggerEvent.EntityA, false);
                        destroyableA = true;
                    }
                    //a is enemy
                }
                else if (bulletLookup.HasComponent(triggerEvent.EntityA))
                {
                    if (bulletLookup.IsComponentEnabled(triggerEvent.EntityA))
                    {
                        //a is bullet
                        ecb.SetComponentEnabled<BulletComponent>(triggerEvent.EntityA, false);
                        destroyableA = true;
                    }
                }

                if (enemyLookup.HasComponent(triggerEvent.EntityB))
                {
                    if (enemyLookup.IsComponentEnabled(triggerEvent.EntityB))
                    {
                        ecb.SetComponentEnabled<EnemyComponent>(triggerEvent.EntityB, false);
                        destroyableB = true;
                    }
                    //b is enemy
                }
                else if (bulletLookup.HasComponent(triggerEvent.EntityB))
                {
                    if (bulletLookup.IsComponentEnabled(triggerEvent.EntityB))
                    {
                        ecb.SetComponentEnabled<BulletComponent>(triggerEvent.EntityB, false);
                        destroyableB = true;
                    }
                    //b is bullet
                }

                if (destroyableA)
                {
                    ecb.AddComponent<DestroyComponent>(triggerEvent.EntityA);
                }

                if (destroyableB)
                {
                    ecb.AddComponent<DestroyComponent>(triggerEvent.EntityB);
                }*/

                if((isEnemyA&&isBulletB)||(isEnemyB&&isBulletA)){
            ecb.AddComponent<DestroyComponent>(triggerEvent.EntityA);
            ecb.AddComponent<DestroyComponent>(triggerEvent.EntityB);
                }
                
                
            }
        }

        public void OnUpdate(ref SystemState state)
        {
           // state.Enabled = false;
          // SystemAPI.GetSingleton<ConfigCompnent>();
            EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.TempJob);
            state.Dependency = new JobCheckCollision
            {
                enemyLookup = SystemAPI.GetComponentLookup<EnemyComponent>(),
                bulletLookup = SystemAPI.GetComponentLookup<BulletComponent>(),
                ecb = entityCommandBuffer,
            }.Schedule(
                SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);

            state.Dependency.Complete();
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }

    }
}