//using System.Collections.Generic;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using Components;

namespace System
{
//[RequireMatchingQueriesForUpdate]
public partial struct MaterialChangerSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {

    }
    public void OnUpdate(ref SystemState state)
    {

            foreach (var (changer, mmi) in SystemAPI.Query<RefRW<MaterialChanger>, RefRW<MaterialMeshInfo>>())
            {
                changer.ValueRW.frame = changer.ValueRO.frame + 1;

                if (changer.ValueRO.frame >= changer.ValueRO.frequency)
                {
                    changer.ValueRW.frame = 0;
                    changer.ValueRW.active = changer.ValueRO.active == 0 ? 1u : 0u;
                    var material = changer.ValueRO.active == 0 ? changer.ValueRO.material0 : changer.ValueRO.material1;
                    mmi.ValueRW.MaterialID = material;
                }
            }
    }
}
}
