using System;
using Zenject;

namespace Game.UI
{
    public abstract class UIStateBase : IDisposable
    {
        protected SignalBus SignalBus;
        
        protected abstract GameUIState State { get; }
        
        protected abstract void RunState();

        [Inject]
        private void Constrict(SignalBus signalBus)
        {
            SignalBus = signalBus;
            
            SignalBus.Subscribe<ChangeUIStateSignal>(OnChangeUIStateSignal);
        }

        public void Dispose()
        {
            SignalBus.Unsubscribe<ChangeUIStateSignal>(OnChangeUIStateSignal);
        }

        private void OnChangeUIStateSignal(ChangeUIStateSignal signal)
        {
            if (signal.State != State) return;

            RunState();
        }
    }
}