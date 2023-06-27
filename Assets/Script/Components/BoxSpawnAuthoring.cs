using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class BoxSpawnAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public Vector3 Offset = new Vector3(0, 1, -2);
        public float Angle = 25f;
        public float numBox;

        public class BoxSpawnBaker : Baker<BoxSpawnAuthoring>
        {
            public override void Bake(BoxSpawnAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new BoxSpawnComponent
                {
                    Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                    Offset = authoring.Offset,
                    Angle = authoring.Angle,
                    numBox = authoring.numBox
                });
            }
        }
    }

}