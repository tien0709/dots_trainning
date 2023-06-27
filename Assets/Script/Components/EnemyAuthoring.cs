using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class EnemyAuthoring : MonoBehaviour
    {

        public class EnemyBaker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new EnemyComponent
                {
                });
            }
        }
    }

}