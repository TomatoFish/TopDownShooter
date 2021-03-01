using UnityEngine;
using System.Collections;
using Assets.Scripts.Input;

namespace Assets.Scripts
{
    public class GameEnterPoint : MonoBehaviour
    {
        private const string charactersPath = "Characters/";

        public CameraBehaviour CameraBehaviour;
        public DebugSettings DebugSettings;
        
        //public PlayerController PlayerController;
        public InputController InputController { get; private set; }

        private Player player;
        private static GameEnterPoint instance;
        public static GameEnterPoint Instance => instance != null ? instance : instance = FindObjectOfType<GameEnterPoint>();

        private void Awake()
        {
            InputController = new InputController();
            player = new Player(InputController);
            var playerGO = Resources.Load<GameObject>(charactersPath + "player");
            var playerInstance = Instantiate(playerGO);
            var playerController = playerInstance.GetComponent<PlayerController>();
            playerController.Init(player);
            CameraBehaviour.Init(playerController);
            //PlayerController.gameObject.SetActive(true);
        }
    }
}