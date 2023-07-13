using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class GunMovingAuthoring : MonoBehaviour
    {
        public float Speed;
        public float MaxRange;
        public float MinRange;

        public class GunMovingBaker : Baker<GunMovingAuthoring>
        {
            public override void Bake(GunMovingAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new GunMovingComponent
                {
                    Speed = authoring.Speed,
                    MaxRange = authoring.MaxRange,
                    MinRange = authoring.MinRange,
                });
            }

        }

        
    }

}