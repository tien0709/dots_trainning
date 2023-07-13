
using CortexDeveloper.ECSMessages.Service;
using TMPro;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;
using Components;

namespace UI_canvas
{
    //[RequireComponent(typeof(Button))]
    public class ChangeStateMessage: MonoBehaviour
    {

        public bool isPause = false;//chuyen qua lai button Pause Resume
        public bool isClickReturn = false;//bao dam khi ReturnGame() run thi EndGame() khong run
        private void Awake()
        {

            if(GameObject.Find("StartButton")) {
                GameObject.Find("StartButton").GetComponent<Button>().onClick.AddListener(PlayGame);
            }
            if(GameObject.Find("ReturnButton")) GameObject.Find("ReturnButton").GetComponent<Button>().onClick.AddListener(ReturnGame);
            if(GameObject.Find("PauseButton")) GameObject.Find("PauseButton").GetComponent<Button>().onClick.AddListener(PauseGame);
 
        }

        public void Update(){
                    EntityManager _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                    if( !_entityManager.CreateEntityQuery(typeof(TimeComponent)).IsEmpty ) {
                        Entity _playerEntity = _entityManager.CreateEntityQuery(typeof(TimeComponent)).GetSingletonEntity();
                    // phai bo o day khong duoc bo o start vi menu ban dau khien 
                    //_playerEntity luon la null ma start chi cap nhat 1 lan duy nhat nên du ban click start game thi _playerEntity luôn là null
                       var time = _entityManager.GetComponentData<TimeComponent>(_playerEntity).Time;
                       if(time<=0 && !isClickReturn)  EndGame();
                    } 
        }


        public void PlayGame()
        {
            World world = World.DefaultGameObjectInjectionWorld;
            EntityCommandBufferSystem ecbSystem = world.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer ecb = ecbSystem.CreateCommandBuffer();

            MessageBroadcaster.PrepareMessage().AliveForOneFrame().Post(ecb, new StateGameCommand{
                currentState = 1f
            });

        }
        
        public void PauseGame()
        {
            World world = World.DefaultGameObjectInjectionWorld;
            EntityCommandBufferSystem ecbSystem = world.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer ecb = ecbSystem.CreateCommandBuffer();

            if(isPause){
            MessageBroadcaster.PrepareMessage().AliveForOneFrame().Post(ecb, new StateGameCommand{
                currentState= 1f
            });
              isPause = false;
            }

            else {
                MessageBroadcaster.PrepareMessage().AliveForOneFrame().Post(ecb, new StateGameCommand{
                currentState= 2f
                });
                isPause = true;
            }


        }

        public void EndGame()
        {
            World world = World.DefaultGameObjectInjectionWorld;
            EntityCommandBufferSystem ecbSystem = world.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer ecb = ecbSystem.CreateCommandBuffer();

            MessageBroadcaster.PrepareMessage().AliveForOneFrame().Post(ecb, new StateGameCommand{
                currentState = 3f
            });

        }
        
        public void ReturnGame()
        {
            isClickReturn = true; 
            World world = World.DefaultGameObjectInjectionWorld;
            EntityCommandBufferSystem ecbSystem = world.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer ecb = ecbSystem.CreateCommandBuffer();
            MessageBroadcaster.PrepareMessage().AliveForOneFrame().Post(ecb, new StateGameCommand{
                currentState = 0f
            });

    }
}
}


