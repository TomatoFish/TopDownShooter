using Game.Settings;
using Zenject;

namespace Game.Level
{
    public class Dummy : Enemy
    {
        public Dummy(DummyView view, UnitSettings settings, SignalBus signalBus) : base(view, settings, signalBus)
        {
        }
    }
}