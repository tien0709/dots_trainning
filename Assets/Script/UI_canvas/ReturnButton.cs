using System.Net.Mime;
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
    public class ReturnGameButton : MonoBehaviour
    {
        Button _button;
        public Button startButton;
        public GameObject text;
        public GameObject whiteBg;
        public TextMeshProUGUI EndLevel;
        public TextMeshProUGUI EndPoint;

        private void Awake(){
            _button =  GameObject.Find("ReturnButton").GetComponent<Button>();
            _button.onClick.AddListener(ReturnGame);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ReturnGame);
        }

        public void ReturnGame()
        {
            World world = World.DefaultGameObjectInjectionWorld;
            EntityCommandBufferSystem ecbSystem = world.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer ecb = ecbSystem.CreateCommandBuffer();


            MessageBroadcaster.PrepareMessage().AliveForOneFrame().Post(ecb, new ReturnGameCommand()
            );

            whiteBg.gameObject.SetActive(false);
            text.gameObject.SetActive(true);
            startButton.gameObject.SetActive(true);
            _button.gameObject.SetActive(false);

            EndPoint.gameObject.SetActive(false);
            EndLevel.gameObject.SetActive(false);

        }

    }
}