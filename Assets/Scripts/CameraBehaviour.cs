using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraBehaviour : MonoBehaviour
    {
        public float TranslateSpeed;
        public float MaxRadiusSqr;
        public PlayerController PlayerController;

        private void LateUpdate()
        {
            Vector3 targetPosition;
            if (PlayerController.IsAiming)
                targetPosition = FollowAimPosition() + PlayerController.PlayerTransformMovement.position;
            else
                targetPosition = PlayerController.PlayerTransformMovement.position;

            transform.position = Vector3.Lerp(transform.position, targetPosition, TranslateSpeed * Vector3.Distance(transform.position, targetPosition));
        }

        private Vector3 FollowAimPosition()
        {
            var direction = PlayerController.GetRelativeMousePosition() - PlayerController.PlayerTransformMovement.position;
            var distance = direction.magnitude / 2;
            var length = Mathf.Min(MaxRadiusSqr, distance);
            var targetPosition = direction.normalized * length;
            
            //translatePosition = Vector3.Lerp(transform.position, targetPosition, TranslateSpeed * Vector3.Distance(translatePosition, targetPosition));
            return targetPosition;
            
            //transform.position = PlayerController.transform.position + direction.normalized * length;
        }
    }
}