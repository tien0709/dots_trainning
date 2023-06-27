using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class DestroyAuthoring : MonoBehaviour
    {

        public class DestroyBaker : Baker<DestroyAuthoring>
        {
            public override void Bake(DestroyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new DestroyComponent
                {
                });
            }
        }
    }

}