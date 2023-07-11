using CortexDeveloper.ECSMessages.Service;
using System.Diagnostics;
using System;
using System.Collections;
using TMPro;
using Components;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace UI_canvas
{
    //[RequireComponent(typeof(Button))]
    public class PauseGameButton : MonoBehaviour
    {
        Button _button;
        bool isPause = false;
        public TextMeshProUGUI Text;

        private void Awake(){
            _button =  GameObject.Find("PauseButton").GetComponent<Button>();
            _button.onClick.AddListener(PauseGame);
        }

        private void Update()
        {

             // _button.onClick.AddListener(PauseGame);
 
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(PauseGame);
        }

        public void PauseGame()
        {
            World world = World.DefaultGameObjectInjectionWorld;
            EntityCommandBufferSystem ecbSystem = world.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer ecb = ecbSystem.CreateCommandBuffer();

            MessageBroadcaster.PrepareMessage().AliveForOneFrame().Post(ecb, new StartGameCommand());
            //RéumeGame
            if(isPause) {
                Time.timeScale = 1f;
                isPause = false;
                //GameObject.Find("PauseButton").GetComponent<TextMeshProUGUI>().text = "Resume";
                Text.text = "Pause";
            }
            //PauseGame
            else {    
                Time.timeScale = 0f;
                isPause = true;
                Text.text = "Resume";
            }
            /* EntityManager _entityManager = world.EntityManager;

            Entity _playerEntity = _entityManager.CreateEntityQuery(typeof(GunMovingComponent)).GetSingletonEntity();
            _entityManager.SetComponentData<GunMovingComponent>() {Speed = 0};

            EntityManager _entityManager = DoWorkEventHandler.EntityManager;
            Entity _enemyEntity = _entityManager.CreateEntityQuery(typeof(BoxMovingComponent)).GetSingletonEntity();
            _entityManager.SetComponentData<BoxMovingComponent>(){Speed = 0}; */
        }

    }
}