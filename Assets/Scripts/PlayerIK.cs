using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerIK : MonoBehaviour
    {
        [SerializeField] private PlayerController controller;
        [SerializeField] private Transform playerBoneBodyRotation;

        [Range(0, 1)] public float leftFootPositionWeight;
        [Range(0, 1)] public float leftFootRotationWeight;
        public Transform leftFootObj;

        private Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            var lookDir = controller.GetMouseLookDirection();
            var angle = Vector3.Lerp(playerBoneBodyRotation.forward, lookDir, controller.RotationSpeed * Time.deltaTime);

            animator.SetLookAtWeight(leftFootRotationWeight, leftFootRotationWeight, leftFootRotationWeight);
            animator.SetLookAtPosition(playerBoneBodyRotation.position + angle);
            //playerBoneBodyRotation.LookAt(playerBoneBodyRotation.position + angle);



            //animator.SetIKPositionWeight(AvatarIKGoal.RightHand, leftFootPositionWeight);
            //animator.SetIKRotationWeight(AvatarIKGoal.RightHand, leftFootRotationWeight);
            //animator.SetIKPosition(AvatarIKGoal.RightHand, leftFootObj.position);
            //animator.SetIKRotation(AvatarIKGoal.RightHand, leftFootObj.rotation);
        }
    }
}