using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UI
{
    [UIWidget("MainMenu")]
    public class MainMenuWidget : UIWidget
    {
        private Button _levelButton;
        private Button _exitButton;
        
        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            _levelButton = root.Q<Button>("RunLevel");
            _exitButton = root.Q<Button>("ExitGame");
            
            _levelButton.SetEnabled(true);
            _exitButton.SetEnabled(true);
        }
        
        private void Start()
        {
            _levelButton.clicked += RunLevelButtonHandler;
            _exitButton.clicked += ExitGameButtonHandler;
        }

        private void OnDestroy()
        {
            _levelButton.clicked -= RunLevelButtonHandler;
            _exitButton.clicked -= ExitGameButtonHandler;
        }

        private void RunLevelButtonHandler()
        {
            
        }
        
        private void ExitGameButtonHandler()
        {
            
        }
    }
}