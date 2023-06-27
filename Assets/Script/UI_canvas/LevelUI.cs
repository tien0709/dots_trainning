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
        _playerEntity = _entityManager.CreateEntityQuery(typeof(LevelComponent)).GetSingletonEntity();
    }

    // Update is called once per frame
    void Update()
    {
       
       var level = _entityManager.GetComponentData<LevelComponent>(_playerEntity).Level;
       Text.text = $"Level:{level}"; 
    }
}
}