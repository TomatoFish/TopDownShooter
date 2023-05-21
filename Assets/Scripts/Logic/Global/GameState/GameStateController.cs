namespace Logic
{
    public class GameStateController
    {
        private GameState _state;

        public GameStateController()
        {
            _state = GameState.Play;
        }

        public void SetState(GameState newState)
        {
            if (newState.Equals(_state)) return;

            _state = newState;
        }
    }
}
