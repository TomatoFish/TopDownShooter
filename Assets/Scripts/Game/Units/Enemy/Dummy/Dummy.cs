using Game.Settings;

namespace Game.Level
{
    public class Dummy : Enemy
    {
        public Dummy(EnemyView view, UnitSettings settings) : base(view, settings)
        {
        }

        public float WalkSpeed => Settings.MoveProperties.WalkSpeed;
        public float WalkAcceleration => Settings.MoveProperties.WalkAcceleration;
        public float RotationSpeed => Settings.MoveProperties.RotationSpeed;
        public float AimSpeed => Settings.MoveProperties.AimSpeed;
        public float Height => Settings.MoveProperties.Height;
    }
}