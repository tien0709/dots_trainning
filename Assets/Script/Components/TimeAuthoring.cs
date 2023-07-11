using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class TimeAuthoring : MonoBehaviour
    {
       public float Time;
        
        public class TimeBaker : Baker<TimeAuthoring>
        {
            public override void Bake(TimeAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new TimeComponent
                {
                      Time = authoring.Time
                });
            }
        }
    }

}

