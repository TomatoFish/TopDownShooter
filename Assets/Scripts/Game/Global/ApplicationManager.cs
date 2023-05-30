using System;
using Zenject;

namespace Game
{
    public class ApplicationManager : IInitializable, IDisposable
    {
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<ExitGameSignal>(ExitGame);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ExitGameSignal>(ExitGame);
        }
        
        private void ExitGame(ExitGameSignal signal)
        {
            UnityEngine.Application.Quit();
        }
    }
}