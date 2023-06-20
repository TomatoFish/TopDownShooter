using System;

namespace Game.UI
{
    public class MainMenuUIState : UIStateBase
    {
        protected override GameUIState State => GameUIState.MainMenu;

        protected override void RunState()
        {
            SignalBus.Fire(new HideAllWidgetsSignal());
            SignalBus.Fire(new ShowWidgetSignal(typeof(MainMenuWidget)));
        }
    }
}