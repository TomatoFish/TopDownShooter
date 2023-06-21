using System;
using System.Collections.Generic;
using Game.UI;
using Zenject;

namespace Game.Scenes
{
    public class LevelManager : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private SceneManager _sceneManager;
        private List<string> _loadedLevelScenes;

        [Inject]
        private void Construct(SignalBus signalBus, SceneManager sceneManager)
        {
            _signalBus = signalBus;
            _sceneManager = sceneManager;
            _loadedLevelScenes = new List<string>();
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<RunLevelSignal>(OnRunLevelSignal);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<RunLevelSignal>(OnRunLevelSignal);
        }

        private void OnRunLevelSignal(RunLevelSignal signal)
        {
            switch (signal.Type)
            {
                case LevelType.MainMenu:
                    RunMainMenu();
                    break;
                case LevelType.Gameplay:
                    RunLevel(signal.LevelId);
                    break;
            }
        }
        
        private async void RunMainMenu()
        {
            _signalBus.Fire(new ChangeUIStateSignal(GameUIState.Loading));
            await _sceneManager.UnloadAllScenes();
            await _sceneManager.LoadScene("MenuScene");
            _signalBus.Fire(new ChangeUIStateSignal(GameUIState.MainMenu));
        }

        private async void RunLevel(string levelId)
        {
            _signalBus.Fire(new ChangeUIStateSignal(GameUIState.Loading));
            await _sceneManager.UnloadAllScenes();
            await _sceneManager.LoadScene(levelId);
            _loadedLevelScenes.Add(levelId);
            _signalBus.Fire(new ChangeUIStateSignal(GameUIState.Gameplay));
        }
    }
}