using Unity.Mathematics;
using Unity.Entities;

namespace Components
{
    public partial struct BoxSpawnComponent : IComponentData
    {
        public Entity Prefab;
        public float3 Offset;
        public float Angle;
        public float numBox;
    }
}