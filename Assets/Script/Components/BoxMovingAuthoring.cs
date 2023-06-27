using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class BoxMovingAuthoring : MonoBehaviour
    {
        public float Speed;
        public float MaxRange;
        public float MinRange;

        public class BoxMovingBaker : Baker<BoxMovingAuthoring>
        {
            public override void Bake(BoxMovingAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new BoxMovingComponent
                {
                    Speed = authoring.Speed,
                    MaxRange = authoring.MaxRange,
                    MinRange = authoring.MinRange,
                    Direction = 1f
                }) ;
            }
        }
    }

}
