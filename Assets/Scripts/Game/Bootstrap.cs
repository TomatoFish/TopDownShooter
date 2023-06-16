using Game.Scenes;
using Game.UI;
using Zenject;

namespace Game
{
    public class Bootstrap : IInitializable
    {
        private SceneManager _sceneManager;
        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public Bootstrap(SceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }

        public void Initialize()
        {
            _sceneManager.LoadScene("MenuScene", OpenMenu);
        }

        private void OpenMenu()
        {
            _signalBus.Fire(new ShowWidgetSignal(typeof(MainMenuWidget)));
        }
    }
}
