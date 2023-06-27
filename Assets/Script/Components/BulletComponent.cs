using Unity.Mathematics;
using Unity.Entities;

namespace Components
{
    public partial struct BulletComponent : IComponentData, IEnableableComponent
    {
        public float Speed;
        public float3 Direction;
    }
}