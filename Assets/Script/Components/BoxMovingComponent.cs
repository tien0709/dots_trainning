using Unity.Mathematics;
using Unity.Entities;

namespace Components 
{
    public partial struct BoxMovingComponent : IComponentData
    {
        public float MaxRange;
        public float MinRange;
        public float Direction;
        public float Speed;
    }
}
