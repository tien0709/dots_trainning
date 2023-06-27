using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public partial struct GunMovingComponent : IComponentData
    {
        public float Speed;
        public float MaxRange;
        public float MinRange;
    }
}

