using System.Drawing;
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
    public class EndGameUI : MonoBehaviour
    {
        public TextMeshProUGUI PointEnd;
        public TextMeshProUGUI LevelEnd;
        public GameObject bg;
        public GameObject whiteBg;

        public Button PauseButton;
        public Button ReturnButton;
        public GameObject esc;
        public TextMeshProUGUI Point;
        public TextMeshProUGUI Time;
        public TextMeshProUGUI Level;
        private Entity _playerEntity;
        private EntityManager _entityManager;

        private void Start()
        {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }
        
        private void Update()
        {

            if( !_entityManager.CreateEntityQuery(typeof(TimeComponent)).IsEmpty ) {
            _playerEntity = _entityManager.CreateEntityQuery(typeof(TimeComponent)).GetSingletonEntity();
            // phai bo o day khong duoc bo o start vi menu ban dau khien 
            //_playerEntity luon la null ma start chi cap nhat 1 lan duy nhat nên du ban click start game thi _playerEntity luôn là null
            var time = _entityManager.GetComponentData<TimeComponent>(_playerEntity).Time;
            if(time<=0) EndGame();
            }
 
        }


        public void EndGame()
        {
            World world = World.DefaultGameObjectInjectionWorld;
            EntityCommandBufferSystem ecbSystem = world.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer ecb = ecbSystem.CreateCommandBuffer();
            
            _playerEntity = _entityManager.CreateEntityQuery(typeof(LevelComponent)).GetSingletonEntity();
            var point = _entityManager.GetComponentData<PointComponent>(_playerEntity).Point;
            var level = _entityManager.GetComponentData<LevelComponent>(_playerEntity).Level;
            
            MessageBroadcaster.PrepareMessage().AliveForOneFrame().Post(ecb, new EndGameCommand{
                Point = point,
                Level = level
            });

            PointEnd.text = $"Total Point: {point}";
            LevelEnd.text = $"Total Level: {level}";
            bg.gameObject.SetActive(true);
            PointEnd.gameObject.SetActive(true);
            LevelEnd.gameObject.SetActive(true);
            whiteBg.gameObject.SetActive(true);
            ReturnButton.gameObject.SetActive(true);

            Point.gameObject.SetActive(false);
            Time.gameObject.SetActive(false);
            Level.gameObject.SetActive(false);
            esc.gameObject.SetActive(false);
            PauseButton.gameObject.SetActive(false);
        }

    }
}