using System.Diagnostics;
using CortexDeveloper.ECSMessages.Service;
using TMPro;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace UI_canvas
{
    //[RequireComponent(typeof(Button))]
    public class StartGameButton : MonoBehaviour
    {
        Button _button;
        public GameObject bg;
        public GameObject menu;
        public GameObject text;
        public GameObject esc;
        TextMeshProUGUI Point;
        TextMeshProUGUI Level;
        bool isPressed = false;
private void Update()
{

        if(GameObject.Find("Point")) Point = GameObject.Find("Point").GetComponent<TextMeshProUGUI>();
        if(GameObject.Find("Level")) Level = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
    if (!isPressed)
    {
        Point.gameObject.SetActive(false);
        Level.gameObject.SetActive(false);
        esc.gameObject.SetActive(false);
    }
}

        private void Awake()
        {
            _button =  GameObject.Find("StartButton").GetComponent<Button>();

              _button.onClick.AddListener(StartGame);
 
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(StartGame);
        }

        public void StartGame()
        {
            World world = World.DefaultGameObjectInjectionWorld;
            EntityCommandBufferSystem ecbSystem = world.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer ecb = ecbSystem.CreateCommandBuffer();

            MessageBroadcaster.PrepareMessage().AliveForOneFrame().Post(ecb, new StartGameCommand());
            isPressed = true;
            bg.gameObject.SetActive(false);
            menu.gameObject.SetActive(false);
            text.gameObject.SetActive(false);

            Point.gameObject.SetActive(true);
            Level.gameObject.SetActive(true);
            esc.gameObject.SetActive(true);
        }
    }
}