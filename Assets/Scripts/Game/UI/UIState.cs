using Zenject;

namespace Game.UI
{
    public class UIState : IInitializable
    {
        private GameUIState _state;

        public GameUIState State => _state;
        
        public void Initialize()
        {
            
        }
    }
}