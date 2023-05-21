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

        public float WalkSpeed => Settings.MoveProperties.WalkSpeed;
        public float WalkAcceleration => Settings.MoveProperties.WalkAcceleration;
        public float RotationSpeed => Settings.MoveProperties.RotationSpeed;
        public float AimSpeed => Settings.MoveProperties.AimSpeed;
        public float Height => Settings.MoveProperties.Height;

        public override void Dispose()
        {
            base.Dispose();
            
            UnityEngine.Object.Destroy((UnityEngine.Object)UnitView.Object);
        }
    }
}