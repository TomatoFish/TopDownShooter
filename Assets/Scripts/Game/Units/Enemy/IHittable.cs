using UnityEngine;

namespace Game.Level
{
    public interface IHittable
    {
        bool IsDamaged();

        Vector3 Position { get; }

        public bool Hit(Vector3 position, Vector3 normal);
    }
}