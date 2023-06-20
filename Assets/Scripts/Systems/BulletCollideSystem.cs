using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public partial struct BulletCollideSystem:ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            return;
            foreach (var (tf, bullet)
                     in SystemAPI.Query<RefRO<LocalTransform>, RefRW<BulletComponent>>())
            {
                //find nearest enemy move to job
                var f1 = new float3();
                var f2 = new float3();
              var dist =  math.distancesq(f1, f2);
              if (dist <= bullet.ValueRO.minDistance)
              {
                  //collide
                  //add component "collided" {bullet, enemy}
              }
            }
        }
    }
    
    //collideResutl{bullet, }
}