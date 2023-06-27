using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class LevelAuthoring : MonoBehaviour
    {
        public class LevelBaker : Baker<LevelAuthoring>
        {
            public override void Bake(LevelAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new LevelComponent
                {
                      Level = 0,//first frame, box haven't been Spawned so BoxSpawn_UpLevelSystem.cs count boxCOunt = 0 => up Level to 1  
                });
            }
        }
    }

}