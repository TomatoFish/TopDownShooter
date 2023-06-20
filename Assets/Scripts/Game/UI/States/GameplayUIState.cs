namespace Game.UI
{
    public class GameplayUIState : UIStateBase
    {
        protected override GameUIState State => GameUIState.Gameplay;
        
        protected override void RunState()
        {
            SignalBus.Fire(new HideAllWidgetsSignal());
            //SignalBus.Fire(new ShowWidgetSignal(typeof(MainMenuWidget)));
        }
    }
}