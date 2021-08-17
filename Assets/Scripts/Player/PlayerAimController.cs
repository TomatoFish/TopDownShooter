using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerAimController : MonoBehaviour
    {
        private PlayerController player;
        
        public void Init(PlayerController player)
        {
            this.player = player;
        }
        
        private void Update()
        {
            var targetLook = player.Player.GetMousePositionRelativeHeight(player.WeaponPosition.y, player.PlayerTransformMovement.position.y);
            if ((targetLook - player.PlayerTransformMovement.position).magnitude < 1.8)
            {
                targetLook = player.PlayerTransformMovement.position + player.GetMouseLookDirection(player.PlayerTransformMovement.position.y) + Vector3.up * player.WeaponPosition.y;
            }

            targetLook = Vector3.Lerp(transform.position, targetLook, 1 / (transform.position - targetLook).magnitude * player.AimSpeed * Time.deltaTime);
            transform.position = targetLook;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 0.2f);
            Gizmos.DrawSphere(transform.position, 0.15f);
        }
    }
}