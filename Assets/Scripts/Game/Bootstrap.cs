using Game.Scenes;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Bootstrap : IInitializable
    {
        [Inject(Id = "UIContainer")] private Transform _uiContainer;
        
        private SceneManager _sceneManager;
        private UIManager _uiManager;
        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(SignalBus signalBus, UIManager uiManager)
        {
            _signalBus = signalBus;
            _uiManager = uiManager;
        }

        public Bootstrap(SceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }

        public async void Initialize()
        {
            await _uiManager.InitializePool(_uiContainer);
            _sceneManager.LoadScene("MenuScene", OpenMenu);
        }

        private void OpenMenu()
        {
            _signalBus.Fire(new ShowWidgetSignal(typeof(MainMenuWidget)));
        }
    }
}
