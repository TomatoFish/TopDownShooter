using UnityEngine;

namespace Game.Level
{
    public interface IHittable
    {
        Vector3 Position { get; }

        public bool Hit(Vector3 position, Vector3 normal);
    }
}