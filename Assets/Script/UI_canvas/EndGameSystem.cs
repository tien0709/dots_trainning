using System.Text;
using Unity.Entities;
using UnityEngine;

namespace UI_canvas
{
    [DisableAutoCreation]
    public partial struct EndGameSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndGameCommand>();       
        }
        
        public void OnUpdate(ref SystemState state)
        {
            
            new EndGameCommandListenerJob().Schedule();
            state.Enabled = false;
        }
    }

        public partial struct EndGameCommandListenerJob : IJobEntity
    {
        public void Execute(in EndGameCommand command)
        {
            Debug.Log($"Game Ended. Level: {command.Level}, Point: {command.Point}.");
            
        }
    }

}
