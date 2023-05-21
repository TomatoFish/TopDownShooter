namespace Logic.Settings
{
    public interface IUnitMoveProperties
    {
        public float WalkSpeed { get; }
        public float WalkAcceleration { get; }
        public float RotationSpeed { get; }
        public float BodyRotationAngle { get; }
        public float Height { get; }
        public float AimSpeed { get; }
    }
}