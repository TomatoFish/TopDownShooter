using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyDummy : MonoBehaviour, IHittable
    {
        public Vector3 Position => transform.position;
        
        public bool IsDamaged()
        {
            return true;
        }
    }
}