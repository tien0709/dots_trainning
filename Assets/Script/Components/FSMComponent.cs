
using System.ComponentModel;
using System;
using Components;
using Unity.Mathematics;
using Unity.Entities;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Collections;//for Allocator
using Unity.Transforms;

namespace Components
{
       
       public interface IFSMComp
    {
       void Update(TextMeshProUGUI Point, TextMeshProUGUI Time, TextMeshProUGUI Level, TextMeshProUGUI PointEnd, TextMeshProUGUI LevelEnd,  TextMeshProUGUI startText,
       GameObject esc ,  Button PauseButton, Button ReturnButton, Button StartButton, GameObject Background, GameObject whiteBg);
    }
 
    public struct FSMComponent : IComponentData
    {
  
       public StartStatus start;
       public GameStatus game;
       public PauseStatus pause;
       public EndStatus end;
       public float StatusId;
 
       public void Update(TextMeshProUGUI Point, TextMeshProUGUI Time, TextMeshProUGUI Level, TextMeshProUGUI PointEnd, TextMeshProUGUI LevelEnd,  
       TextMeshProUGUI startText, GameObject esc ,  Button PauseButton, Button ReturnButton, Button StartButton, GameObject bg, GameObject whiteBg)
       {    
            switch (StatusId)
            {
                case 0:
                    start.Update( Point, Time,  Level,  PointEnd,  LevelEnd,  
       startText,  esc ,   PauseButton,  ReturnButton, StartButton,  bg,  whiteBg);
                    break;
                case 1:
                    game.Update(Point, Time,  Level,  PointEnd,  LevelEnd,  
       startText,  esc ,   PauseButton,  ReturnButton, StartButton,  bg,  whiteBg);
                    break;
                case 2:
                    pause.Update(Point, Time,  Level,  PointEnd,  LevelEnd,  
       startText,  esc ,   PauseButton,  ReturnButton, StartButton,  bg,  whiteBg);
                    break;
                case 3:
                    end.Update(Point, Time,  Level,  PointEnd,  LevelEnd,  
       startText,  esc ,   PauseButton,  ReturnButton, StartButton,  bg,  whiteBg);
                    break;
            }
        }
    }
 
  public struct StartStatus: IFSMComp {

    public TimeComponent time;
    public GunMovingComponent gunmoving;
    public BoxSpawnComponent  boxspawn;

    public void Update(TextMeshProUGUI Point, TextMeshProUGUI Time, TextMeshProUGUI Level, TextMeshProUGUI PointEnd, TextMeshProUGUI LevelEnd, 
     TextMeshProUGUI startText, GameObject esc ,  Button PauseButton, Button ReturnButton, Button StartButton, GameObject bg, GameObject whiteBg) {
            Point.gameObject.SetActive(false);
            Time.gameObject.SetActive(false);
            Level.gameObject.SetActive(false);
            PointEnd.gameObject.SetActive(false);
            LevelEnd.gameObject.SetActive(false);  
            PauseButton.gameObject.SetActive(false);
            ReturnButton.gameObject.SetActive(false);
            whiteBg.gameObject.SetActive(false);
            StartButton.gameObject.SetActive(true);
            startText.gameObject.SetActive(true);

        }
    }


public struct GameStatus: IFSMComp {

    public void Update(TextMeshProUGUI Point, TextMeshProUGUI time, TextMeshProUGUI Level, TextMeshProUGUI PointEnd, TextMeshProUGUI LevelEnd, 
     TextMeshProUGUI startText, GameObject esc ,  Button PauseButton, Button ReturnButton, Button StartButton, GameObject bg, GameObject whiteBg) {
            Point.gameObject.SetActive(true);
            time.gameObject.SetActive(true);
            Level.gameObject.SetActive(true);
            PauseButton.gameObject.SetActive(true);
            bg.gameObject.SetActive(false); 
            StartButton.gameObject.SetActive(false);
            startText.gameObject.SetActive(false);
            Time.timeScale = 1f;
            PauseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Pause";

    }
}

    public struct PauseStatus : IFSMComp
    {
       public void Update(TextMeshProUGUI Point, TextMeshProUGUI time, TextMeshProUGUI Level, TextMeshProUGUI PointEnd, TextMeshProUGUI LevelEnd,  TextMeshProUGUI startText,
       GameObject esc ,  Button PauseButton, Button ReturnButton, Button StartButton, GameObject bg, GameObject whiteBg)
        {
            if(Time.timeScale == 1f) {
                Time.timeScale = 0f;
                PauseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Resume";
            }
        }
    }

    public struct EndStatus : IFSMComp
    {
    
        public void Update(TextMeshProUGUI Point, TextMeshProUGUI time, TextMeshProUGUI Level, TextMeshProUGUI PointEnd, TextMeshProUGUI LevelEnd,  TextMeshProUGUI startText,
       GameObject esc ,  Button PauseButton, Button ReturnButton, Button StartButton, GameObject bg, GameObject whiteBg)
        {
            EntityManager _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            Entity _playerEntity = _entityManager.CreateEntityQuery(typeof(LevelComponent)).GetSingletonEntity();
            var point = _entityManager.GetComponentData<PointComponent>(_playerEntity).Point;
            var level = _entityManager.GetComponentData<LevelComponent>(_playerEntity).Level;
            LevelEnd.gameObject.SetActive(true);
            PointEnd.gameObject.SetActive(true);
            LevelEnd.GetComponentInChildren<TextMeshProUGUI>().text =  $"Total Level: {level}";
            PointEnd.GetComponentInChildren<TextMeshProUGUI>().text =  $"Total Point: {point}";

            Point.gameObject.SetActive(false);
            time.gameObject.SetActive(false);
            Level.gameObject.SetActive(false);
            PauseButton.gameObject.SetActive(false);
            bg.gameObject.SetActive(true); 
            ReturnButton.gameObject.SetActive(true);
            whiteBg.gameObject.SetActive(true);
        }
    }
}

