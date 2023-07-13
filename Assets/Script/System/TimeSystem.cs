using Unity.Entities;
using Components;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

namespace System
{

    public partial struct TimeSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var time in SystemAPI.Query< RefRW<TimeComponent>>())
            {
                time.ValueRW.Time -=  SystemAPI.Time.DeltaTime;
                if(time.ValueRO.Time<0) time.ValueRW.Time = 0;

            }
        }
    }
}
