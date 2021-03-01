using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Assets.Scripts
{
    public class CameraBehaviour : MonoBehaviour
    {
        public float TransitionSpeed;
        public float MaxRadiusSqr;
        
        private PlayerController playerController;

        public void Init(PlayerController playerController)
        {
            this.playerController = playerController;
        }
        
        private void LateUpdate()
        {
            if (playerController == null) return;

            var targetPosition = playerController.GetCameraTargetPosition(MaxRadiusSqr);

            transform.position = Vector3.Lerp(transform.position, targetPosition, TransitionSpeed * Vector3.Distance(transform.position, targetPosition));
        }
    }
}