using UnityEngine;

namespace Game.Level
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Transform _leftHandTarget;

        public Transform LeftHandTarget => _leftHandTarget;
        public virtual Transform AimSightTransform => null;
        public bool NeedAimSight => AimSightTransform != null;
        
        public virtual bool Use()
        {
            return true;
        }
    }
}