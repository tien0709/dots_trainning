using Unity.Mathematics;
using Unity.Entities;

namespace Components
{
    public partial struct BulletSpawnComponent : IComponentData
    {
        public Entity Prefab;
        public float3 Offset;
        public float Angle;
    }
}

