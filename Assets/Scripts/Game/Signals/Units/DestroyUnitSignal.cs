using Logic;

namespace Game.Level
{
    public class DestroyUnitSignal
    {
        public readonly GUID Guid;

        public DestroyUnitSignal(GUID guid)
        {
            Guid = guid;
        }
    }
}