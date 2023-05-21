using Game.Scenes;
using Zenject;

namespace Game
{
    public class Bootstrap : IInitializable
    {
        private SceneManager _sceneManager;

        public Bootstrap(SceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }

        public void Initialize()
        {
            _sceneManager.LoadScene("UIScene");
            _sceneManager.LoadScene("SampleScene");
        }
    }
}
