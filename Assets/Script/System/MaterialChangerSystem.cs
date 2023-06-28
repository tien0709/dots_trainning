using System.Collections.Generic;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;
using UnityEngine.Rendering;


namespace System
{
[RequireMatchingQueriesForUpdate]
public partial struct MaterialChangerSystem : ISystem
{
    private Dictionary<Material, BatchMaterialID> m_MaterialMapping;

    private void RegisterMaterial(EntitiesGraphicsSystem hybridRendererSystem, Material material)
    {
        // Only register each mesh once, so we can also unregister each mesh just once
        if (!m_MaterialMapping.ContainsKey(material))
            m_MaterialMapping[material] = hybridRendererSystem.RegisterMaterial(material);
    }

    public void OnStartRunning(ref SystemState state)
    {
        var hybridRenderer = state.GetOrCreateSystemManaged<EntitiesGraphicsSystem>();
        m_MaterialMapping = new Dictionary<Material, BatchMaterialID>();

                foreach (var changer in SystemAPI.Query<RefRW<MaterialChanger>>())
            {
                RegisterMaterial(hybridRenderer, changer.ValueRW.material0);
                RegisterMaterial(hybridRenderer, changer.ValueRW.material1);
            }
    }

   /* private void UnregisterMaterials()
    {
        // Can't call this from OnDestroy(), so we can't do this on teardown
        var hybridRenderer = state.GetExistingSystemManaged<EntitiesGraphicsSystem>();
        if (hybridRenderer == null)
            return;

        foreach (var kv in m_MaterialMapping)
            hybridRenderer.UnregisterMaterial(kv.Value);
    }*/

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
                    mmi.ValueRW.MaterialID = m_MaterialMapping[material];
                }
            }
    }
}
}
