using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Game.Level
{
    public class PlayerRigView : MonoBehaviour
    {
        [SerializeField] private MultiAimConstraint _headConstraint;
        [SerializeField] private TwoBoneIKConstraint _leftHandConstraint;
        [SerializeField] private Transform _leftHandTarget;

        public IRigConstraint HeadConstraint => _headConstraint;
        public IRigConstraint LeftHandConstraint => _leftHandConstraint;
        public Transform LeftHandTarget => _leftHandTarget;
    }
}
