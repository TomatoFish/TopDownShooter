using System;
using Game.Settings;
using Zenject;

namespace Game.Level
{
    [Serializable]
    public class Player : Unit<PlayerView, UnitSettings>
    {
        public Player(PlayerView view, UnitSettings settings, SignalBus signalBus) : base(view, settings, signalBus)
        {
        }

        public override void Dispose()
        {
            base.Dispose();
            
            UnityEngine.Object.Destroy((UnityEngine.Object)UnitView.Object);
        }
    }
}