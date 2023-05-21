using Logic;
using Logic.Level;

namespace Game.Level
{
    public class UnitSpawnedSignal
    {
        public readonly IUnit Unit;

        public UnitSpawnedSignal(IUnit unit)
        {
            Unit = unit;
        }
    }
}