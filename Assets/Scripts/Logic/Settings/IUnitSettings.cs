namespace Logic.Settings
{
    public interface IUnitSettings
    {
        public IUnitBaseProperties BaseProperties { get; }
        public IUnitMoveProperties MoveProperties { get; }
    }
}