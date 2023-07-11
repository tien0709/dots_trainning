using Unity.Entities;
using UnityEngine;

namespace UI_canvas
{
    [DisableAutoCreation]
    public partial struct ReturnGameSystem : ISystem
    {

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ReturnGameCommand>();       
        }
        
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
        }
    }
}