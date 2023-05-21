using System;
using UnityEngine;
using Zenject;

namespace Game.Level
{
    public class EnemyView : UnitView, IHittable
    {
        public Vector3 Position => transform.position;
        
        public bool Hit(Vector3 position, Vector3 normal)
        {
            return true;
        }

        public bool IsDamaged()
        {
            Debug.Log("--> enemy view :: damaged");
            return true;
        }

    }
}
