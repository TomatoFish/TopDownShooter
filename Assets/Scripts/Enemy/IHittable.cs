using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public interface IHittable
    {
        bool IsDamaged();

        Vector3 Position { get; }
    }
}