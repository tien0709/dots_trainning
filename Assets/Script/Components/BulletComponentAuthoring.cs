using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class BulletComponentAuthoring : MonoBehaviour
    {
        public float Speed = 30f;
        public class BulletComponentBaker : Baker<BulletComponentAuthoring>
        {
            public override void Bake(BulletComponentAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new BulletComponent
                {
                    Speed = authoring.Speed,
                });
            }
        }
    }

}
