using System.Diagnostics;
using System;
using System.Collections;
using TMPro;
using Components;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace UI_canvas{
public class LevelUI : MonoBehaviour
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
        if( !_entityManager.CreateEntityQuery(typeof(LevelComponent)).IsEmpty ) {
            _playerEntity = _entityManager.CreateEntityQuery(typeof(LevelComponent)).GetSingletonEntity();
            // phai bo o day khong duoc bo o start vi menu ban dau khien 
        //_playerEntity luon la null ma start chi cap nhat 1 lan duy nhat nên du ban click start game thi _playerEntity luôn là null
           var level = _entityManager.GetComponentData<LevelComponent>(_playerEntity).Level;
       // them if de dam bao khong bi bug vi ban dau start menu da disable LevelUI va Esc
           UnityEngine.Debug.Log("ttt");
           Text.text = $"Level:{level}"; 
       }
    }
}
}