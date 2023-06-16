using System;
using Game.UI;
using Zenject;

namespace Game.Scenes
{
    public class LevelManager : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private SceneManager _sceneManager;

        [Inject]
        private void Construct(SignalBus signalBus, SceneManager sceneManager)
        {
            _signalBus = signalBus;
            _sceneManager = sceneManager;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<RunLevelSignal>(RunLevel);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<RunLevelSignal>(RunLevel);
        }

        private async void RunLevel(RunLevelSignal signal)
        {
            _signalBus.Fire(new ChangeUIStateSignal(GameUIState.Loading));
            await _sceneManager.UnloadScene("MenuScene");
            await _sceneManager.LoadScene("SampleScene");
        }
    }
}