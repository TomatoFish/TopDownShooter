namespace Game.UI
{
    public class ChangeUIStateSignal
    {
        public readonly GameUIState State;

        public ChangeUIStateSignal(GameUIState state)
        {
            State = state;
        }
    }
}