using Zenject;

namespace Game
{
    public class Bootstrap : IInitializable
    {
        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public Bootstrap(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Fire(new RunLevelSignal(LevelType.MainMenu));
        }
    }
}
