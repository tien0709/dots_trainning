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
        public Button _button;
        public Button PauseButton;
        public Button ReturnButton;
        public GameObject bg;
        public GameObject text;
        public GameObject esc;
        public GameObject whiteBg;
        public TextMeshProUGUI Time;
        public TextMeshProUGUI Point;
        public TextMeshProUGUI Level;
        public TextMeshProUGUI EndLevel;
        public TextMeshProUGUI EndPoint;
        bool isPressed = false;
        private void Update()
        { 
            /*Point = GameObject.Find("Point").GetComponent<TextMeshProUGUI>();*/
            Point = Point.GetComponent<TextMeshProUGUI>();
            Level = Level.GetComponent<TextMeshProUGUI>();
            EndPoint = EndPoint.GetComponent<TextMeshProUGUI>();
            EndLevel = EndLevel.GetComponent<TextMeshProUGUI>();
            Time = Time.GetComponent<TextMeshProUGUI>();
            if (!isPressed)
            {
                Point.gameObject.SetActive(false);
                Level.gameObject.SetActive(false);
                esc.gameObject.SetActive(false);
                Time.gameObject.SetActive(false);
                PauseButton.gameObject.SetActive(false);
                EndLevel.gameObject.SetActive(false);
                EndPoint.gameObject.SetActive(false);
                whiteBg.gameObject.SetActive(false);
                ReturnButton.gameObject.SetActive(false);
            }
        }

        private void Awake()
        {
           // _button =  GameObject.Find("StartButton").GetComponent<Button>();

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
            text.gameObject.SetActive(false);
            _button.gameObject.SetActive(false);

            Point.gameObject.SetActive(true);
            Time.gameObject.SetActive(true);
            Level.gameObject.SetActive(true);
            esc.gameObject.SetActive(true);
            PauseButton.gameObject.SetActive(true);
        }
    }
}


