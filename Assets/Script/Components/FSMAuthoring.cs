using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class FSMAuthoring : MonoBehaviour
    {
        public class FSMAuthoringBaker : Baker<FSMAuthoring>
        {
            public override void Bake(FSMAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new FSMComponent 
                {
                    StatusId = 0f
                }) ;
            }
        }
    }
}