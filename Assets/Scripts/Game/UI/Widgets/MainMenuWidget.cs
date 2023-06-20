using UnityEngine.UIElements;
using Zenject;

namespace Game.UI
{
    public class MainMenuWidget : UIWidget
    {
        private SignalBus _signalBus;

        protected override string WidgetName => "MainMenu";
        public override bool IsAdditive => false;

        private Button _levelButton;
        private Button _exitButton;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public MainMenuWidget(UIDocument uiDocument) : base(uiDocument)
        {
            _levelButton = GetVisualElement<Button>("LevelButton");
            _exitButton = GetVisualElement<Button>("ExitButton");
        }

        protected override void ShowInternal()
        {
            _levelButton.clicked += RunLevelButtonHandler;
            _exitButton.clicked += ExitGameButtonHandler;
        }

        protected override void HideInternal()
        {
            _levelButton.clicked -= RunLevelButtonHandler;
            _exitButton.clicked -= ExitGameButtonHandler;
        }

        private void RunLevelButtonHandler()
        {
            _signalBus.Fire(new RunLevelSignal(LevelType.GamePlay, "SampleScene"));
        }

        private void ExitGameButtonHandler()
        {
            _signalBus.Fire(new ExitGameSignal());
        }
    }
}