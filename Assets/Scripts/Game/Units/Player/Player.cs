using System;
using Game.Settings;
using Logic.Level;
using Zenject;

namespace Game.Level
{
    [Serializable]
    public class Player : Unit<PlayerView, UnitSettings>
    {
        public Player(PlayerView view, UnitSettings settings) : base(view, settings)
        {
        }

        public float Height => Settings.MoveProperties.Height;

        public override void Dispose()
        {
            base.Dispose();
            
            UnityEngine.Object.Destroy((UnityEngine.Object)UnitView.Object);
        }
    }
}