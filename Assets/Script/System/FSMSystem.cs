using System.Diagnostics;
using Unity.Entities;
using Components;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace System{ 
public class FSMSystem : MonoBehaviour
{
        public TextMeshProUGUI PointEnd;
        public TextMeshProUGUI LevelEnd;
        public TextMeshProUGUI startText;
        public GameObject bg;
        public GameObject whiteBg;

        public Button StartButton;
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
    public void Update()
    {
 
         if( !_entityManager.CreateEntityQuery(typeof(FSMComponent)).IsEmpty ) {
            _playerEntity = _entityManager.CreateEntityQuery(typeof(FSMComponent)).GetSingletonEntity();
            // phai bo o day khong duoc bo o start vi menu ban dau khien 
            //_playerEntity luon la null ma start chi cap nhat 1 lan duy nhat nên du ban click start game thi _playerEntity luôn là null
            var fsm = _entityManager.GetComponentData<FSMComponent>(_playerEntity);
            fsm.Update(Point, Time, Level,  PointEnd,  LevelEnd,  
        startText,  esc ,   PauseButton,  ReturnButton,  StartButton,  bg,  whiteBg);
        }
    }
}
}