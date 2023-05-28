namespace Game.UI
{
    public class UIState
    {
        private GameUIState _state;

        public GameUIState State
        {
            get => _state;
            set => _state = value;
        }
    }
}