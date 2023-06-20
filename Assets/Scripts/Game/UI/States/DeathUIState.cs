namespace Game.UI
{
    public class DeathUIState : UIStateBase
    {
        protected override GameUIState State => GameUIState.Death;
        
        protected override void RunState()
        {
            SignalBus.Fire(new HideAllWidgetsSignal());
            //SignalBus.Fire(new ShowWidgetSignal(typeof(MainMenuWidget)));
        }
    }
}