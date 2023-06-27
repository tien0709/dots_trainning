/*using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class RenderBoxAuthoring : MonoBehaviour
    {

        public class RenderBoxBaker : Baker<RenderBoxAuthoring>
        {
            public override void Bake(PointAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                RenderMeshUtility.AddComponents(entity, new RenderBox{

                });
            }
        }
    }

}*/