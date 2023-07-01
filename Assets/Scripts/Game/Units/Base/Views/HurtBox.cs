using Logic;
using UnityEngine;

namespace Game.Level
{
    public class HurtBox : MonoBehaviour, IHittable
    {
        private GUID _ownersGuid;
        
        public Vector3 Position => transform.position;

        public void SetGuid(GUID guid)
        {
            _ownersGuid = guid;
        }
        
        public bool Hit(Vector3 position, Vector3 normal)
        {
            return true;
        }
    }
}