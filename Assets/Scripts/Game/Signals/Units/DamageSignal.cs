using UnityEngine;

namespace Game.Level
{
    public class DamageSignal
    {
        public readonly float Value;
        public readonly Vector3 Position;
        public readonly Vector3 Normal;

        public DamageSignal(float value, Vector3 position, Vector3 normal)
        {
            Value = value;
            Position = position;
            Normal = normal;
        }
    }
}