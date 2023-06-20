namespace Game.UI
{
    public class LoadingUIState : UIStateBase
    {
        protected override GameUIState State => GameUIState.Loading;
        
        protected override void RunState()
        {
            SignalBus.Fire(new HideAllWidgetsSignal());
            //SignalBus.Fire(new ShowWidgetSignal(typeof(MainMenuWidget)));
        }
    }
}