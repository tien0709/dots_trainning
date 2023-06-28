using System.Collections.Generic;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

 
namespace Components{
public class MaterialChangerAuthoring : MonoBehaviour
{
    public Material material0;
    public Material material1;
    public uint frequency = 1000;
    public uint frame = 1000;
    public uint active = 1;

    class MaterialChangerBaker : Baker<MaterialChangerAuthoring>
    {
        public override void Bake(MaterialChangerAuthoring authoring)
        {
            var hybridRenderer = World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<EntitiesGraphicsSystem>();
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new MaterialChanger {

                material0 = hybridRenderer.RegisterMaterial(authoring.material0),
                material1 = hybridRenderer.RegisterMaterial(authoring.material1),
                frequency = authoring.frequency,
                frame = authoring.frame,
                active = authoring.active
            });
        }
    }
}
}