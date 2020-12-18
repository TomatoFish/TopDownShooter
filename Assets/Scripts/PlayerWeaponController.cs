using System;
using UnityEngine;

namespace Assets.Scripts
{
    class PlayerWeaponController : MonoBehaviour
    {
        public PlayerController PlayerController;
        public LineRenderer LineRenderer;
        public Transform StartPosition;

        private void Update()
        {
            LineRenderer.enabled = PlayerController.IsAiming;
            if (PlayerController.IsAiming)
            {
                LineRenderer.SetPosition(0, StartPosition.position);
                LineRenderer.SetPosition(1, StartPosition.position + StartPosition.forward * 10);
            }
        }

        private void LateUpdate()
        {
            if (PlayerController.IsAiming)
            {
                var targetLook = PlayerController.GetRelativeMousePosition();
                if (targetLook.magnitude < 1)
                {
                    targetLook = PlayerController.GetMouseLookDirectionRelative();
                }
                transform.LookAt(targetLook);
            }
        }
    }
}
