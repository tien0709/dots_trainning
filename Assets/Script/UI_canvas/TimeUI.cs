using System.Diagnostics;
using System;
using System.Collections;
using TMPro;
using Components;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace UI_canvas{
public class TimeUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI Text;
    private Entity _playerEntity;
    private EntityManager _entityManager;
    // Start is called before the first frame update
    void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        //_playerEntity = _entityManager.CreateEntityQuery(typeof(LevelComponent)).GetSingletonEntity();
    }

    // Update is called once per frame
    void Update()
    {
       //_entityManager.TryGetComponentData<LevelComponent>(_playerEntity)
        if( !_entityManager.CreateEntityQuery(typeof(TimeComponent)).IsEmpty ) {
            _playerEntity = _entityManager.CreateEntityQuery(typeof(TimeComponent)).GetSingletonEntity();
            // phai bo o day khong duoc bo o start vi menu ban dau khien 
        //_playerEntity luon la null ma start chi cap nhat 1 lan duy nhat nên du ban click start game thi _playerEntity luôn là null
           var time = _entityManager.GetComponentData<TimeComponent>(_playerEntity).Time;
       // them if de dam bao khong bi bug vi ban dau start menu da disable LevelUI va Esc
           Text.text = $"Time Left: {(int)time}"; 
       }
    }
}
}