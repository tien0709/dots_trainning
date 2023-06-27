using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class PointAuthoring : MonoBehaviour
    {

        public class PointBaker : Baker<PointAuthoring>
        {
            public override void Bake(PointAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PointComponent
                {
                      Point = 0
                });
            }
        }
    }

}
