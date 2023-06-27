using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class BulletSpawnAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public Vector3 Offset = new Vector3(0, 1, -2);
        public float Angle = 25f;

        public class BulletSpawnBaker : Baker<BulletSpawnAuthoring>
        {
            public override void Bake(BulletSpawnAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new BulletSpawnComponent
                {
                    Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                    Offset = authoring.Offset,
                    Angle = authoring.Angle,
                });
            }
        }
    }

}
