using UnityEngine;
using System.Collections;
using Assets.Scripts.Input;

namespace Assets.Scripts
{
    public class GameEnterPoint : MonoBehaviour
    {
        public GameObject Player;
        public InputController InputController { get; private set; }

        private static GameEnterPoint instance;
        public static GameEnterPoint Instance => instance != null ? instance : instance = FindObjectOfType<GameEnterPoint>();

        private void Awake()
        {
            InputController = new InputController();
            Player.SetActive(true);
        }
    }
}