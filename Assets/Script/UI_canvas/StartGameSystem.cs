using Unity.Entities;
using UnityEngine;

namespace UI_canvas
{
    [DisableAutoCreation]
    public partial struct StartGameSystem : ISystem
    {

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<StartGameCommand>();       
        }
        
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
        }
    }
}