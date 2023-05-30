using UnityEngine.UIElements;
using Zenject;

namespace Game.UI
{
    [UIWidget("MainMenu")]
    public class MainMenuWidget : UIWidget
    {
        private SignalBus _signalBus;
        
        private Button _levelButton;
        private Button _exitButton;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            _levelButton = root.Q<Button>("LevelButton");
            _exitButton = root.Q<Button>("ExitButton");
            
            _levelButton.SetEnabled(true);
            _exitButton.SetEnabled(true);
        }
        
        private void Start()
        {
            _levelButton.clicked += RunLevelButtonHandler;
            _exitButton.clicked += ExitGameButtonHandler;
        }

        private void OnDestroy()
        {
            _levelButton.clicked -= RunLevelButtonHandler;
            _exitButton.clicked -= ExitGameButtonHandler;
        }

        private void RunLevelButtonHandler()
        {
            _signalBus.Fire<RunLevelSignal>();
        }
        
        private void ExitGameButtonHandler()
        {
            _signalBus.Fire<ExitGameSignal>();
        }
    }
}