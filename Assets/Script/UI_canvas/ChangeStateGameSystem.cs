
using System;
using Unity.Entities;
using UnityEngine;
using Components;

namespace UI_canvas
{
    [DisableAutoCreation]
    public partial struct ChangeStateGameSystem : ISystem
    {

        public void OnCreate(ref SystemState state)
        {
            //state.RequireForUpdate<StateGameCommand>();       
        }
        
        public void OnUpdate(ref SystemState state)
        {    
            
            new StateGameCommandListenerJob().Schedule();
            //state.Enabled = false;

        }
    }

    public partial struct StateGameCommandListenerJob : IJobEntity
    {
        public void Execute(in StateGameCommand  state)
        {
                    EntityManager _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                    if( !_entityManager.CreateEntityQuery(typeof(FSMComponent)).IsEmpty ) {
                        Entity _playerEntity = _entityManager.CreateEntityQuery(typeof(FSMComponent)).GetSingletonEntity();
                    // phai bo o day khong duoc bo o start vi menu ban dau khien 
                    //_playerEntity luon la null ma start chi cap nhat 1 lan duy nhat nên du ban click start game thi _playerEntity luôn là null
                        _entityManager.SetComponentData(_playerEntity, new FSMComponent{
                        StatusId = state.currentState
                        });


                    }
        }
    }
}