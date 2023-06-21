using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

namespace Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(PhysicsSystemGroup))]
    public partial struct GetNumTriggerEventsSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
        }

        [BurstCompile]
        public partial struct CountNumTriggerEvents : ITriggerEventsJob
        {
            [ReadOnly] public ComponentLookup<EnemyComponent> enemyList;
            [ReadOnly] public ComponentLookup<BulletComponent> bulletList;

            public NativeReference<int> NumTriggerEvents;
            public void Execute(TriggerEvent collisionEvent)
            {
                NumTriggerEvents.Value++;
              Debug.Log($"CollisionEvent {collisionEvent.EntityA} {collisionEvent.EntityB}");
              enemyList.HasComponent(collisionEvent.EntityA);
            }
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            NativeReference<int> numTriggerEvents = new NativeReference<int>(0, Allocator.TempJob);
            
            state.Dependency = new CountNumTriggerEvents
            {
                enemyList =  SystemAPI.GetComponentLookup<EnemyComponent>(),
                bulletList = SystemAPI.GetComponentLookup<BulletComponent>(),
                NumTriggerEvents = numTriggerEvents
            }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
            state.Dependency.Complete();
          //  Debug.Log($"NumTriggerEvents {numTriggerEvents.Value}");
        }
    }
}