using UnityEngine;

namespace Game.Level
{
    public class SpawnUnitSignal
    {
        public readonly string UnitId;
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;

        public SpawnUnitSignal(string unitId, Vector3 position, Quaternion rotation)
        {
            UnitId = unitId;
            Position = position;
            Rotation = rotation;
        }
    }
}