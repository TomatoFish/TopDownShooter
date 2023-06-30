using Game.Settings;
using Zenject;

namespace Game.Level
{
    public class Enemy : Unit<EnemyView, UnitSettings>
    {
        public Enemy(EnemyView view, UnitSettings settings, SignalBus signalBus) : base(view, settings, signalBus)
        {
        }
    }
}
