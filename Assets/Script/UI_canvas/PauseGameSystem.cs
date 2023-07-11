using Unity.Entities;
using UnityEngine;

namespace UI_canvas
{
    [DisableAutoCreation]
    public partial struct PauseGameSystem : ISystem
    {

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PauseGameCommand>();       
        }
        
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
        }
    }
}