using UnityEngine;

namespace Game.Level
{
    public class UnitFactoryArgs : GameObjectFactoryArgs
    {
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;

        public UnitFactoryArgs(GameObject prefab,
                               Transform root,
                               bool isOutOfBounds,
                               Vector3 position,
                               Quaternion rotation)
            : base(prefab, root, isOutOfBounds)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}